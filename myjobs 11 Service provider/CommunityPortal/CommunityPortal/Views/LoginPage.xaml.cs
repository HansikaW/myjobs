using CommunityPortal.MasterDetailHome;
using CommunityPortal.Models;
using CommunityPortal.Views;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CommunityPortal
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
       


        public LoginPage ()
		{
			InitializeComponent ();
            //logo.Source = ImageSource.FromResource("CommunityPortal.Images.logo.png");
            this.BindingContext = this;
            this.IsBusy = false;
		}


        public static MobileServiceClient client = new MobileServiceClient("https://myjobapps.azurewebsites.net");
        IMobileServiceTable<UserReg> DataTable = client.GetTable<UserReg>();

        static string EncryptionKey = "encryptionkey";

        private async void LoginBtn_Clicked(object sender, EventArgs e)
        {
           // await Navigation.PushAsync(new HomePage(), true);
           
            

            if (validate()) {


                try
                {
                    this.IsBusy = true;
                    LoginBtn.IsEnabled = false;


                    IMobileServiceTableQuery<string> query = DataTable
                       .Where(ur => ur.Email == usernameEntry.Text)
                     .Select(ur => ur.Password);

                    List<string> passlist = await query.ToListAsync();


                    string Encpass = passlist[0];
                    string Dcpass= passwordDecrypt(Encpass, EncryptionKey);
                    

                    if (Dcpass == passwordEntry.Text)  //validate()

                    {
                        // apiCall();

                      string nameis =  string.Format("{0} {1}", "You are login as", usernameEntry.Text);

                        DisplayAlert("Login success!", nameis, "Ok");
                        tempdata.Loginas = usernameEntry.Text;

                        await Navigation.PushAsync(new HomePage(), true);
                    }
                    else
                    {
                        DisplayAlert("Login Fail!", "Please enter correct details ", "Ok");
                        this.IsBusy = false;
                        LoginBtn.IsEnabled = true;

                    }




                }
                catch (Exception ee)
                {

                    Debug.WriteLine("" + ee);

                }


            }

           
            

        }


      //  static string EncryptionKey = "encryptionkey";
        //Decrypting a string
        public static string passwordDecrypt(string cryptTxt, string key)
        {
            cryptTxt = cryptTxt.Replace(" ", "+");
            byte[] bytesBuff = Convert.FromBase64String(cryptTxt);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes crypto = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                aes.Key = crypto.GetBytes(32);
                aes.IV = crypto.GetBytes(16);
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cStream.Write(bytesBuff, 0, bytesBuff.Length);
                        cStream.Close();
                    }
                    cryptTxt = Encoding.Unicode.GetString(mStream.ToArray());
                }
            }
            return cryptTxt;
        }








        private bool validate()
        {
            if (string.IsNullOrEmpty(usernameEntry.Text))
            {
                DisplayAlert("Login", "Please Enter Email!", "Ok");
                return false;
            }
            else if (string.IsNullOrEmpty(passwordEntry.Text))
            {
                DisplayAlert("Login", "Please Enter Password!", "Ok");
                return false;
            }

            else if (1==1)
            {
                try
                {
                    MailAddress m = new MailAddress(usernameEntry.Text);
                    return true;
                }
                catch (FormatException)
                {

                    DisplayAlert("Invald Email", "Please Enter valid Email!", "Ok");
                    return false;
                }
            }



            else
            {
                return true;
            }          
        }



        private void RegBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegPage1(), true);
        }
    }
}