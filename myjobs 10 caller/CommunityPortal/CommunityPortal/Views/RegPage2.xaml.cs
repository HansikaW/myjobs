using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.WindowsAzure.MobileServices;
using System.Diagnostics;
using CommunityPortal.Models;
using System.Security.Cryptography;
using System.IO;

namespace CommunityPortal.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegPage2 : ContentPage
	{
		public RegPage2 ()
		{
            InitializeComponent();
		}



        private bool validate()
        {
            if (string.IsNullOrEmpty(passwordEntry.Text))
            {
                DisplayAlert("Login", "Please Enter Email !", "Ok");
                return false;
            }
            else if (string.IsNullOrEmpty(passwordEntry.Text))
            {
                DisplayAlert("Login", "Please Enter password!", "Ok");
                return false;
            }
            else if (string.IsNullOrEmpty(passwordEntry2.Text))
            {
                DisplayAlert("Login", "Please Enter password again!", "Ok");
                return false;
            }

            else if (passwordEntry.Text!= passwordEntry2.Text)
            {
                DisplayAlert("Login", "Please Enter same password", "Ok");
                return false;
            }


            else
            {
                return true;
            }
        }


        static string EncryptionKey = "encryptionkey";
      

        //Encrypting a string
        public static string passwordEncrypt(string inText, string key)
        {
            byte[] bytesBuff = Encoding.Unicode.GetBytes(inText);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes crypto = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                aes.Key = crypto.GetBytes(32);
                aes.IV = crypto.GetBytes(16);
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cStream.Write(bytesBuff, 0, bytesBuff.Length);
                        cStream.Close();
                    }
                    inText = Convert.ToBase64String(mStream.ToArray());
                }
            }
            return inText;
        }








        public static MobileServiceClient MobileService =
      new MobileServiceClient("https://myjobapps.azurewebsites.net");


        private async void RegBtn1_Clicked(object sender, EventArgs e)
        {

            LoginBtn.IsEnabled = true;
            if (validate()) //validate()
            {
                // apiCall();
                this.IsBusy = true;
               // LoginBtn.IsEnabled = false;


                try
                {
                    CallerReg user = new CallerReg
                    {
                        FirstName = tempdata.FirstName,
                        LastName = tempdata.LastName,
                        Email = tempdata.Email,
                     
                        PhoneNumber = tempdata.PhoneNumber,
                       
                       
                        Password = passwordEncrypt(passwordEntry2.Text, EncryptionKey),

                         


                     };

                    await MobileService.GetTable<CallerReg>().InsertAsync(user);

                    this.IsBusy = false;
                    LoginBtn.IsEnabled = true;

                    await DisplayAlert("Register", "You are sucessfully registerd!", "Ok");

                    await Navigation.PushAsync(new LoginPage(), true);



                }
                catch (Exception ee)
                { Debug.WriteLine("" + ee); }






            }
        }


    }
}