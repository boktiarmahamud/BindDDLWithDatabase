using System.Diagnostics;
using BindDDLWithDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BindDDLWithDatabase.Controllers
{
    public class HomeController : Controller
    {
        private readonly CodeFirstDbContext context;// db context declaration

		public HomeController(CodeFirstDbContext context) // constructor
		{
            this.context = context;// initialize db context
		}

		private StudentModel BindDDl() // method to bind dropdown list
		{
			StudentModel stdModel = new StudentModel();// create object of student model
			stdModel.StudentList = new List<SelectListItem>();//	create list of select list item
			var data = context.Students.ToList();// fetch data from student table

			stdModel.StudentList.Add(new SelectListItem //default item
			{
				Text = "--Select Name--",

				Value = ""
			});


			foreach (var item in data)
			{
				stdModel.StudentList.Add(new SelectListItem
				{// bind dropdown list
					Text = item.StudentName,// bind student name
					Value = item.Id.ToString()// bind student id
				});
			}
			return stdModel;
		}
		public IActionResult Index()
        {
           var std = BindDDl();//call bind ddl method


			return View(std);
        }
       

        [HttpPost] // post method
		[ValidateAntiForgeryToken]
		public IActionResult Index(StudentModel std)
		{
            var student = context.Students.Where(x => x.Id == std.Id).FirstOrDefault();
            if (student != null) { 
                ViewBag.record = student.StudentName;
			}
			var data = BindDDl();//call bind ddl method
			return View(data);
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
