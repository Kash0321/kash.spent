using kash.spent.Expenses;
using kash.spent.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace kash.spent.ExpenseDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpenseDetailView : ContentPage
    {
        /// <summary>
        /// Gasto a gestionar
        /// </summary>
        public Expense Expense { get; set; }

        public ExpenseDetailView(Expense expense)
        {
            InitializeComponent();

            Expense = expense;
            BindingContext = this;
        }
    }
}
