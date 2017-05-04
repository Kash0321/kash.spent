using kash.spent.ExpenseDetail;
using kash.spent.Model;
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
        ExpensesView _expensesView;

        /// <summary>
        /// Colección de gastos gestionados
        /// </summary>
        public ObservableCollection<Expense> Expenses { get; set; }

        /// <summary>
        /// Comando: Obtiene los gastos ya registrados
        /// </summary>
        public Command GetExpensesCommand { get; set; }

        /// <summary>
        /// Inicializa una instancia de <see cref="ExpensesViewModel"/>
        /// </summary>
        public ExpensesViewModel(ExpensesView expensesView)
        {
            _expensesView = expensesView;
            Expenses = new ObservableCollection<Expense>();
            GetExpensesCommand = new Command(async () => await GetExpensesAsync());
            GetExpensesAsync();
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
                    _expensesView.Navigation.PushAsync(new ExpenseDetailView(SelectedExpenseItem));
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
                Expenses.Add(new Expense
                {
                    Company = "Walmart",
                    Description = "Always low prices.",
                    Amount = "$14.99",
                    Date = DateTime.Now
                });
                Expenses.Add(new Expense
                {
                    Company = "Apple",
                    Description = "New iPhone came out - irresistable.",
                    Amount = "$999",
                    Date = DateTime.Now.AddDays(-7)
                });
                Expenses.Add(new Expense
                {
                    Company = "Amazon",
                    Description = "Case to protect my new iPhone.",
                    Amount = "$50",
                    Date = DateTime.Now.AddDays(-2)
                });
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
