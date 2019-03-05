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
	public partial class showSelectedInvoice : ContentPage
	{
		public showSelectedInvoice ()
		{
			InitializeComponent ();
         
            GetdataAsync();

        }


        public static MobileServiceClient client = new MobileServiceClient("https://myjobapps.azurewebsites.net");


        IMobileServiceTable<Invoice> DataTable = client.GetTable<Invoice>();











        string a;
        string b;
        string c;
        float d;
        float ee;
        float f;
        string ids;
        int g;
        string i;


        public async Task GetdataAsync()
        {
            try
            {


                /*
                 
                List<UserReg> items = await DataTable
                    .Where(ur => ur.FirstName != null)
                    .ToListAsync();


                //IEnumerable itemsControl = items;
                EmployeeView.ItemsSource = items;

                */


                IMobileServiceTableQuery<Invoice> query = DataTable
                .Where(ur => ur.ServiceProvideId != tempdata.Loginas && ur.InvoiceSubject == tempdata.selected_InvoiceSubject);
              // .Select(todoItem => todoItem.InvoiceSubject);

                List<Invoice> items = await query.ToListAsync();
               // EmployeeView.ItemsSource = items2;

                // finalname.Text = string.Format("{0}-{1}", items2[0].FirstName, items2[0].LastName);

                lb1.Text = string.Format("{0} :- {1}", "Subject", items[0].InvoiceSubject);
                lb2.Text = string.Format("{0} :- {1}", "Description", items[0].Description);
                lb3.Text = string.Format("{0} :- {1}", "WorkPlace", items[0].WorkPlace);
                lb4.Text = string.Format("{0} :- {1}", "Service Fee", items[0].ServiceFee);
                lb5.Text = string.Format("{0} :- {1}", "Other Fee", items[0].OtherFee);
                lb6.Text = string.Format("{0} :- {1}", "Total", items[0].Total); 
                lb7.Text = string.Format("{0} :- {1}", "Invoice Status", items[0].InvoiceStatus);

                a = items[0].InvoiceSubject;
                b = items[0].Description;
                c = items[0].WorkPlace;
                d = items[0].ServiceFee;
                ee = items[0].OtherFee;
                f = items[0].Total;
                ids = items[0].Id;
                g = items[0].InvoiceStatus;
                i = items[0].ServiceProvideId;

                string st;
                if (items[0].InvoiceStatus == 0)
                {
                    st = "Pending";
                }
                else if (items[0].InvoiceStatus == 1)
                {

                    st = "Payment Accepted";
                }
                else {
                    st = "Payment Rejected";

                }



               
                lb8.Text = string.Format("{0} :- {1}", "Payment Status", st);
                






            }
            catch (Exception e)
            {

                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }



        }

        private async void paybtn(object sender, EventArgs e)
        {



            try
            {
                Invoice acc = new Invoice
                {

                    Id = ids,
                    InvoiceStatus = 1,
                    ServiceProvideId = i,

                    InvoiceSubject = a,

                    Description = b,
                    ServiceFee = d,
                    WorkPlace = c,
                    OtherFee = ee,
                    Total = f,



                };

                await client.GetTable<Invoice>().UpdateAsync(acc); //.InsertAsync(acc);


                this.IsBusy = false;

                await DisplayAlert("Paid", "This request mark as Paid", "Ok");

                await Navigation.PushAsync(new InvoicePage(), true);



            }
            catch (Exception ee)

            {
                Debug.WriteLine("" + ee);
                await DisplayAlert("fail..!", "Connection erro", "Ok");
            }






        }

        private async void rejectbtn(object sender, EventArgs e)
        {


            try
            {
                Invoice acc = new Invoice
                {

                    Id = ids,
                    InvoiceStatus = 2,


                    InvoiceSubject = a,

                    Description = b,
                    ServiceFee = d,
                    WorkPlace = c,
                    OtherFee = ee,
                    Total = f,



                };

                await client.GetTable<Invoice>().UpdateAsync(acc); //.InsertAsync(acc);


                this.IsBusy = false;

                await DisplayAlert("Rejected", "This request mark as Reject", "Ok");

                await Navigation.PushAsync(new InvoicePage(), true);



            }
            catch (Exception ee)

            {
                Debug.WriteLine("" + ee);
                await DisplayAlert("fail..!", "Connection erro", "Ok");
            }


        }
      

    }
}