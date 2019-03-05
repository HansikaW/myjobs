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
	public partial class showSelectedWorkOrder : ContentPage
	{
		public showSelectedWorkOrder ()
		{
			InitializeComponent ();

            GetdataAsync();

        }


        public static MobileServiceClient client = new MobileServiceClient("https://myjobapps.azurewebsites.net");


        IMobileServiceTable<WorkOrder> DataTable = client.GetTable<WorkOrder>();















        public async Task GetdataAsync()
        {
            try
            {
            

                IMobileServiceTableQuery<WorkOrder> query = DataTable
                .Where(ur => ur.ServiceProviderID != tempdata.Loginas && ur.WorkOrderSubject == tempdata.selected_WorkOrderSubject);
                // .Select(todoItem => todoItem.InvoiceSubject);

                List<WorkOrder> items = await query.ToListAsync();
                // EmployeeView.ItemsSource = items2;

                // finalname.Text = string.Format("{0}-{1}", items2[0].FirstName, items2[0].LastName);

                lb1.Text = string.Format("{0} :- {1}", "Subject", items[0].WorkOrderSubject);
                lb2.Text = string.Format("{0} :- {1}", "Service provider", items[0].ServiceProviderID);
                lb3.Text = string.Format("{0} :- {1}", "DueDate", items[0].DueDate);
                lb4.Text = string.Format("{0} :- {1}", "Workplace", items[0].Workplace);
                lb5.Text = string.Format("{0} :- {1}", "Description", items[0].Description);










            }
            catch (Exception e)
            {

                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }



        }

        private void doneworkbtn(object sender, EventArgs e)
        {

        }
    }
}