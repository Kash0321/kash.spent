using kash.spent.ExpenseDetail;
using kash.spent.Model;
using kash.spent.NewExpense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace kash.spent.Expenses
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpensesView : ContentPage
    {
        public ExpensesView()
        {
            InitializeComponent();

            BindingContext = new ExpensesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SubscribeToMessages();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            UnsubscribeFromMessages();
        }

        void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<ExpensesViewModel, Expense>(this, "NavigateToDetail", async (obj, expense) =>
            {
                if (expense != null)
                {
                    await Navigation.PushAsync(new ExpenseDetailView(expense));
                }
            });

            MessagingCenter.Subscribe<ExpensesViewModel, string>(this, "Error", (obj, s) =>
            {
                DisplayAlert("Error", s, "OK");
            });

            MessagingCenter.Subscribe<ExpensesViewModel, string>(this, "NavigateToNewExpense", async (obj, s) =>
            {
                if (s == "NewExpenseView")
                {
                    await Navigation.PushAsync(new NewExpenseView());
                }
            });
        }

        void UnsubscribeFromMessages()
        {
            MessagingCenter.Unsubscribe<ExpensesViewModel, Expense>(this, "NavigateToDetail");
            MessagingCenter.Unsubscribe<ExpensesViewModel, string>(this, "Error");
            MessagingCenter.Unsubscribe<ExpensesViewModel, string>(this, "NavigateToNewExpense");
        }
    }
}
