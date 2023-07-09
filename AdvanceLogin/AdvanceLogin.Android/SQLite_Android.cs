using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AdvanceLogin.Droid;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace AdvanceLogin.Droid
{
    public class SQLite_Android : SQLiteInterface
    {
        SQLiteConnection database;

        public void DeleteEmployee(int id)
        {
            string sql = $"Delete from User where id={id}";
            database.Execute(sql);
        }

        public SQLiteConnection GetConnectionWithDatabase()
        {
            string filename = "LoginDatabase.db3";
            string documentpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(documentpath, filename);
            database = new SQLiteConnection(path);
            database.CreateTable<User>();
            return database;
        }

        public List<User> GetEmployees(User user)
        {
            string sql = $"Select * from User Where UserId ='{user.UserId}' and password='{user.Password}'";
            List<User> employees = database.Query<User>(sql);
            return employees;
        }

        public User GetUser(User user)
        {
            //bool res = false;
            try
           {
            //     string sql = $"Select * from User Where UserId='{user.UserId}' and Password='{user.Password}'";
            //    int temp = database.Execute(sql);


            //    if (temp >= 0)
            //    {
            //        res = true;
            //    }
            //    //database.Update(user);
            //    else
            //    {
            //        res = false;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    res = false;
            //}

            return database.Table<User>().Where(x => x.UserId == user.UserId && x.Password == user.Password ).SingleOrDefault();

            }
            catch (SQLiteException ex)
            {
                return null;
            }
        }

        public bool SaveEmployee(User user)
        {
            bool res = false;
            try
            {
                database.Insert(user);
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }

        public bool UpdateEmployee(User user)
        {
            bool res = false;
            try
            {
                // string sql = $"Update Employee set Name='{employee.Name}', Address='{employee.Address}',PhoneNumber='{employee.PhoneNumber}',Email='{employee.Email}', myArray='{employee.myArray}' Where id={employee.id}";
                // database.Execute(sql);
                database.Update(user);
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
    }
}