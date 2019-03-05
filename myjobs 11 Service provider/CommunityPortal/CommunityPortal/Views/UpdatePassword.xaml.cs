using CommunityPortal.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CommunityPortal.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdatePassword : ContentPage
	{
		public UpdatePassword ()
		{
			InitializeComponent ();

            GetdataAsync();



        }


        public static MobileServiceClient client = new MobileServiceClient("https://myjobapps.azurewebsites.net");


        IMobileServiceTable<UserReg> DataTable = client.GetTable<UserReg>();





        private bool validate()
        {
            if (string.IsNullOrEmpty(CurrentPassword.Text))
            {
                DisplayAlert("update", "Please Enter Current Password!", "Ok");
                return false;
            }
            else if (string.IsNullOrEmpty(NewPassword.Text))
            {
                DisplayAlert("update", "Please Enter New Password!", "Ok");
                return false;
            }
            else if (string.IsNullOrEmpty(Confirm.Text))
            {
                DisplayAlert("update", "Please Confirm!", "Ok");
                return false;
            }

            else if (NewPassword.Text != Confirm.Text)
            {
                DisplayAlert("update", "Please enter same password!", "Ok");
                return false;
            }




            /*
            else if (!IsPhoneNumber(PhoneNumber.Text))
            {
                DisplayAlert("Register", "Please Enter valid TP no!", "Ok");
                return false;
            }
            */

            else
            {
                return true;
            }
        }



          static string EncryptionKey = "encryptionkey";
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











        string FirstName;
        string LastName;
        string Email;
        string Address;
        string PhoneNumber; 
        string ServiceType;
        string pass;

        public async Task GetdataAsync()
        {
            try
            {


                IMobileServiceTableQuery<UserReg> query = DataTable
                .Where(ur => ur.Email == tempdata.Loginas);
                // .Select(todoItem => todoItem.InvoiceSubject);

                List<UserReg> items = await query.ToListAsync();



                FirstName = items[0].FirstName;
                LastName = items[0].LastName;
                Email = items[0].Email;
                Address = items[0].Address;
                PhoneNumber = items[0].PhoneNumber;
                ServiceType = Convert.ToString(items[0].ServiceType);
                tempdata.uId2 = items[0].Id;
                tempdata.pass = passwordDecrypt(items[0].Password, EncryptionKey);



            }
            catch (Exception e)
            {

                Debug.WriteLine("Sync error: {0}", new[] { e.Message });
            }



        }



       private  bool validatepass() {

            if (tempdata.pass == CurrentPassword.Text)
            {
                return true;

            }
            else {
                DisplayAlert("Incorrect", "Please Enter Current Password correctly!", "Ok");
                return false;

            }


        }




        private async void updatebtn(object sender, EventArgs e)
        {


            if (validate() && validatepass()) //validate()
            {
                // apiCall();
                this.IsBusy = true;


                try
                {
                    UserReg acc = new UserReg
                    {
                        Id = tempdata.uId2,
                        FirstName = FirstName,
                        LastName = LastName,

                        Email = Email,
                        Address = Address,
                        PhoneNumber = PhoneNumber,
                        ServiceType = ServiceType,
                        Password = passwordEncrypt(Confirm.Text, EncryptionKey),



                    };

                    await client.GetTable<UserReg>().UpdateAsync(acc); //.InsertAsync(acc);


                    this.IsBusy = false;


                    await DisplayAlert("Successful", "Your password updated", "Ok");
                    await Navigation.PushAsync(new LoginPage(), true);




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