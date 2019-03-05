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
	public partial class UpdateBankAccount : ContentPage
	{
		public UpdateBankAccount ()
		{
			InitializeComponent ();
            autofill();



        }

        public static MobileServiceClient MobileService =
        new MobileServiceClient("https://myjobapp.azurewebsites.net");




        private bool validate()
        {
            if (string.IsNullOrEmpty(AccountName.Text))
            {
                DisplayAlert("Account", "Please Enter Account Name!", "Ok");
                return false;
            }
            else if (string.IsNullOrEmpty(Bank.Text))
            {
                DisplayAlert("Account", "Please Enter Bank!", "Ok");
                return false;
            }
            else if (string.IsNullOrEmpty(AccountNumber.Text))
            {
                DisplayAlert("Account", "Please Enter AccountNumber!", "Ok");
                return false;
            }




            else
            {
                return true;
            }
        }







        void autofill()
        {

            AccountName.Text = tempdata.uAccountName;
            Bank.Text = tempdata.uBank;
            AccountNumber.Text = tempdata.uAccountNumber;
            Branch.Text = tempdata.uBranch;
            Info.Text = tempdata.uInfo;



        }





        private async void updatebtn(object sender, EventArgs e)
        {


            if (validate()) //validate()
            {
                // apiCall();
                this.IsBusy = true;
                AddAccount.IsEnabled = false;


                try
                {
                    BankAcount acc = new BankAcount
                    {
                        AccountNumber = AccountNumber.Text,
                        AccountName = AccountName.Text,

                        Bank = Bank.Text,
                        Branch = Branch.Text,
                        Info = Info.Text,
                        ServiceProviderId = tempdata.Loginas,
                        Id = tempdata.uId,






                    };

                    await MobileService.GetTable<BankAcount>().UpdateAsync(acc); //.InsertAsync(acc);
                    

                    this.IsBusy = false;
                    AddAccount.IsEnabled = true;

                    await DisplayAlert("Successful", "Your Account updated", "Ok");

                    await Navigation.PushAsync(new BankAccount(), true);



                }
                catch (Exception ee)

                {
                    Debug.WriteLine("" + ee);
                    await DisplayAlert("fail..!", "Connection erro", "Ok");
                }






            }



        }
    }
}