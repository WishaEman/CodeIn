using Code;
using Microsoft.AspNetCore.Mvc;

namespace Code.Models
{
    public class CodebaseRepository
    {
        public static bool CheckFileExists(string fname, int Id)
        {
            CodeInContext dbContext = new CodeInContext();
            bool isPresent = dbContext.Codebases.Any(c => c.Name == fname && c.UserId == Id);
            if (isPresent)
                return true;
            else
                return false;
        }

        public static int getDbId(int userId, string fname)
        {
            CodeInContext dbContext = new CodeInContext();
            Codebase codebase = dbContext.Codebases.FirstOrDefault(u => u.UserId == userId && u.Name == fname);
            if (codebase != null)
            {
                return codebase.Id;
            }
            return -1;
        }

        public static string getDbName(int dbId)
        {
            CodeInContext dbContext = new CodeInContext();
            Codebase codebase = dbContext.Codebases.FirstOrDefault(u => u.Id == dbId );
            if (codebase != null)
            {
                return codebase.Name;
            }
            return "";
        }


        public static void AddCodebase(string fname, int Uid)
        {
            CodeInContext dbContext = new CodeInContext();
            Codebase codebase = new Codebase();
            codebase.Name = fname;
            codebase.UserId = Uid;
            dbContext.Codebases.Add(codebase);
            dbContext.SaveChanges();
        }

        public static List<Codebase> codebases(int Uid)
        {
           List<Codebase> code = new List<Codebase>();
           CodeInContext dbContext = new CodeInContext();
           var c = dbContext.Codebases.ToList();
           
           foreach(var i in c)
           {
                if (i.UserId == Uid)
                    code.Add(i);
           }
            return code;
        }
    

        public static Codebase? SearchFile(string name, int Uid)
        {
            bool flag = CheckFileExists(name, Uid);
            if (flag)
            {
                CodeInContext dbContext = new CodeInContext();
                Codebase codebase = dbContext.Codebases.FirstOrDefault(u => u.UserId == Uid && u.Name == name);
                return codebase;
            }
            return null;
        }
    }
}
