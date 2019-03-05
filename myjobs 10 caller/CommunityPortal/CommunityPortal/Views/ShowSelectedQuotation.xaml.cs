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
	public partial class ShowSelectedQuotation : ContentPage
	{
		public ShowSelectedQuotation ()
		{
			InitializeComponent ();
            GetdataAsync();

        }




        public static MobileServiceClient client = new MobileServiceClient("https://myjobapp.azurewebsites.net");


        IMobileServiceTable<Quotation> DataTable = client.GetTable<Quotation>();















        public async Task GetdataAsync()
        {
            try
            {


                IMobileServiceTableQuery<Quotation> query = DataTable
                .Where(ur => ur.ServiceProviderId == tempdata.Loginas && ur.QuotationSubject == tempdata.selected_QuotationSubject);
                // .Select(todoItem => todoItem.InvoiceSubject);

                List<Quotation> items = await query.ToListAsync();
                // EmployeeView.ItemsSource = items2;

                // finalname.Text = string.Format("{0}-{1}", items2[0].FirstName, items2[0].LastName);

                lb1.Text = string.Format("{0} :- {1}", "Subject", items[0].QuotationSubject);
                lb2.Text = string.Format("{0} :- {1}", "Refference Id", items[0].RefferenceId);
                lb6.Text = string.Format("{0} :- {1}", "WorkPlace", items[0].WorkPlace);
                lb3.Text = string.Format("{0} :- {1}", "Job Description", items[0].JobDescription);
                lb4.Text = string.Format("{0} :- {1}", "Estimated Service Fee", items[0].EstimatedServiceFee);
                lb5.Text = string.Format("{0} :- {1}", "Estimated Other Fee", items[0].EstimatedOtherFee);

                lb7.Text = string.Format("{0} :- {1}", "EstimatedTotal", items[0].EstimatedTotal);

                string st;
                if (items[0].QuotationStatus == 0)
                {
                    st = "Pending";
                }
                else if (items[0].QuotationStatus == 1)
                {

                    st = "Accepted";
                }
                else
                {
                    st = "Rejected";

                }





                lb8.Text = string.Format("{0} :- {1}", "Quotation Status", st);







            }
            catch (Exception e)
            {

                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }



        }


    }
}