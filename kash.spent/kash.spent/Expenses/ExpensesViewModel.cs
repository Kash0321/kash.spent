using kash.spent.ExpenseDetail;
using kash.spent.Model;
using kash.spent.NewExpense;
using kash.spent.Services;
using Microsoft.WindowsAzure.Storage;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace kash.spent.Expenses
{
    /// <summary>
    /// ViewModel que gestiona la entidad <see cref="Expense"/>
    /// </summary>
    public class ExpensesViewModel : BaseViewModel
    {
        /// <summary>
        /// Colección de gastos gestionados
        /// </summary>
        public ObservableCollection<Expense> Expenses { get; set; }

        /// <summary>
        /// Comando: Obtiene los gastos ya registrados
        /// </summary>
        public Command GetExpensesCommand { get; set; }

        public Command AddExpenseCommand { get; set; }

        /// <summary>
        /// Inicializa una instancia de <see cref="ExpensesViewModel"/>
        /// </summary>
        public ExpensesViewModel()
        {
            Expenses = new ObservableCollection<Expense>();
            GetExpensesCommand = new Command(async () => await GetExpensesAsync());
            AddExpenseCommand = new Command(() => AddExpense());
            GetExpensesAsync();

            MessagingCenter.Subscribe<NewExpenseViewModel, object[]>(this, "AddExpense", async (obj, expenseData) =>
            {
                var expense = expenseData[0] as Expense;
                var photo = expenseData[1] as MediaFile;
                Expenses.Add(expense);

                // TODO: Upload photo to Azure Storage.
                if (photo != null)
                {
                    // Connect to the Azure Storage account.
                    // NOTE: You should use SAS tokens instead of Shared Keys in production applications.
                    var storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=spentkash;AccountKey=7QrTqmxtaH2DsyLLjiuV0/US+q6ke5P5wZY3/1WOJs+oRcArIF7il3HJEZ6XV00s086Ei0I4bgLdqh1JD/m8kA==;EndpointSuffix=core.windows.net");
                    var blobClient = storageAccount.CreateCloudBlobClient();

                    // Create the blob container if it doesn't already exist.
                    var container = blobClient.GetContainerReference("receipts");
                    await container.CreateIfNotExistsAsync();

                    // Upload the blob to Azure Storage.
                    var blockBlob = container.GetBlockBlobReference(Guid.NewGuid().ToString());
                    await blockBlob.UploadFromStreamAsync(photo.GetStream());
                    expense.Receipt = blockBlob.Uri.ToString();
                }

                await DependencyService.Get<IDataService>().AddExpenseAsync(expense);
            });
        }

        Expense selectedExpenseItem;
        /// <summary>
        /// Gasto seleccionado en la UI
        /// </summary>
        public Expense SelectedExpenseItem
        {
            get { return selectedExpenseItem; }
            set
            {
                selectedExpenseItem = value;
                OnPropertyChanged();

                if (selectedExpenseItem != null)
                {
                    MessagingCenter.Send(this, "NavigateToDetail", SelectedExpenseItem);
                    SelectedExpenseItem = null;
                }
            }
        }

        async Task GetExpensesAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Expenses.Clear();

                var expenses = await DependencyService.Get<IDataService>().GetExpensesAsync();
                foreach (var expense in expenses)
                {
                    Expenses.Add(expense);
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(this, "Error", ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
        void AddExpense()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                MessagingCenter.Send(this, "NavigateToNewExpense", "NewExpenseView");
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(this, "Error", ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
