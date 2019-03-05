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
	public partial class SendQuotation : ContentPage
	{
		public SendQuotation ()
		{
			InitializeComponent ();
            autofill();

        }

        public static MobileServiceClient MobileService =
          new MobileServiceClient("https://myjobapp.azurewebsites.net");


       void autofill() {

            Subject.Text = tempdata.QuotationSubject;
            RefferenceId.Text = tempdata.RefferenceId;
            WorkPlace.Text = tempdata.WorkPlace;
        }



        private bool validate()
        {
            if (string.IsNullOrEmpty(Subject.Text))
            {
                DisplayAlert("Quotation", "Please Enter Subject!", "Ok");
                return false;
            }
            else if (string.IsNullOrEmpty(Description.Text))
            {
                DisplayAlert("Quotation", "Please Enter Description!", "Ok");
                return false;
            }
            else if (string.IsNullOrEmpty(EstimatedServiceFee.Text))
            {
                DisplayAlert("Quotation", "Please Enter Estimated Service Fee !", "Ok");
                return false;
            }

            else if (string.IsNullOrEmpty(WorkPlace.Text))
            {
                DisplayAlert("Quotation", "Please Enter Work Place!", "Ok");
                return false;
            }
            else if (string.IsNullOrEmpty(RefferenceId.Text))
            {
                DisplayAlert("Quotation", "Please Enter Refference Id.Text", "Ok");
                return false;
            }
          

            else
            {
                return true;
            }
        }




        private async void Send_Quotation(object sender, EventArgs e)
        {

            if (validate()) //validate()
            {
                // apiCall();
                this.IsBusy = true;
                sendQuotation.IsEnabled = false;


                try
                {
                    Quotation newQuotation = new Quotation
                    {
                        QuotationSubject = Subject.Text,
                        RefferenceId = RefferenceId.Text,
                        JobDescription = Description.Text,


                        EstimatedServiceFee = Convert.ToSingle(EstimatedServiceFee.Text),
                        EstimatedTotal = Convert.ToSingle(EstimatedTotal.Text),
                        QuotationStatus = 0,
                        WorkPlace = WorkPlace.Text,
                        EstimatedOtherFee = Convert.ToSingle(EstimatedOtherFee.Text),

                        ServiceProviderId = tempdata.Loginas,





                    };

                    await MobileService.GetTable<Quotation>().InsertAsync(newQuotation);

                    this.IsBusy = false;
                    sendQuotation.IsEnabled = true;

                    await DisplayAlert("Sent", "Your Quotation sent", "Ok");



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


        void valchange()
        {


            try
            {

                float servicefee = Convert.ToSingle(EstimatedServiceFee.Text);
                float OtherFees = Convert.ToSingle(EstimatedOtherFee.Text);
                float total = servicefee + OtherFees;
                EstimatedTotal.Text = Convert.ToString(total);


            }
            catch (Exception e)
            {


                if (e.Message == null)
                {


                }
                else
                {
                    DisplayAlert("Invalid price!", "Please enter correct price ", "Ok");
                    EstimatedServiceFee.Text = "";
                    EstimatedOtherFee.Text = "";
                    EstimatedTotal.Text = "";

                }




            }



        }


    }
}