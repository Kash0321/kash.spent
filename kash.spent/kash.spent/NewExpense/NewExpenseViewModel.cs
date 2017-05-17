using kash.spent.Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace kash.spent.NewExpense
{
    public class NewExpenseViewModel : BaseViewModel
    {
        public string Company { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string Amount { get; set; }

        string receipt;
        public string Receipt
        {
            get { return receipt; }
            set { receipt = value; OnPropertyChanged(); }
        }

        async Task AttachReceiptAsync()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                MediaFile photo;
                if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
                {
                    photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        Directory = "Expenses",
                        Name = "expense.jpg"
                    });
                }
                else
                {
                    photo = await CrossMedia.Current.PickPhotoAsync();
                }

                Receipt = photo?.Path;
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

        async Task SaveExpenseAsync()
        {
            try
            {
                var expense = new Expense
                {
                    Company = Company,
			        Description = Description,
			        Date = DateTime,
			        Amount = Amount,
			        Receipt = Receipt
                };

                MessagingCenter.Send(this, "AddExpense", expense);
                MessagingCenter.Send(this, "NavigateBack", "ExpensesView");
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

        public Command SaveExpenseCommand { get; set; }
        public Command AttachReceiptCommand { get; set; }

        public NewExpenseViewModel()
        {
            AttachReceiptCommand = new Command(
                async () => await AttachReceiptAsync());

            SaveExpenseCommand = new Command(
                async () => await SaveExpenseAsync());
        }
    }
}
