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
    public partial class QuotationTabbedPage : TabbedPage
    {
        public QuotationTabbedPage ()
        {
            InitializeComponent();
             GetdataAsync();
        }


        public static MobileServiceClient client = new MobileServiceClient("https://myjobapp.azurewebsites.net");

        IMobileServiceTable<Quotation> DataTable = client.GetTable<Quotation>();


        public async Task GetdataAsync()
        {
            try
            {


                IMobileServiceTableQuery<string> query = DataTable
                .Where(ur => ur.ServiceProviderId == tempdata.Loginas)
                .Select(ur => ur.QuotationSubject);

                List<string> items = await query.ToListAsync();
                SQuotationView.ItemsSource = items;

                //finalname.Text = string.Format("{0}-{1}", items2[0].FirstName, items2[0].LastName);
//----------------------------------------------------------------
                




                 IMobileServiceTableQuery<string> query2 = DataTable
                .Where(ur => ur.ServiceProviderId == tempdata.received_quotationID)
                .Select(ur => ur.QuotationSubject);

                List<string> items2 = await query2.ToListAsync();
                RQuotationView.ItemsSource = items2;











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





        private async void Select_Receiced_Quotation(object sender, SelectedItemChangedEventArgs e)
        {
            tempdata.selected_QuotationSubject = (string)RQuotationView.SelectedItem;

            await Navigation.PushAsync(new ShowReceivedSelectedQuotation(), true);

        }


        private async void Select_Quotation(object sender, SelectedItemChangedEventArgs e)
        {
            tempdata.selected_QuotationSubject = (string)SQuotationView.SelectedItem;

            await Navigation.PushAsync(new ShowSelectedQuotation(), true);
        }
    }
}