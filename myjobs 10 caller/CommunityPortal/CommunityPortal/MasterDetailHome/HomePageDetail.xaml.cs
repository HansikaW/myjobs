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
                IMobileServiceTable<CallerTaskRecord> dsin = client.GetTable<CallerTaskRecord>();

                IMobileServiceTableQuery<string> query = dsin
                .Where(ur => ur.callerID == tempdata.Loginas)
               .Select(ur => ur.pholdername);

                List<string> listin = await query.ToListAsync();


               


                
                
                qno.Text = Convert.ToString(listin.Count);
               




            }
            catch (Exception e)
            {

                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }



        }




    }
}