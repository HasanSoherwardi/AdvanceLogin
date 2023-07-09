using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AdvanceLogin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : TabbedPage
    {
        User userDetails = new User();
        public Home (User user)
		{
			InitializeComponent ();

            userDetails = user;
            if (user != null)
            {
                if (user.myArray == null)
                {
                    myImage.Source = "man.png";
                }
                else
                {
                   PopulateDetails(userDetails);
                }
              
            }
            else
            {
                myImage.Source = "man.png";
                userDetails.myArray = null;
            }
        }
        private void PopulateDetails(User employee)
        {
         
            MemoryStream streamRead = new MemoryStream(employee.myArray.ToArray());
            myImage.Source = ImageSource.FromStream(() => { return streamRead; });
            userDetails.myArray = employee.myArray.ToArray();
            
        }
        protected override void OnAppearing()
        {
            PopulateEmployeeList();

        }
        public void PopulateEmployeeList()
        {
            EmployeeList.ItemsSource = null;
            EmployeeList.ItemsSource = DependencyService.Get<SQLiteInterface>().GetEmployees(userDetails);
        }

        private void EmployeeList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //User user = e.Item as User;
            //if (user != null)
            //{
            //    Navigation.PushAsync(new RegistrationPage(user));
            //}

            User user = e.Item as User;
            if (user != null)
            {
                Navigation.PushAsync(new RegistrationPage(user));
                MessagingCenter.Unsubscribe<User>(this, "ReciveData");
                MessagingCenter.Subscribe<User>(this, "ReciveData", (value) =>
                {
                    if (user.myArray == null)
                    {
                        myImage.Source = "man.png";
                        MessagingCenter.Unsubscribe<User>(this, "ReciveData");
                    }
                    else
                    {
                        MemoryStream streamRead = new MemoryStream(user.myArray.ToArray());
                        myImage.Source = ImageSource.FromStream(() => { return streamRead; });
                        userDetails.myArray = user.myArray.ToArray();
                        MessagingCenter.Unsubscribe<User>(this, "ReciveData");
                    }
                });
            }
        }
    }
}