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
	public partial class SendWorkOrder : ContentPage
	{
		public SendWorkOrder ()
		{
			InitializeComponent ();
            DueDate.MinimumDate = System.DateTime.Today;


            autofill();

        }

        public static MobileServiceClient MobileService =
          new MobileServiceClient("https://myjobapps.azurewebsites.net");


        void autofill()
        {

            Subject.Text = tempdata.WoSubject;
            ServiceProviderID.Text = tempdata.WoServiceProviderId;
            Workplace.Text = tempdata.WoWorkplace;
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
         

            else if (string.IsNullOrEmpty(Workplace.Text))
            {
                DisplayAlert("Quotation", "Please Enter Work Place!", "Ok");
                return false;
            }



            else
            {
                return true;
            }
        }


 

        private async void sendOrder(object sender, EventArgs e)
        {

            if (validate()) //validate()
            {
                // apiCall();
                this.IsBusy = true;


                try
                {
                    WorkOrder www = new WorkOrder{
                        ServiceProviderID = ServiceProviderID.Text,

                        Description = Description.Text,


                        WorkOrderSubject = Subject.Text,
                        DueDate = DueDate.Date,
                        Workplace = Workplace.Text,

                    };

                    await MobileService.GetTable<WorkOrder>().InsertAsync(www);

                    this.IsBusy = false;

                    await DisplayAlert("Sent", "Your Workorder sent", "Ok");
                    await Navigation.PushAsync(new WorkOrders(), true);



                }
                catch (Exception ee)
                { Debug.WriteLine("" + ee); }






            }

        }
    }
}