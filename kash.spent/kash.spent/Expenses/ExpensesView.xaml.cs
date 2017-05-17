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

#if __ANDROID__
using Android.Support.Design.Widget;
using Xamarin.Forms.Platform.Android;
#endif

namespace kash.spent.Expenses
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpensesView : ContentPage
    {
        public ExpensesView()
        {
            InitializeComponent();

            #if __ANDROID__
            ToolbarItems.RemoveAt(0);

            var fab = new FloatingActionButton(Forms.Context)
            {
	            UseCompatPadding = true
            };

            fab.Click += (sender, e) =>
            {
	            var viewModel = BindingContext as ExpensesViewModel;
	            viewModel.AddExpenseCommand.Execute(null);
            };

            relativeLayout.Children.Add(fab.ToView(), 
                Constraint.RelativeToParent((parent) =>
	            {
		            return parent.Width - 100;
	            }),
                Constraint.RelativeToParent((parent) =>
	            {
		            return parent.Height - 100;
	            }),
                Constraint.Constant(75),
	            Constraint.Constant(85));
            #endif

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
