using Code.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Code;
using System.Reflection.Metadata;

namespace Code.Controllers
{
    public class UserInterfaceController : Controller
    {
        public IActionResult Welcome(int check = 0)
        {
            string username = HttpContext.Session.GetString("Username");
            ViewBag.check = false;
            if (check == 1)
                ViewBag.check = true;
            if (!string.IsNullOrEmpty(username))
            {
                // Pass the username to the view
                ViewBag.Username = username;
                return View();
            }
            return View();
        }

        public IActionResult CodeBase()
        {
            ViewBag.Username = string.Empty;
            string username = HttpContext.Session.GetString("Username");
            if (!string.IsNullOrEmpty(username))
            {
                ViewBag.Username = username + "'s CodeBase";
            }
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            List<Codebase> list = CodebaseRepository.codebases(userId);
            return View(list);
        }

        
        public IActionResult CodeBaseEditor(int dbId) 
        {
            ViewBag.name = String.Empty;
            if(dbId == -1)
            {
                return View();
            }
            else
            {
                ViewBag.name  = CodebaseRepository.getDbName(dbId);
                Object data = FileHandlingRepository.GetFileData(dbId);
                return View(data);
            }
        }

        [HttpPost]
        public IActionResult SaveFile(string fileName, string code)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            bool exists = CodebaseRepository.CheckFileExists(fileName, userId);
            if (!exists)
            {
                CodebaseRepository.AddCodebase(fileName, userId);
                FileHandlingRepository.SetFileData(code, userId, fileName);
            }
            return Json(new { exists });
        }

        public IActionResult ProblemSolvingEditor(int id)
        {
            Problem p =  ProblemSolvingRespository.getFileData(id);
            ViewBag.problemName = ProblemSolvingRespository.ProblemName(id);
            Compilation.WriteSampleOutput(p.output);
            return View(p);
        }

        public IActionResult ProblemSolving(bool solved = false)
        {
            ViewBag.check = false;
            if (solved)
                ViewBag.check = true;
            List<ProblemSolving>  list = ProblemSolvingRespository.Problems();
            return View(list);
        }

        public IActionResult CodeEditor()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Execute(string content, string lang, string input)
        {
            string output = string.Empty;
            if (lang == "cpp")
                output = Compilation.ExecuteCpp(content, input);
            else if (lang == "python")
                output = Compilation.ExecutePython(content, input);
            return Json(output);
        }

        [HttpPost]
        public IActionResult SearchCodebase(string fileName)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            Codebase b = CodebaseRepository.SearchFile(fileName, userId);
            List<Codebase> list = new List<Codebase>();
            if (b != null)
            {
                list.Add(b);
            }
            return View("CodeBase", list);
        }

        [HttpPost]
        public  IActionResult SearchProblem(string type)
        {
          ViewBag.Check =  false;
          List<ProblemSolving> list = ProblemSolvingRespository.SearchType(type);
          return View("ProblemSolving", list);
        }

        [HttpPost]
        public JsonResult CheckOutput(string content, string lang, string input)
        {
            string output = string.Empty;
            if (lang == "cpp")
                output = Compilation.ExecuteCpp(content, input);
            else if (lang == "python")
                output = Compilation.ExecutePython(content, input);
            bool result = Compilation.Compare(output);
            return Json(new { success = result });
        }
    }
}
