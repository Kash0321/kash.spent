using kash.spent.ExpenseDetail;
using kash.spent.Model;
using kash.spent.NewExpense;
using kash.spent.Services;
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

            MessagingCenter.Subscribe<NewExpenseViewModel, Expense>(this, "AddExpense", async (obj, expense) =>
            {
                Expenses.Add(expense);

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
