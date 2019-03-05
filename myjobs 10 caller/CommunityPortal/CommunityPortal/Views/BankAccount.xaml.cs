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
    public partial class BankAccount : ContentPage
    {
        public BankAccount()
        {
            InitializeComponent();
            GetdataAsync();
        }

        public static MobileServiceClient client = new MobileServiceClient("https://myjobapp.azurewebsites.net");


        IMobileServiceTable<BankAcount> DataTable = client.GetTable<BankAcount>();





        public async Task GetdataAsync()
        {
            try
            {


               

                IMobileServiceTableQuery<string> query = DataTable
                .Where(ur => ur.ServiceProviderId == tempdata.Loginas)
               .Select(todoItem => todoItem.AccountName);

                List<string> items2 = await query.ToListAsync();
                bankaccount.ItemsSource = items2;

                if (items2.Count>0) {

                    addbtn.IsEnabled = false;

                }

                // finalname.Text = string.Format("{0}-{1}", items2[0].FirstName, items2[0].LastName);




            }
            catch (Exception e)
            {

                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }



        }





        private async void addaccount(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddBankaccount(), true);
        }

        private async void Select_account(object sender, SelectedItemChangedEventArgs e)
        {
            tempdata.selected_bankaccount = (string)bankaccount.SelectedItem;

            await Navigation.PushAsync(new ShowSelectedBankAccount(), true);
        }
    }
}







