using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CommunityPortal.MasterDetailHome
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageMaster : ContentPage
    {
        public ListView ListView;

        public HomePageMaster()
        {
            InitializeComponent();

            BindingContext = new HomePageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class HomePageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<HomePageMenuItem> MenuItems { get; set; }

            public HomePageMasterViewModel()
            {
                MenuItems = new ObservableCollection<HomePageMenuItem>(new[]
                {
                    new HomePageMenuItem { Id = 0, Title = "Home", TargetType = typeof(HomePageDetail) },
                    new HomePageMenuItem { Id = 2, Title = "Invoice", TargetType = typeof(Views.InvoicePage)},
                    new HomePageMenuItem { Id = 2, Title = "Quotation", TargetType = typeof(Views.QuotationTabbedPage)},
                     new HomePageMenuItem { Id = 2, Title = "WorkOrders", TargetType = typeof(Views.WorkOrders)},
                    new HomePageMenuItem { Id = 1, Title = "User Details", TargetType = typeof(Views.UserDetailsPage) },
                    //new HomePageMenuItem { Id = 2, Title = "Settings", TargetType = typeof(Views.SettingsPage)},
                      new HomePageMenuItem { Id = 2, Title = "MyAccount", TargetType = typeof(Views.BankAccount)},


                    new HomePageMenuItem { Id = 3, Title = "Logout",TargetType = typeof(LoginPage) },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}