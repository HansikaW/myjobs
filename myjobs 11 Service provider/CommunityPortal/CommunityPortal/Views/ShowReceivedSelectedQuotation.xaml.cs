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
	public partial class ShowReceivedSelectedQuotation : ContentPage
	{
		public ShowReceivedSelectedQuotation ()
		{
			InitializeComponent ();
            GetdataAsync();

        }




        public static MobileServiceClient client = new MobileServiceClient("https://myjobapps.azurewebsites.net");


        IMobileServiceTable<Quotation> DataTable = client.GetTable<Quotation>();















        public async Task GetdataAsync()
        {
            try
            {


                IMobileServiceTableQuery<Quotation> query = DataTable
                .Where(ur => ur.ServiceProviderId == tempdata.received_quotationID && ur.QuotationSubject == tempdata.selected_QuotationSubject);
                // .Select(todoItem => todoItem.InvoiceSubject);

                List<Quotation> items = await query.ToListAsync();
                // EmployeeView.ItemsSource = items2;

                // finalname.Text = string.Format("{0}-{1}", items2[0].FirstName, items2[0].LastName);

                lb1.Text = string.Format("{0} :- {1}", "Subject", items[0].QuotationSubject);
              //  lb2.Text = string.Format("{0} :- {1}", "Refference Id", items[0].RefferenceId);
                lb2.Text = string.Format("{0} :- {1}", "WorkPlace", items[0].WorkPlace);
                lb3.Text = string.Format("{0} :- {1}", "Job Description", items[0].JobDescription);
                lb4.Text = string.Format("{0} :- {1}", "Estimated Service Fee", items[0].EstimatedServiceFee);
                lb5.Text = string.Format("{0} :- {1}", "Due Date", items[0].Duedate);

                //lb5.Text = string.Format("{0} :- {1}", "EstimatedTotal", items[0].EstimatedTotal);
            //    lb8.Text = string.Format("{0} :- {1}", "Quotation Status", items[0].QuotationStatus);




                tempdata.QuotationSubject = items[0].QuotationSubject;
                tempdata.RefferenceId = items[0].RefferenceId;
                tempdata.WorkPlace = items[0].WorkPlace;


            }
            catch (Exception e)
            {

                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }



        }

        private async void Create_Quotation(object sender, EventArgs e)
        {          
            await Navigation.PushAsync(new SendQuotation(), true);
        }
    }
}