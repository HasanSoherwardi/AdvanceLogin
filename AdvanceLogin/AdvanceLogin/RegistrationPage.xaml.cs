using Plugin.Media;
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
	public partial class RegistrationPage : ContentPage
	{
        User UserDetails = new User();
        public RegistrationPage (User user)
		{
			InitializeComponent ();

            if (user != null)
            {
                if (user.myArray == null)
                {
                    UserDetails = user;
                    Name.Text = user.Name;
                    dp.Date = user.DOB;
                    Place.Text = user.POB;
                    Email.Text = user.Email;
                    UserId.Text = user.UserId;
                    Password.Text = user.Password;
                    //Password.IsEnabled = false;

                    myImage.Source = "man.png";
                    SaveBtn.Text = "Update";
                    this.Title = "Edit Info";
                }
                else
                {
                    UserDetails = user;
                    PopulateDetails(UserDetails);
                }

            }
            else
            {
                SaveBtn.Text = "Save";
                this.Title = "Registration";
                myImage.Source = "man.png";
                UserDetails.myArray = null;
            }
        }

        private void PopulateDetails(User user)
        {
            UserDetails = user;
            Name.Text = user.Name;
            dp.Date = user.DOB;
            Place.Text = user.POB;
            Email.Text = user.Email;
            UserId.Text = user.UserId;
            Password.Text = user.Password;

            MemoryStream streamRead = new MemoryStream(user.myArray.ToArray());
            myImage.Source = ImageSource.FromStream(() => { return streamRead; });
            UserDetails.myArray = user.myArray.ToArray();
            SaveBtn.Text = "Update";
            this.Title = "Edit Info";
        }


        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            try
            {


                if (SaveBtn.Text == "Save")
                {
                    UserDetails.Name = Name.Text;
                    UserDetails.DOB = dp.Date;
                    UserDetails.POB = Place.Text;
                    UserDetails.Email = Email.Text;
                    UserDetails.UserId = UserId.Text;
                    UserDetails.Password = Password.Text;

                    bool response = DependencyService.Get<SQLiteInterface>().SaveEmployee(UserDetails);
                    if (response)
                    {
                        await DisplayAlert("Saved", "Save Successfully.", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error", "Not Save.", "OK");
                    }


                }
                else
                {
                    UserDetails.Name = Name.Text;
                    UserDetails.DOB = dp.Date;
                    UserDetails.POB = Place.Text;
                    UserDetails.Email = Email.Text;
                    UserDetails.UserId = UserId.Text;
                    UserDetails.Password = Password.Text;

                    bool response = DependencyService.Get<SQLiteInterface>().UpdateEmployee(UserDetails);
                    if (response)
                    {
                        await DisplayAlert("Updated", "Update Successfully.", "OK");

                        //for (var counter = 1; counter < 2; counter++)
                        //{
                        //   Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                        //}
                        MessagingCenter.Send<User>(UserDetails, "ReciveData");
                        await Navigation.PopAsync();

                        //Navigation.PopAsync();
                        //Navigation.PushAsync(new Home(UserDetails));
                    }
                    else
                    {
                        await DisplayAlert("Error", "Not Update.", "OK");
                    }
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Input Error", ex.ToString(), "Ok");
            }
        }

        //private async void BtnCapture_Clicked(object sender, EventArgs e)
        //{
        //    await CrossMedia.Current.Initialize();

        //    if (!CrossMedia.Current.IsTakePhotoSupported && !CrossMedia.Current.IsPickPhotoSupported)
        //    {
        //        await DisplayAlert("Error", "Photo Capture and Picked not Supported", "OK");
        //        return;
        //    }
        //    else
        //    {
        //        var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
        //        {
        //            Directory = "Image",
        //            Name = DateTime.Now + "_Text.jpg"
        //        });
        //        if (file == null)
        //        {
        //            return;
        //        }
        //        //await DisplayAlert("File Path", file.Path, "Ok");
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            file.GetStream().CopyTo(memoryStream);
        //            UserDetails.myArray = memoryStream.ToArray();
        //        }
        //        myImage.Source = ImageSource.FromStream(() =>

        //        {
        //            var stream = file.GetStream();
        //            return stream;
        //        });
        //    }
        //}
        private async void BtnBrowse_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No Upload", "Picking a photo is not supported", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
            {
                return;
            }
            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                UserDetails.myArray = memoryStream.ToArray();
            }
            myImage.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }
    }
}