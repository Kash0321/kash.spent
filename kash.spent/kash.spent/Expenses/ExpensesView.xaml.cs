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
    }
}
