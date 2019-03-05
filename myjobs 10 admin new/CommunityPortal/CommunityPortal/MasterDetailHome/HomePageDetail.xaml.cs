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

namespace CommunityPortal.MasterDetailHome
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageDetail : ContentPage
    {
        public HomePageDetail()
        {
            InitializeComponent();
            nameis.Text = tempdata.Loginas;
            GetdataAsync();
        }

        public static MobileServiceClient client = new MobileServiceClient("https://myjobapps.azurewebsites.net");

        IMobileServiceTable<Invoice> DataTable = client.GetTable<Invoice>(); 


        public async Task GetdataAsync()
        {
            try
            {
                IMobileServiceTable<Invoice> dsin = client.GetTable<Invoice>();

                IMobileServiceTableQuery<string> query = dsin
                .Where(ur => ur.ServiceProvideId != tempdata.Loginas)
               .Select(ur => ur.InvoiceSubject);

                List<string> listin = await query.ToListAsync();


                //-- paind invoice--
                IMobileServiceTable<Invoice> dsinp = client.GetTable<Invoice>();

                IMobileServiceTableQuery<string> query4 = dsin
                .Where(ur => ur.ServiceProvideId == tempdata.Loginas & ur.InvoiceStatus==1 )
               .Select(ur => ur.InvoiceSubject);

                List<string> listinp = await query4.ToListAsync();
                //------------

                IMobileServiceTable<Quotation> dsqu = client.GetTable<Quotation>();

                IMobileServiceTableQuery<string> query2 = dsqu
               .Where(ur => ur.ServiceProviderId == tempdata.received_quotationID)
               .Select(ur => ur.QuotationSubject);

                List<string> ilistqu = await query2.ToListAsync();

                // --------------------

                IMobileServiceTable<WorkOrder> dswork = client.GetTable<WorkOrder>();

                IMobileServiceTableQuery<string> query3 = dswork
                .Where(ur => ur.ServiceProviderID != tempdata.Loginas)
               .Select(ur => ur.WorkOrderSubject);

                List<string> listwkd = await query3.ToListAsync();


               /* 
                qno.Text = Convert.ToString(ilistqu.Count);
                inv.Text = Convert.ToString(listin.Count);
                wod.Text = Convert.ToString(listwkd.Count);
                pinv.Text = Convert.ToString(listinp.Count);
                */




            }
            catch (Exception e)
            {

                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }



        }




    }
}