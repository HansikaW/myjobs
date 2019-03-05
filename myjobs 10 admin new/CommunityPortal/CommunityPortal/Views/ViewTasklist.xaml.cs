using CommunityPortal.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CommunityPortal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewTasklist : ContentPage
    {
        public ViewTasklist()
        {
            InitializeComponent();

            GetdataAsync();
        }

        public static MobileServiceClient client = new MobileServiceClient("https://myjobapps.azurewebsites.net");


        IMobileServiceTable<CallerTaskRecord> DataTable = client.GetTable<CallerTaskRecord>();





        public async Task GetdataAsync()
        {
            try
            {



                IMobileServiceTableQuery<string> query = DataTable
                .Where(ur => ur.pholdername != null)
               .Select(ur => ur.pholdername);

                List<string> items2 = await query.ToListAsync();
                EmployeeView22.ItemsSource = items2;

                // finalname.Text = string.Format("{0}-{1}", items2[0].FirstName, items2[0].LastName);




            }
            catch (Exception e)
            {

                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }



        }



        private async void Select_invoice(object sender, SelectedItemChangedEventArgs e)
        {
            tempdata.selected_InvoiceSubject = (string)EmployeeView22.SelectedItem;

            await Navigation.PushAsync(new ShowSelectedTask(), true);
        }











        /*


        public async Task<ObservableCollection<UserReg>> GetItemsAsync(bool syncItems = false)
        {
            try
            {       
                IEnumerable<UserReg> items = await DataTable.ToEnumerableAsync();
             

                return new ObservableCollection<UserReg>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine("Invalid sync operation: {0}", new[] { msioe.Message });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }
            



    }


        */
    }
}