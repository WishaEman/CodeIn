using Code;
using Microsoft.AspNetCore.Mvc;

namespace Code
{
    public class UserRepository
    {
        public static bool AddUser(User user)
        {
            CodeInContext dbContext = new CodeInContext();
            bool isTaken = dbContext.Users.Any(u => u.Email == user.Email);
            if (isTaken)
            {
                return isTaken;
            }
            else
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }
            return false;
        }
        public static bool AuthorizeUser(User user)
        {
            CodeInContext dbContext = new CodeInContext();
            bool isPresent = dbContext.Users.Any(u => u.Email == user.Email && u.Password == user.Password);
            if (isPresent)
                return true;
            else
                return false;

        }

        public static string returnName(User user)
        {
            CodeInContext dbContext = new CodeInContext();
            var temp = dbContext.Users.FirstOrDefault(u => user.Email == u.Email);

            if (temp != null)
            {
                string name = temp.Name;
                return name;
            }
            return "User not found";
        }

        public static int returnId(User user)
        {
            CodeInContext dbContext = new CodeInContext();
            var temp = dbContext.Users.FirstOrDefault(u => user.Email == u.Email);

            if (temp != null)
            {
                int Uid = temp.Id;
                return Uid;
            }
            return -1;
        }

    }
}
