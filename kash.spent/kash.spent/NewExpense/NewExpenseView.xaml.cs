using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace kash.spent.NewExpense
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewExpenseView : ContentPage
    {
        public NewExpenseView()
        {
            InitializeComponent();
            BindingContext = new NewExpenseViewModel();
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
            MessagingCenter.Subscribe<NewExpenseViewModel, string>(this, "Error", (obj, s) =>
            {
                DisplayAlert("Error", s, "OK");
            });

            MessagingCenter.Subscribe<NewExpenseViewModel, string>(this, "NavigateBack", async (obj, s) =>
            {
                if (s == "ExpensesView")
                {
                    await Navigation.PopAsync();
                }
            });
        }

        void UnsubscribeFromMessages()
        {
            MessagingCenter.Unsubscribe<NewExpenseViewModel, string>(this, "Error");
            MessagingCenter.Unsubscribe<NewExpenseViewModel, string>(this, "NavigateBack");
        }
    }
}
