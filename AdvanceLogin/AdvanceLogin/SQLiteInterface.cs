using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvanceLogin
{
    public interface SQLiteInterface
    {
        SQLiteConnection GetConnectionWithDatabase();
        //bool SaveEmployee(string name, string address, string phonenumber, string email, string password);

        bool SaveEmployee(User user);
        List<User> GetEmployees(User user);
        bool UpdateEmployee(User user);
        void DeleteEmployee(int id);
        User GetUser(User user);
    }
}
