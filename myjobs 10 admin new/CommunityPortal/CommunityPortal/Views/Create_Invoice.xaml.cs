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

        
        }

        public static MobileServiceClient MobileService =
          new MobileServiceClient("https://myjobapps.azurewebsites.net");




        private bool validate()
        {
            if (string.IsNullOrEmpty(Subject.Text))
            {
                DisplayAlert("Login", "Please Enter Subject!", "Ok");
                return false;
            }
            else if (string.IsNullOrEmpty(Description.Text))
            {
                DisplayAlert("Login", "Please Enter Description!", "Ok");
                return false;
            }
            else if (string.IsNullOrEmpty(ServiceFee.Text))
            {
                DisplayAlert("Login", "Please Enter Service Fee!", "Ok");
                return false;
            }

            else if (string.IsNullOrEmpty(WorkPlace.Text))
            {
                DisplayAlert("Login", "Please Enter Work Place!", "Ok");
                return false;
            }
           

            else
            {
                return true;
            }
        }




        void valchange() {


            try {

                float servicefee = Convert.ToSingle(ServiceFee.Text);
                float OtherFees = Convert.ToSingle(OtherFee.Text);
                float total = servicefee + OtherFees;
                Total.Text = Convert.ToString(total);


            }
            catch (Exception e) {


                if (e.Message == null)
                {


                }
                else {
                    DisplayAlert("Invalid price!", "Please enter correct price ", "Ok");
                    ServiceFee.Text = "";
                    OtherFee.Text = "";
                    Total.Text = "";

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
                    Invoice newinvoice = new Invoice
                    {
                        InvoiceSubject = Subject.Text,
                        Description = Description.Text,
                       
                        ServiceFee = Convert.ToSingle(ServiceFee.Text),
                        Total = Convert.ToSingle(Total.Text),
                        InvoiceStatus = 0, // pending
                        WorkPlace = WorkPlace.Text,
                        OtherFee = Convert.ToSingle(OtherFee.Text),


                        ServiceProvideId = tempdata.Loginas,





                    };

                    await MobileService.GetTable<Invoice>().InsertAsync(newinvoice);

                    this.IsBusy = false;
                    SInvoice.IsEnabled = true;

                    await DisplayAlert("Sent", "Your Invoice sent", "Ok");



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