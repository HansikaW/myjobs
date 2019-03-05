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
	public partial class Create_Invoice : ContentPage
	{
		public Create_Invoice ()
		{
			InitializeComponent ();
            datepic.MinimumDate = System.DateTime.Today;
        }

        public static MobileServiceClient MobileService =
          new MobileServiceClient("https://myjobapps.azurewebsites.net");






        private bool Phoneno(string ph)
        {

            try
            {

                Convert.ToInt32(ph);
                return true;
            }
            catch (Exception e)
            {

                return false;

            }

        }


        private bool validate()
        {
            if (string.IsNullOrEmpty(pholder.Text))
            {
                DisplayAlert("Fail", "Please Enter holder!", "Ok");
                return false;
            }
            else if (string.IsNullOrEmpty(Description1.Text))
            {
                DisplayAlert("Fail", "Please Enter Description!", "Ok");
                return false;
            }
            else if (string.IsNullOrEmpty(Address1.Text))
            {
                DisplayAlert("Fail", "Please Enter Address!", "Ok");
                return false;
            }

            else if (string.IsNullOrEmpty(phoneno.Text))
            {
                DisplayAlert("Fail", "Please Enter phone no!", "Ok");
                return false;
            }

            else if (!Phoneno(phoneno.Text))
            {
                DisplayAlert("Register", "Please Enter valid Phone Number!", "Ok");
                return false;
            }





            else if (string.IsNullOrEmpty(Budget.Text))
            {
                DisplayAlert("Fail", "Please Enter Budget!", "Ok");
                return false;
            }

            else
            {
                return true;
            }
        }




        void valchange() {


            try {

                float esbudget = Convert.ToSingle(Budget.Text);
             

            }
            catch (Exception e) {


                if (e.Message == null)
                {


                }
                else {
                    DisplayAlert("Invalid price!", "Please enter correct price ", "Ok");
                    Budget.Text = "";
                   

                }

               


            }

           

        }

        private async void Send_Invoicebtn(object sender, EventArgs e)
        {

           
            if (validate()) //validate()
            {
                // apiCall();
                this.IsBusy = true;
                SInvoice.IsEnabled = false;


                try
                {
                    CallerTaskRecord newinvoice = new CallerTaskRecord
                    {
                        pholdername = pholder.Text,
                        Address = Address1.Text,
                        ServiceType =  Convert.ToString(ServiceType1.SelectedItem),
                        PhoneNumber = phoneno.Text,
                        Description = Description1.Text,
                        Budget = Convert.ToSingle(Budget.Text),
                        Duedate = datepic.Date,
                        
                        callerID = tempdata.Loginas,
                        AssignStatus = "pending",




    };

                    await MobileService.GetTable<CallerTaskRecord>().InsertAsync(newinvoice);

                    this.IsBusy = false;
                    SInvoice.IsEnabled = true;

                    await DisplayAlert("Sent", "Your Task added", "Ok");
                    await Navigation.PushAsync(new InvoicePage(), true);



                }
                catch (Exception ee)
                { Debug.WriteLine("" + ee); }






            }





        }







        private void servicefesschanged(object sender, TextChangedEventArgs e)
        {
            valchange();
        }

        private void otherfesschanged(object sender, TextChangedEventArgs e)
        {
            valchange();
        }
    }
}