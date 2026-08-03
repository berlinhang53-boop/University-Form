using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NECOP_Form.Data;
using NECOP_Form.Models;
using System.Diagnostics;

namespace NECOP_Form.Controllers
{
    public class DesignationController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _environment;
        public DesignationController(ApplicationDbContext context,IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;

            
        }

        public IActionResult Index()
        {
            ViewBag.designation = _context.DgModels.ToList(); // Designation aa rahi database se

            ViewBag.TotalRecords = _context.Designations.Count(); // count ho rhy

            ViewBag.Departments = _context.Departments.ToList(); // Deparments le k aa raha database se


            return View();
        }





        //[HttpPost]
        //public IActionResult Save(DesignationModel dg)
        //{
        //    dg.CreatedAt = DateTime.Now;
        //    _context.Designations.Add(dg);
        //    _context.SaveChanges();
        //    TempData["SuccessMessage"] = "Form Submitted Successfully";

        //    //return RedirectToAction("Index");
        //    return RedirectToAction("List");
        //}






        [HttpPost]
        // ye action sirf post request accept kary ga
        public IActionResult Save(
       DesignationModel dg, //Ye form ka normal data receive karta hai. example <input name="Name"> <input name = "Description" >

        IFormFile ImageFile, //Ye uploaded image receive karta hai.
        IFormFile DocumentFile) //Ye uploaded document receive karta hai.
        {

            //dg.CreatedAt = DateTime.Now; Current date/time set kar raha hai.



            dg.CreatedAt = DateTime.UtcNow; // for postgre sql


            string uploadFolder = Path.Combine(_environment.WebRootPath, "uploads");  //Ye uploads folder ka path bana raha hai.



            if (!Directory.Exists(uploadFolder)) //Check kar raha hai:                 "Kya uploads folder exist karta hai?" Agar nahi: tou create kar de ga, createDirectory se




            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Image Upload

            if (ImageFile != null && ImageFile.Length > 0) // Matlb User ne file select ki? File Empty tou nai.................... Agar dono true hain to upload hoga.
            {
                string imageName =
                    Guid.NewGuid().ToString() + //Unique filename banana: jese jpg, png
                    Path.GetExtension(ImageFile.FileName);

                string imagePath =
                    Path.Combine(uploadFolder, imageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))  //Ye actual file server par save karta hai.
                {
                    ImageFile.CopyTo(stream);
                }

                dg.ImagePath = "/uploads/" + imageName; //database mein opath save hoga
            }

            // Document Upload

            if (DocumentFile != null && DocumentFile.Length > 0)
            {
                string fileName =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(DocumentFile.FileName);

                string filePath =
                    Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    DocumentFile.CopyTo(stream);
                }

