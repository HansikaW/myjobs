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
	public partial class WorkOrders : ContentPage
	{
		public WorkOrders ()
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



                IMobileServiceTableQuery<string> query = DataTable
                .Where(ur => ur.ServiceProviderID != tempdata.Loginas)
               .Select(ur => ur.WorkOrderSubject);

                List<string> items = await query.ToListAsync();
                workorders.ItemsSource = items;

                // finalname.Text = string.Format("{0}-{1}", items2[0].FirstName, items2[0].LastName);




            }
            catch (Exception e)
            {

                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }



        }

        private async void Select_workorder(object sender, SelectedItemChangedEventArgs e)
        {
            tempdata.selected_WorkOrderSubject = (string)workorders.SelectedItem;

            await Navigation.PushAsync(new showSelectedWorkOrder(), true);
        }
    }
}