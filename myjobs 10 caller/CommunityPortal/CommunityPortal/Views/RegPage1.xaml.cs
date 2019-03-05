using CommunityPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CommunityPortal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegPage1 : ContentPage
    {
        public RegPage1()
        {
            InitializeComponent();
        }






        private bool Phoneno(string ph)
        {

            try
            {

                Convert.ToInt32(ph);
                return true;
            }
            catch (Exception e)
            {

                return false;

            }

        }
         




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
            else if (!Phoneno(PhoneNumber.Text))
            {
                DisplayAlert("Register", "Please Enter valid Phone Number!", "Ok");
                return false;
            }




            else if (1==1)
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

       

        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
        }




        private async void NextBtn(object sender, EventArgs e)
        {

            if (validate()) {

                tempdata.FirstName = FirstName.Text;
                tempdata.LastName = LastName.Text;
                tempdata.Email = Email.Text;              
                tempdata.PhoneNumber = PhoneNumber.Text;



                await Navigation.PushAsync(new RegPage2(), true);


            }


           
        }
    }
}