                dg.FilePath = "/uploads/" + fileName;
            }   

            _context.Designations.Add(dg); //database mein save karna ha

            _context.SaveChanges(); //ye actually insert ki query vhalaye ga

            TempData["SuccessMessage"] = "Saved Successfully";

            return RedirectToAction("List");
        }
         
















        //public IActionResult List(string searchString)
        //{
        //    var data = _context.Designations.Include(x => x.Department).AsQueryable(); // Designations aur Departments k lie hai  

        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        data = data.Where(x =>

        //           (x.RefNo != null && x.RefNo.Contains(searchString)) ||

        //           (x.NameTasked != null && x.NameTasked.Contains(searchString)) ||

        //           (x.Designation != null && x.Designation.Contains(searchString)) ||

        //           (x.Officer != null && x.Officer.Contains(searchString)) ||

        //           (x.Department != null &&
        //            x.Department.DepartmentName.Contains(searchString))
        //         );
        //    }
        //    ViewBag.CurrentSearch = searchString;
        //    return View(data.ToList());
        //}














        public IActionResult List(string searchString, string sortOrder)
        {
            var data = _context.Designations
                .Include(x => x.Department)
                .AsQueryable();

            // Search
            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(x =>

                    (x.RefNo != null && x.RefNo.Contains(searchString)) ||

                    (x.NameTasked != null && x.NameTasked.Contains(searchString)) ||

                    (x.Designation != null && x.Designation.Contains(searchString)) ||

                    (x.Officer != null && x.Officer.Contains(searchString)) ||

                    (x.Department != null &&
                     x.Department.DepartmentName.Contains(searchString)) 

                     

                ); 
            }

            // Sorting ViewBags
            ViewBag.RefSort = sortOrder == "ref_asc" ? "ref_desc" : "ref_asc";
            ViewBag.RecordedSort = sortOrder == "record_asc" ? "record_desc" : "record_asc";
            ViewBag.OfficerSort = sortOrder == "officer_asc" ? "officer_desc" : "officer_asc";
            ViewBag.TaskSort = sortOrder == "task_asc" ? "task_desc" : "task_asc";
            ViewBag.DesignationSort = sortOrder == "des_asc" ? "des_desc" : "des_asc";
            ViewBag.ExecutionSort = sortOrder == "exe_asc" ? "exe_desc" : "exe_asc";
            ViewBag.RegistrationSort = sortOrder == "reg_asc" ? "reg_desc" : "reg_asc";
            ViewBag.SanctionSort = sortOrder == "sanc_asc" ? "sanc_desc" : "sanc_asc";
            ViewBag.CostSort = sortOrder == "cost_asc" ? "cost_desc" : "cost_asc";
            //ViewBag.detailssort = sortOrder == "detail_asc" ? "detail_desc" : "detail_asc";
            ViewBag.DepartmentSort = sortOrder == "dept_asc" ? "dept_desc" : "dept_asc";
            ViewBag.DateSort = sortOrder == "date_asc" ? "date_desc" : "date_asc";

            // Sorting
            switch (sortOrder)
            {
                case "ref_asc":
                    data = data.OrderBy(x => x.RefNo);
                    break;

                case "ref_desc":
                    data = data.OrderByDescending(x => x.RefNo);
                    break;

                case "record_asc":
                    data = data.OrderBy(x => x.RecordedBy);
                    break;

                case "record_desc":
                    data = data.OrderByDescending(x => x.RecordedBy);
                    break;

                case "officer_asc":
                    data = data.OrderBy(x => x.Officer);
                    break;

                case "officer_desc":
                    data = data.OrderByDescending(x => x.Officer);
                    break;

                case "task_asc":
                    data = data.OrderBy(x => x.NameTasked);
                    break;

                case "task_desc":
                    data = data.OrderByDescending(x => x.NameTasked);
                    break;

                case "des_asc":
                    data = data.OrderBy(x => x.Designation);
                    break;

                case "des_desc":
                    data = data.OrderByDescending(x => x.Designation);
                    break;


                case "exe_asc":
                    data = data.OrderBy(x => x.ExecutionOfficerIncharge);
                    break;

                case "exe_desc":
                    data = data.OrderByDescending(x => x.ExecutionOfficerIncharge);
                    break;




                case "reg_asc":
                    data = data.OrderBy(x => x.RegistrationType);
                    break;

                case "reg_desc":
                    data = data.OrderByDescending(x => x.RegistrationType);
                    break;


                case "sanc_asc":
                    data = data.OrderBy(x => x.SanctionType);
                    break;

                case "sanc_desc":
                    data = data.OrderByDescending(x => x.SanctionType);
                    break;



                case "cost_asc":
                    data = data.OrderBy(x => x.EstimatedCost);
                    break;

                case "cost_desc":
                    data = data.OrderByDescending(x => x.EstimatedCost);
                    break;




                //case "detail_asc":
                //    data = data.OrderBy(x => x.details);
                //    break;

                //case "detail_desc":
                //    data = data.OrderByDescending(x => x.details);
                //    break;


                case "dept_asc":
                    data = data.OrderBy(x => x.Department.DepartmentName);
                    break;

                case "dept_desc":
                    data = data.OrderByDescending(x => x.Department.DepartmentName);
                    break;

                case "date_asc":
                    data = data.OrderBy(x => x.CreatedAt);
                    break;

                case "date_desc":
                    data = data.OrderByDescending(x => x.CreatedAt);
                    break;

                default:
                    data = data.OrderByDescending(x => x.CreatedAt);
                    break;
            }

            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentSort = sortOrder;

            return View(data.ToList());
        }
























        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }


                                                                     
         
          
                                                         
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var record = _context.Designations.Find(id);

            if(record==null)
            {
                return NotFound();
            }
            TempData["SuccessMessage"] = "Record Updated Successfully";
            ViewBag.designation = _context.DgModels.ToList();
            ViewBag.Departments = _context.Departments.ToList();
            return View(record);
        }




        //[HttpPost]
        //public IActionResult Edit(DesignationModel model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _context.Update(model);
        //        _context.SaveChanges();
        //        TempData["SuccessMessage"] = "Record Updated Successfully";
        //        return RedirectToAction("List");
        //    }
        //    ViewBag.designation = _context.DgModels.ToList();
        //    ViewBag.Departments = _context.Departments.ToList();
        //    return View(model);

        //}



        [HttpPost]
        public IActionResult Edit(DesignationModel model)
        {
            try
            {
                _context.Update(model);
                _context.SaveChanges();

                return RedirectToAction("List");


                
            }
           
            catch (Exception ex)
            {
                return Content(ex.InnerException?.Message ?? ex.Message);
            }
        }



        public IActionResult Delete(int id)
        {
            var record = _context.Designations.Find(id);



            if (record != null)
            {
                _context.Designations.Remove(record);
                _context.SaveChanges();
            }

            TempData["SuccessMessage"] = "Record deleted successfully.";

            return RedirectToAction("List");
        }



        [HttpGet]
        public IActionResult PageView(int id)
        {
            //return View();
            ViewBag.TotalRecords = _context.Designations.Count();
            var record = _context.Designations.Include(x => x.Department).FirstOrDefault(x => x.Id == id);

            if(record==null)
            {
                return NotFound();
            }
            ViewBag.designation = _context.DgModels.ToList();
            ViewBag.Departments = _context.Departments.ToList();
            return View(record);
        }






    //    [HttpPost]
    //    public async Task<IActionResult> AddDesignation(string designationName)
    //    {
    //        if (string.IsNullOrWhiteSpace(designationName))
    //            return Json(new { success = false, message = "Designation name is required." });

    //        var trimmedName = designationName.Trim();

    //        var exists = await _context.DgModels
    //            .AnyAsync(d => d.Name.ToLower() == trimmedName.ToLower());

    //        if (exists)
    //            return Json(new { success = false, message = "This designation already exists." });

    //        var newDesignation = new DgModel
    //        {
    //            Name = trimmedName
    //        };

    //        _context.DgModels.Add(newDesignation);
    //        await _context.SaveChangesAsync();

    //        return Json(new { success = true, name = newDesignation.Name });
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> AddDepartment(string departmentName)
    //    {
    //        if (string.IsNullOrWhiteSpace(departmentName))
    //            return Json(new { success = false, message = "Department name is required." });

    //        var trimmedName = departmentName.Trim();

    //        var exists = await _context.Departments
    //            .AnyAsync(d => d.DepartmentName.ToLower() == trimmedName.ToLower());

    //        if (exists)
    //            return Json(new { success = false, message = "This department already exists." });

    //        var newDepartment = new DepartmentModel
    //        {
    //            DepartmentName = trimmedName
    //        };

    //        _context.Departments.Add(newDepartment);
    //        await _context.SaveChangesAsync();

    //        return Json(new { success = true, id = newDepartment.DepartmentId, name = newDepartment.DepartmentName });
    //    }



    }




}




