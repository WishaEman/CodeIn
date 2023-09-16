using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;
using Microsoft.Extensions.ObjectPool;
using Microsoft.AspNetCore.Components.Forms;

namespace Code.Models
{
    public class Compilation
    {
        static public string ExecuteCpp(string content, string compileInput)
        {

            string output = String.Empty;
            string path = "C:\\Users\\Wisha\\source\\repos\\Code\\Code\\wwwroot\\file\\compilecpp.cpp";
            string inputPath = "C:\\Users\\Wisha\\source\\repos\\Code\\Code\\wwwroot\\file\\inpcpp.txt"; 
            string filepath = "C:/Users/Wisha/source/repos/Code/Code/wwwroot/file/";
            if (File.Exists(path))
            {
                StreamWriter sw = new StreamWriter(path, append: false);
                sw.WriteLine(content);
                sw.Close();
            }
            if(File.Exists(inputPath))
            { 
                StreamWriter sw = new StreamWriter(inputPath);
                sw.Write(string.Empty);
                sw.Close();
            }
            if (File.Exists(inputPath) && !string.IsNullOrEmpty(compileInput))
            {
                StreamWriter sw = new StreamWriter(inputPath, append: false);
                sw.WriteLine(compileInput);
                sw.Close();
            }
            
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = false;

            process.StartInfo = startInfo;
            process.Start();
            process.StandardInput.WriteLine(@"cd " + filepath);
            process.StandardInput.WriteLine("g++ compilecpp.cpp 2> out.txt -o a.exe && a.exe < inpcpp.txt > out.txt");
            process.StandardInput.WriteLine("exit");
            process.WaitForExit();
            string outPath = "C:\\Users\\Wisha\\source\\repos\\Code\\Code\\wwwroot\\file\\out.txt";
            if (File.Exists(outPath))
            {
                StreamReader sr = new StreamReader(outPath);
                output = sr.ReadToEnd();
                sr.Close();
            }
            return output;
        }
        static public string ExecutePython(string content, string compileInput)
        {

            string output = String.Empty;
            string path = "C:\\Users\\Wisha\\source\\repos\\Code\\Code\\wwwroot\\file\\compilepy.py";
            string filepath = "C:/Users/Wisha/source/repos/Code/Code/wwwroot/file/";
            string inputPath = "C:\\Users\\Wisha\\source\\repos\\Code\\Code\\wwwroot\\file\\inppy.txt";
            if (File.Exists(path))
            {
                StreamWriter sw = new StreamWriter(path, append: false);
                sw.WriteLine(content);
                sw.Close();
            }
            if (File.Exists(inputPath))
            {
                StreamWriter sw = new StreamWriter(inputPath);
                sw.Write(string.Empty);
                sw.Close();
            }
            if (File.Exists(inputPath) && !string.IsNullOrEmpty(compileInput))
            {
                StreamWriter sw = new StreamWriter(inputPath, append: false);
                sw.WriteLine(compileInput);
                sw.Close();
            }
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = false;

            process.StartInfo = startInfo;
            process.Start();
            process.StandardInput.WriteLine(@"cd " + filepath);
            process.StandardInput.WriteLine("python compilepy.py < inppy.txt > outpy.txt 2>&1");
            process.StandardInput.WriteLine("echo %errorlevel% > returnstatus.txt");
            process.StandardInput.WriteLine("exit");
            process.WaitForExit();
            string outPath = "C:\\Users\\Wisha\\source\\repos\\Code\\Code\\wwwroot\\file\\outpy.txt";
            if (File.Exists(outPath))
            {
                StreamReader sr = new StreamReader(outPath);
                output = sr.ReadToEnd();
                sr.Close();
            }
            outPath = "C:\\Users\\Wisha\\source\\repos\\Code\\Code\\wwwroot\\file\\returnstatus.txt";
            string temp = string.Empty;
            if (File.Exists(outPath))
            {
                StreamReader sr = new StreamReader(outPath);
                temp = sr.ReadToEnd();
                int check = int.Parse(temp.Trim());
                if (check > 0)
                {
                    int commaIndex = output.IndexOf(",");
                    if (commaIndex >= 0)
                        output = output.Substring(commaIndex + 1).Trim();
                }
                sr.Close();
            }

            return output;
        }

        static public void WriteSampleOutput(string output)
        {
            string path = "C:\\Users\\Wisha\\source\\repos\\Code\\Code\\wwwroot\\file\\SampleOut.txt";
            if (File.Exists(path))
            {
                StreamWriter sw = new StreamWriter(path, append: false);
                sw.WriteLine(output);
                sw.Close();
            }
        }

        static public bool Compare(string output)
        {
            string path = "C:\\Users\\Wisha\\source\\repos\\Code\\Code\\wwwroot\\file\\SampleOut.txt";
            string fileContent = File.ReadAllText(path);
            fileContent = fileContent.Trim();
            output = output.Trim();
            return fileContent.Equals(output, StringComparison.OrdinalIgnoreCase);
        }
    }
    
   

}