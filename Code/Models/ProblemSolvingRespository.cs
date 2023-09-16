using Code;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Globalization;
using System.IO;
using System.Transactions;

namespace Code.Models
{
    public class Problem
    {
        public int Id { get; set; }
        public string? statement { get; set; }
        public string? description { get; set; }
        public string? returns { get; set; }

        public string? input { get; set; }

        public string? output { get; set; }

    }

    public class ProblemSolvingRespository
    {
        public static Problem getFileData(int targetId)
        {
            string filePath = "C:\\Users\\Wisha\\source\\repos\\Code\\Code\\wwwroot\\file\\problemsolving.txt";
            Problem p  = new Problem();
            int id = 1;
            bool isIdMatched = false;
            bool isStop = false;

            if (File.Exists(filePath))
            {
                using(StreamReader reader  = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("~") && line.EndsWith("~"))
                        {
                            if (int.TryParse(line.Trim('~'), out id) && id == targetId)
                            {
                                isIdMatched = true;
                            }
                            else
                            {
                                isIdMatched = false;
                            }
                        }

                        if (isIdMatched && line.StartsWith("****"))
                        {
                            string data = String.Empty;
                            p.Id  = targetId;
                            while ((line = reader.ReadLine()) != null && !line.StartsWith("***"))
                            {
                                data += line.Trim() + Environment.NewLine;
                            }
                            p.statement = data;

                            data = String.Empty;
                            while ((line = reader.ReadLine()) != null && !line.StartsWith("***"))
                            {
                                data += line.Trim() + Environment.NewLine;
                            }
                            p.description = data;

                            data = String.Empty;
                            while ((line = reader.ReadLine()) != null && !line.StartsWith("***"))
                            {
                                data += line.Trim() + Environment.NewLine;
                            }
                            p.returns = data;

                            data = String.Empty;
                            while ((line = reader.ReadLine()) != null && !line.StartsWith("***"))
                            {
                                data += line.Trim() + Environment.NewLine;
                            }
                            p.input = data;

                            data = String.Empty;
                            while ((line = reader.ReadLine()) != null && !line.StartsWith("***"))
                            {
                                data += line.Trim() + Environment.NewLine;
                            }
                            p.output = data;
                            break;
                        }
                    }
                }
            }
            return p;
        }

        public static List<ProblemSolving> Problems()
        {
            CodeInContext dbContext = new CodeInContext();
            var p = dbContext.ProblemSolvings.ToList();
            return p;
        }

        public static string ProblemName(int id)
        {
            CodeInContext dbContext = new CodeInContext();
            ProblemSolving p = dbContext.ProblemSolvings.FirstOrDefault(u => u.Id == id);
            if (p != null)
            {
                return p.Name;
            }
            return "";

        }
        public static List<ProblemSolving> SearchType(string type)
        {
            if (type == "arrays" || type == "array" || type == "Array" || type == "Arrays" || type == "ARRAY")
                type = "Arrays";

            if (type == "string" || type == "strings" || type == "String" || type == "Strings" || type == "STRING")
                type = "Strings";

            CodeInContext dbContext = new CodeInContext();
            List<ProblemSolving> problems  = dbContext.ProblemSolvings.Where(u => u.Type == type).ToList();
            Console.WriteLine(problems.Count);

            return problems;

        }
    }
}
