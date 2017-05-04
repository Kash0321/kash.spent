using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace kash.spent
{
    /// <summary>
    /// Base de los ViewModels de toda la aplicación
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Inicializa una instancia de <see cref="BaseViewModel"/>
        /// </summary>
        public BaseViewModel()
        {
        }

        /// <summary>
        /// Evento al que se suscriben las vistas medinte su mecanismo de bindeo, para actualizar la presentación
        /// en función de los cambios que se producen en el ViewModel
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isBusy;
        /// <summary>
        /// Flag para controlar si el ViewModel ya está ejecutado alguna otra operación
        /// </summary>
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Debe invocarse desde las clases derivadas, para procurar la notificación de cambios en los datos
        /// del ViewModel hacia la Vista
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
