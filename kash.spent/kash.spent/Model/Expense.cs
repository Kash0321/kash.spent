using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kash.spent.Model
{
    /// <summary>
    /// Representa un gasto concreto que justificar o registrar
    /// </summary>
    public class Expense
    {
        /// <summary>
        /// Empresa a la que justificar el gasto
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Descripción del gasto
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Cantidad económica en la que se ha incurrido
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// Fecha en la que se ha producido el gasto
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Ruta física a la captura digital del ticket o la factura del gasto
        /// </summary>
        public string Receipt { get; set; }
    }
}
