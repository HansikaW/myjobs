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
	public partial class ShowSelectedBankAccount : ContentPage
	{
		public ShowSelectedBankAccount ()
		{
			InitializeComponent ();

            GetdataAsync();

        }


        public static MobileServiceClient client = new MobileServiceClient("https://myjobapp.azurewebsites.net");


        IMobileServiceTable<BankAcount> DataTable = client.GetTable<BankAcount>();



        public async Task GetdataAsync()
        {
            try
            {


              

                IMobileServiceTableQuery<BankAcount> query = DataTable
                .Where(ur => ur.ServiceProviderId == tempdata.Loginas && ur.AccountName == tempdata.selected_bankaccount);
                // .Select(todoItem => todoItem.InvoiceSubject);

               // EmployeeView.ItemsSource = items2;
                List<BankAcount> items = await query.ToListAsync();


                // finalname.Text = string.Format("{0}-{1}", items2[0].FirstName, items2[0].LastName);

                lb1.Text = string.Format("{0} :- {1}", "Account Name", items[0].AccountName);
                lb2.Text = string.Format("{0} :- {1}", "Bank", items[0].Bank);
                lb3.Text = string.Format("{0} :- {1}", "Branch", items[0].Branch);
                lb4.Text = string.Format("{0} :- {1}", "Account Number", items[0].AccountNumber);
                lb5.Text = string.Format("{0} :- {1}", "Other info", items[0].Info);


                tempdata.uId = items[0].Id;
                tempdata.uAccountName = items[0].AccountName;
                tempdata.uBank = items[0].Bank;
                tempdata.uBranch = items[0].Branch;
                tempdata.uAccountNumber = items[0].AccountNumber;
                tempdata.uInfo = items[0].Info;













            }
            catch (Exception e)
            {
                DisplayAlert("Erro", "connection lost", "Ok");
                
                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }



        }

        private async void updateaccount(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UpdateBankAccount(), true);

        }
    }
}