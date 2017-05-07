using kash.spent.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(kash.spent.Services.DataServiceMock))]
namespace kash.spent.Services
{
    public class DataServiceMock : IDataService
    {
        bool isInitialized;
        List<Expense> expenses;

        void Initialize()
        {
            if (isInitialized)
                return;

            expenses = new List<Expense>
            {
                new Expense { Company = "Walmart", Description = "Always low prices.", Amount = "$14.99", Date = DateTime.Now },
                new Expense { Company = "Apple", Description = "New iPhone came out - irresistable.", Amount = "$999", Date = DateTime.Now.AddDays(-7) },
                new Expense { Company = "Amazon", Description = "Case to protect my new iPhone.", Amount = "$50", Date = DateTime.Now.AddDays(-2) }
            };

            isInitialized = true;
        }

        /// <inherited />
        public async Task AddExpenseAsync(Expense expense)
        {
            Initialize();

            expenses.Add(expense);
        }

        /// <inherited />
        public async Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            Initialize();

            return expenses;
        }
    }
}
