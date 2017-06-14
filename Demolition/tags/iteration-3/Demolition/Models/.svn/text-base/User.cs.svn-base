using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace Demolition.Models
{
    public partial class User : Model, IPrincipal
    {
        public enum Roles { Administrator, Salesperson }

        public static IList<User> ListAll()
        {
            var users = from u in GetDataContext().Users
                        select u;
            return users.ToList();
        }

        public IIdentity Identity
        {
            get { return new GenericIdentity(Name); }
        }

        public bool IsInRole(string role)
        {
            return Role == role;
        }

        public static User Find(string username, string password)
        {
            User ourUser = GetDataContext().Users.SingleOrDefault(d => d.Name == username);

            if (ourUser != null && password == ourUser.Password)
                return ourUser;
            else
                return null;
        }

        public static User Create(string username, string password, string email, Roles role)
        {
            var newUser = new User();
            newUser.Email = email;
            newUser.Name = username;
            newUser.Password = password;
            newUser.Role = role.ToString();
            newUser.CreatedAt = newUser.UpdatedAt = DateTime.Now;

            var context = GetDataContext();
            context.Users.InsertOnSubmit(newUser);
            context.SubmitChanges();

            return newUser;
        }

        public bool ChangePassword(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
