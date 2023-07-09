



//MainPage.xaml.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AdvanceLogin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
            if (UName.Text == null)
            {
                DisplayAlert("Input Error", "Please enter user name!!!", "OK");
                UName.Focus();
                return;
            }
            if ((Password.Text == null))
            {
                DisplayAlert("Input Error", "Please enter password!!!", "OK");
                Password.Focus();
                return;
            }
            User UserDetails = new User();
            UserDetails.UserId = UName.Text;
            UserDetails.Password = Password.Text;

            var res = DependencyService.Get<SQLiteInterface>().GetUser(UserDetails);
            if (res != null)
            {
                UserDetails = res;
                DisplayAlert("Welcome", "Login Successfully.", "OK");
                Navigation.PushAsync(new Home(UserDetails));
            }
            else
            {
                DisplayAlert("Error", "Invalid Credentials", "OK");
            }
        
            //bool response = DependencyService.Get<SQLiteInterface>().GetUser(UserDetails);
            //if (response)
            //{
            //    DisplayAlert("Welcome", "Login Successfully.", "OK");
            //    Navigation.PushAsync(new Home(null));
            //}
            //else
            //{
            //    DisplayAlert("Error", "Invalid Credentials", "OK");
            //}
        }

        
        private void BtnRegistration_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistrationPage(null));
        }
    }
}
