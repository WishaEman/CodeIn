using System.IO;

namespace Code.Models
{
    public class FileHandlingRepository
    {

        public static void SetFileData(string code, int userId, string fname)
        {
            int DbId = CodebaseRepository.getDbId(userId, fname);

            if (File.Exists("C:\\Users\\Wisha\\source\\repos\\Code\\Code\\wwwroot\\file\\codebasefile.txt"))
            {
                StreamWriter sw = new StreamWriter("C:\\Users\\Wisha\\source\\repos\\Code\\Code\\wwwroot\\file\\codebasefile.txt", append:true);
                sw.WriteLine($"[{DbId}]");
                sw.WriteLine($"[{code}]");
                sw.WriteLine("-----");
                sw.Close();
            }
        }

        public static string GetFileData(int dbId) 
        {
            string filepath = "C:\\Users\\Wisha\\source\\repos\\Code\\Code\\wwwroot\\file\\codebasefile.txt";
            string content = string.Empty;
            if (File.Exists(filepath))
            {
                using (StreamReader reader = new StreamReader(filepath))
                {
                    string line;
                    bool idMatched = false;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Trim() == $"[{dbId}]")
                        {
                            idMatched = true;
                            continue;
                        }

                        if (idMatched)
                        {
                            if (line.Trim() == "-----")
                                break;

                            content += line + Environment.NewLine;
                        }
                    }
                }
            }
            content = content.TrimStart('[');
            content = content.TrimEnd(']');
            return content;
        }

    }
}
