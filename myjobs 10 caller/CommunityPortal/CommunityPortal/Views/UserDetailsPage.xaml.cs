using CommunityPortal.Models;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CommunityPortal.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserDetailsPage : ContentPage
	{
        private User user;
        private User receivedUser;
        private const string url = "https://testapi01-vt5.conveyor.cloud/api/user";
        private HttpClient httpClient = new HttpClient();

        public UserDetailsPage ()
		{
			InitializeComponent ();
            GetdataAsync();

        }


        public static MobileServiceClient client = new MobileServiceClient("https://myjobapp.azurewebsites.net");


        IMobileServiceTable<UserReg> DataTable = client.GetTable<UserReg>();


        public async Task GetdataAsync()
        {
            try
            {


                IMobileServiceTableQuery<UserReg> query = DataTable
                .Where(ur => ur.Email == tempdata.Loginas);
                // .Select(todoItem => todoItem.InvoiceSubject);

                List<UserReg> items = await query.ToListAsync();
             
               

                name.Text = string.Format("{0} {1}", items[0].FirstName, items[0].LastName);
                Email.Text = items[0].Email;
                Address.Text = items[0].Address;
                PhoneNumber.Text = items[0].PhoneNumber;
                ServiceType1.Text =  Convert.ToString(items[0].ServiceType);












            }
            catch (Exception e)
            {

                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }



        }

        private void doneworkbtn(object sender, EventArgs e)
        {

        }

        private void RegBtn1_Clicked(object sender, EventArgs e)
        {

        }

        private void RegBtn2_Clicked(object sender, EventArgs e)
        {

        }
    }
}