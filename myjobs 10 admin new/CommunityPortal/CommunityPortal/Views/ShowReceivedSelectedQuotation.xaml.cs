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











        string uqid;
        string uQuotationSubject;
        string uJobDescription;
        float uEstimatedServiceFee;
        string uWorkPlace;
        string uServiceProviderId;
        string uservicetype;




        public async Task GetdataAsync()
        {
            try
            {


                IMobileServiceTableQuery<Quotation> query = DataTable
                .Where(ur => ur.ServiceProviderId != tempdata.Loginas && ur.QuotationSubject == tempdata.selected_QuotationSubject);
                // .Select(todoItem => todoItem.InvoiceSubject);

                List<Quotation> items = await query.ToListAsync();
                // EmployeeView.ItemsSource = items2;

                // finalname.Text = string.Format("{0}-{1}", items2[0].FirstName, items2[0].LastName);

                uQuotationSubject = items[0].QuotationSubject;
                  uqid = items[0].Id;
                uQuotationSubject = items[0].QuotationSubject;
                uJobDescription = items[0].JobDescription;
                uEstimatedServiceFee = items[0].EstimatedServiceFee;
                uWorkPlace = items[0].WorkPlace;
                uServiceProviderId = items[0].ServiceProviderId;
                uservicetype = items[0].servicetype;


                lb1.Text = string.Format("{0} :- {1}", "Subject", items[0].QuotationSubject);
                lb2.Text = string.Format("{0} :- {1}", "Service provider", items[0].ServiceProviderId);
                lb3.Text = string.Format("{0} :- {1}", "WorkPlace", items[0].WorkPlace);
                lb4.Text = string.Format("{0} :- {1}", "Job Description", items[0].JobDescription);
                lb5.Text = string.Format("{0} :- {1}", "Estimated Service Fee", items[0].EstimatedServiceFee);
                lb6.Text = string.Format("{0} :- {1}", "Estimated Other Fee", items[0].EstimatedOtherFee);

                //lb7.Text = string.Format("{0} :- {1}", "EstimatedTotal", items[0].EstimatedTotal);


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

                




                tempdata.QuotationSubject = items[0].QuotationSubject;
               // tempdata.RefferenceId = items[0].RefferenceId;
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

       
        private async void rejectbtn(object sender, EventArgs e)
        {


            try
            {
                Quotation acc = new Quotation
                {

                    Id = uqid,
                    QuotationStatus = 2,


                    QuotationSubject = uQuotationSubject,
               
                   JobDescription = uJobDescription,
                   EstimatedServiceFee = uEstimatedServiceFee, 
                      WorkPlace =  uWorkPlace,
                     ServiceProviderId =  uServiceProviderId,
                    servicetype =  uservicetype,



            };

                await client.GetTable<Quotation>().UpdateAsync(acc); //.InsertAsync(acc);


                this.IsBusy = false;

                await DisplayAlert("Reject", "You Reject this request", "Ok");

                await Navigation.PushAsync(new QuotationTabbedPage(), true);



            }
            catch (Exception ee)

            {
                Debug.WriteLine("" + ee);
                await DisplayAlert("fail..!", "Connection erro", "Ok");
            }


        }

        private async void Acceptbtn1(object sender, EventArgs e)
        {
            try
            {
                Quotation acc = new Quotation
                {

                    Id = uqid,
                    QuotationStatus = 1,



                    QuotationSubject = uQuotationSubject,

                    JobDescription = uJobDescription,
                    EstimatedServiceFee = uEstimatedServiceFee,
                    WorkPlace = uWorkPlace,
                    ServiceProviderId = uServiceProviderId,
                    
                    servicetype = uservicetype,




                };

                await client.GetTable<Quotation>().UpdateAsync(acc); //.InsertAsync(acc);

                tempdata.WoSubject = uQuotationSubject;
                tempdata.WoServiceProviderId = uServiceProviderId;
                tempdata.WoWorkplace = uWorkPlace;

                this.IsBusy = false;

                await DisplayAlert("Accept", "Your Accept this request", "Ok");

                await Navigation.PushAsync(new SendWorkOrder(), true);



            }
            catch (Exception ee)

            {
                Debug.WriteLine("" + ee);
                await DisplayAlert("fail..!", "Connection erro", "Ok");
            }
        }
    }
}