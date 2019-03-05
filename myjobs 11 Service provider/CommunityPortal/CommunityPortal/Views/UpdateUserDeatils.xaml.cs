using CommunityPortal.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CommunityPortal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateUserDeatils : ContentPage
    {
        public UpdateUserDeatils()
        {
            InitializeComponent();
           
            GetdataAsync();



        }


        public static MobileServiceClient client = new MobileServiceClient("https://myjobapps.azurewebsites.net");


        IMobileServiceTable<UserReg> DataTable = client.GetTable<UserReg>();





        private bool validate()
        {
            if (string.IsNullOrEmpty(FirstName.Text))
            {
                DisplayAlert("Register", "Please Enter FirstName!", "Ok");
                return false;
            }
            else if (string.IsNullOrEmpty(LastName.Text))
            {
                DisplayAlert("Register", "Please Enter LastName!", "Ok");
                return false;
            }
            else if (string.IsNullOrEmpty(Email.Text))
            {
                DisplayAlert("Register", "Please Enter Email!", "Ok");
                return false;
            }
            /*
            else if (!IsPhoneNumber(PhoneNumber.Text))
            {
                DisplayAlert("Register", "Please Enter valid TP no!", "Ok");
                return false;
            }
            */

            else if (string.IsNullOrEmpty(PhoneNumber.Text))
            {
                DisplayAlert("Register", "Please Enter PhoneNumber!", "Ok");
                return false;
            }
            else if (1 == 1)
            {

                try
                {
                    MailAddress m = new MailAddress(Email.Text);
                    return true;
                }
                catch (FormatException)
                {
                    DisplayAlert("Login", "Please Enter valid Email", "Ok");
                    return false;
                }


            }

            else
            {
                return true;
            }
        }


       
        string pass;

        public async Task GetdataAsync()
        {
            try
            {


                IMobileServiceTableQuery<UserReg> query = DataTable
                .Where(ur => ur.Email == tempdata.Loginas);
                // .Select(todoItem => todoItem.InvoiceSubject);

                List<UserReg> items = await query.ToListAsync();



                FirstName.Text =  items[0].FirstName;
                LastName.Text = items[0].LastName;
                Email.Text = items[0].Email;
                Address.Text = items[0].Address;
                PhoneNumber.Text = items[0].PhoneNumber;
                ServiceType.SelectedItem = Convert.ToString(items[0].ServiceType);
                tempdata.uId2 = items[0].Id;
                tempdata.pass = items[0].Password;
















            }
            catch (Exception e)
            {

                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }



        }



        




        private async void updatebtn(object sender, EventArgs e)
        {


            if (validate()) //validate()
            {
                // apiCall();
                this.IsBusy = true;
           

                try
                {
                    UserReg acc = new UserReg
                    {
                        Id = tempdata.uId2,
                        FirstName = FirstName.Text,
                        LastName = LastName.Text,
                        
                        Email = Email.Text,
                        Address = Address.Text,
                        PhoneNumber = PhoneNumber.Text,                      
                        ServiceType = (String)ServiceType.SelectedItem,
                        Password = tempdata.pass,
                        


                    };

                    await client.GetTable<UserReg>().UpdateAsync(acc); //.InsertAsync(acc);


                    this.IsBusy = false;
                 

                    await DisplayAlert("Successful", "Your details updated", "Ok");
                    await Navigation.PushAsync(new UserDetailsPage(), true);




                }
                catch (Exception ee)

                {
                    Debug.WriteLine("" + ee);
                    await DisplayAlert("fail..!", "Connection erro", "Ok");
                }






            }



        }
    }
}