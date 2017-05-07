using kash.spent.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kash.spent.Services
{
    /// <summary>
    /// Contrato del Data Service 
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Añade un nuevo gasto al sistema
        /// </summary>
        /// <param name="expense">Gasto a añadir</param>
        /// <returns></returns>
        Task AddExpenseAsync(Expense expense);

        /// <summary>
        /// Obtiene todos los gastos registrados en el sistema
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Expense>> GetExpensesAsync();
    }
}
