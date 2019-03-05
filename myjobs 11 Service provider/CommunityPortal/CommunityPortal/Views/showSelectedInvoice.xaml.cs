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
                .Where(ur => ur.ServiceProvideId == tempdata.Loginas && ur.InvoiceSubject == tempdata.selected_InvoiceSubject);
              // .Select(todoItem => todoItem.InvoiceSubject);

                List<Invoice> items = await query.ToListAsync();
               // EmployeeView.ItemsSource = items2;

                // finalname.Text = string.Format("{0}-{1}", items2[0].FirstName, items2[0].LastName);

                lb1.Text = string.Format("{0} :- {1}", "Subject", items[0].InvoiceSubject);
                lb2.Text = string.Format("{0} :- {1}", "WorkPlace", items[0].WorkPlace);
                lb3.Text = string.Format("{0} :- {1}",  "Description", items[0].Description);
                lb4.Text = string.Format("{0} :- {1}", "Service Fee", items[0].ServiceFee);
                lb5.Text = string.Format("{0} :- {1}", "Other Fee", items[0].OtherFee);
                lb6.Text = string.Format("{0} :- {1}", "Total", items[0].Total);


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



               
                lb7.Text = string.Format("{0} :- {1}", "Payment Status", st);
                






            }
            catch (Exception e)
            {

                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }



        }



        /*
         
         axml ui
          <Grid VerticalOptions="CenterAndExpand">
                <Grid.RowDefinitions>
                    <RowDefinition Height="*" />
                    <RowDefinition Height="*" />
                </Grid.RowDefinitions>
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="4*" />
                    <ColumnDefinition Width="5*" />
                </Grid.ColumnDefinitions>
                <Label x:Name="1" Text="Top Left" Grid.Row="0" Grid.Column="0" />
                <Label  x:Name="11" Text="Top Right" Grid.Row="0" Grid.Column="1" />
                <Label x:Name="2" Text="Bottom Left" Grid.Row="1" Grid.Column="0" />
                <Label  x:Name="22" Text="Bottom Right" Grid.Row="1" Grid.Column="1" />
                <Label   x:Name="3" Text="Top Right" Grid.Row="0" Grid.Column="1" />
                <Label  x:Name="33" Text="Bottom Left" Grid.Row="1" Grid.Column="0" />
                <Label  x:Name="4" x:Name="1"  Text="Bottom Right" Grid.Row="1" Grid.Column="1" />
                <Label  x:Name="44" x:Name="1"  Text="Bottom Right" Grid.Row="1" Grid.Column="1" />
                <Label x:Name="1"  Text="Bottom Right" Grid.Row="1" Grid.Column="1" />
                <Label x:Name="1"  Text="Bottom Right" Grid.Row="1" Grid.Column="1" />
                <Label x:Name="1"  Text="Bottom Right" Grid.Row="1" Grid.Column="1" />
                <Label x:Name="1"  Text="Bottom Right" Grid.Row="1" Grid.Column="1" />




            </Grid>



         
         */






    }
}