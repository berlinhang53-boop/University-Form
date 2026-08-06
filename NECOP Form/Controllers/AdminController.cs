using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NECOP_Form.Data;
using NECOP_Form.Models;
using System.Reflection.Metadata.Ecma335;


namespace NECOP_Form.Controllers
{
    public class AdminController : Controller
    {

       

        private readonly ApplicationDbContext _context; // apna actual DbContext naam lagayen

        private readonly IConfiguration _config;

        public AdminController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
       

        // POST: /Admin/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var validUsername = _config["AdminCredentials:Username"];
            var validPassword = _config["AdminCredentials:Password"];

            if (username == validUsername && password == validPassword)
            {  
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction("Manage");
            }

            ViewBag.Error = "Invalid Username or Password";
            return View();
        } 

                                                                   
           
        
         
        // GET: /Admin/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAdmin");
            return RedirectToAction("Login");
        }                                                                                  
                                                                                                                    
                                                                                     

        // GET: /Admin/Manage  View
        //public async Task<IActionResult> Manage()
        //{
        //    ViewBag.Designations = await _context.DgModels.ToListAsync();
        //    ViewBag.Departments = await _context.Departments.ToListAsync();
        //    return View();
        //}





        //public async Task<IActionResult> Manage()
        //{
        //    if (HttpContext.Session.GetString("IsAdmin") != "true")
        //        return RedirectToAction("Login");

        //    ViewBag.Designations = await _context.DgModels.ToListAsync();
        //    ViewBag.Departments = await _context.Departments.ToListAsync();
        //    return View();
        //}

       





        
        public async Task<IActionResult> Manage()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            ViewBag.Designations = await _context.DgModels
                .Include(d => d.Department)
                .ToListAsync();

            ViewBag.Departments = await _context.Departments.ToListAsync();

            return View();
        }
        


        






        //[HttpGet]
        //public IActionResult AdminPage()
        //{
        //    return RedirectToAction("Manage");
        //}


        //[HttpPost]
        //public async Task<IActionResult> AddDesignation(string designationName)
        //{
        //    if (HttpContext.Session.GetString("IsAdmin") != "true")
        //        return Json(new { success = false, message = "Unauthorized" });


        //    if (string.IsNullOrWhiteSpace(designationName))
        //        return Json(new { success = false, message = "Designation name is required." });

        //    var trimmed = designationName.Trim();

        //    var exists = await _context.DgModels
        //        .AnyAsync(d => d.Name.ToLower() == trimmed.ToLower());

        //    if (exists)
        //        return Json(new { success = false, message = "This designation already exists." });

        //    var newDesignation = new DgModel { Name = trimmed };

        //    _context.DgModels.Add(newDesignation);
        //    await _context.SaveChangesAsync();

        //    return Json(new { success = true, id = newDesignation.Id, name = newDesignation.Name });
        //}





        [HttpPost]
        public async Task<IActionResult> AddDesignation(string designationName, int departmentId)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return Json(new { success = false, message = "Unauthorized" });

            if (string.IsNullOrWhiteSpace(designationName))
                return Json(new { success = false, message = "Designation name is required." });

            if (departmentId <= 0)
                return Json(new { success = false, message = "Please select a department." });

            var trimmed = designationName.Trim();

            var exists = await _context.DgModels
                .AnyAsync(d => d.Name.ToLower() == trimmed.ToLower() && d.DepartmentId == departmentId);

            if (exists)
                return Json(new { success = false, message = "This designation already exists in the selected department." });

            var newDesignation = new DgModel
            {
                Name = trimmed,
                DepartmentId = departmentId
            };

            _context.DgModels.Add(newDesignation);
            await _context.SaveChangesAsync();

            var department = await _context.Departments.FindAsync(departmentId);

            return Json(new
            {
                success = true,
                id = newDesignation.Id,
                name = newDesignation.Name,
                departmentId = departmentId,
                departmentName = department?.DepartmentName
            });
        }


        [HttpPost]
        public async Task<IActionResult> EditDesignation(int id, string designationName)
        {

            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return Json(new { success = false, message = "Unauthorized" });


            if (string.IsNullOrWhiteSpace(designationName))
                return Json(new { success = false, message = "Designation name is required." });

            var item = await _context.DgModels.FindAsync(id);
            if (item == null)
                return Json(new { success = false, message = "Designation not found." });

            var trimmed = designationName.Trim();

            var duplicate = await _context.DgModels
                .AnyAsync(d => d.Id != id && d.Name.ToLower() == trimmed.ToLower());

            if (duplicate)
                return Json(new { success = false, message = "Another designation with this name already exists." });

            item.Name = trimmed;
            await _context.SaveChangesAsync();

            return Json(new { success = true, id = item.Id, name = item.Name });
        }


        [HttpPost]
        public async Task<IActionResult> DeleteDesignation(int id)
        {

            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return Json(new { success = false, message = "Unauthorized" });



            var item = await _context.DgModels.FindAsync(id);
            if (item == null)
                return Json(new { success = false, message = "Designation not found." });

            _context.DgModels.Remove(item);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }



        [HttpPost]
        public async Task<IActionResult> AddDepartment(string departmentName)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return Json(new { success = false, message = "Unauthorized" });



            if (string.IsNullOrWhiteSpace(departmentName))
                return Json(new { success = false, message = "Department name is required." });

            var trimmed = departmentName.Trim();

            var exists = await _context.Departments
                .AnyAsync(d => d.DepartmentName.ToLower() == trimmed.ToLower());

            if (exists)
                return Json(new { success = false, message = "This department already exists." });

            var newDept = new DepartmentModel { DepartmentName = trimmed };

            _context.Departments.Add(newDept);
            await _context.SaveChangesAsync();

            return Json(new { success = true, id = newDept.DepartmentId, name = newDept.DepartmentName });
        }












        [HttpPost]
        public async Task<IActionResult> EditDepartment(int id, string departmentName)
        {

            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return Json(new { success = false, message = "Unauthorized" });




            if (string.IsNullOrWhiteSpace(departmentName))
                return Json(new { success = false, message = "Department name is required." });

            var item = await _context.Departments.FindAsync(id);
            if (item == null)
                return Json(new { success = false, message = "Department not found." });

            var trimmed = departmentName.Trim();

            var duplicate = await _context.Departments
                .AnyAsync(d => d.DepartmentId != id && d.DepartmentName.ToLower() == trimmed.ToLower());

            if (duplicate)
                return Json(new { success = false, message = "Another department with this name already exists." });

            item.DepartmentName = trimmed;
            await _context.SaveChangesAsync();

            return Json(new { success = true, id = item.DepartmentId, name = item.DepartmentName });
        }




        [HttpPost]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return Json(new { success = false, message = "Unauthorized" });




            var item = await _context.Departments.FindAsync(id);
            if (item == null)
                return Json(new { success = false, message = "Department not found." });

            // Agar us department pe designations/forms link hain to delete fail ho sakta hai (FK constraint)
            var inUse = await _context.Designations.AnyAsync(d => d.DepartmentId == id);
            if (inUse)
                return Json(new { success = false, message = "Cannot delete: this department is used in existing forms." });

            _context.Departments.Remove(item);
            await _context.SaveChangesAsync();


         

            return Json(new { success = true });
        }
    

        








       


        public IActionResult Index()
        {
            return View();
        }
    }
}
