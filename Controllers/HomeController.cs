using Assessment_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AssessmentDbContext _dbcontext;

        public HomeController(ILogger<HomeController> logger, AssessmentDbContext dbcontext)
        {
            _logger = logger;
            _dbcontext = dbcontext;
        }

        public IActionResult Index()
        {
            return View();
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
        public IActionResult AllCategory()
        {
            ViewBag.SMessage = TempData["SMessage"];
            ViewBag.EMessage = TempData["EMessage"];
            IList<ProductCategory> ICategory = _dbcontext.ProductCategories.ToList();
            return View(ICategory);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(ProductCategory cat)
        {
            try
            {
                _dbcontext.ProductCategories.Add(cat);
                _dbcontext.SaveChanges();
                TempData["SMessage"] = "Product Added Succesfully.";
            }
            catch (Exception ex)
            {
                TempData["EMessage"] = "Some Error Occuried While Adding Please Try Again!!";
            }
            return RedirectToAction(nameof(HomeController.AllCategory));
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.ListCategories = _dbcontext.ProductCategories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Product pro)
        {
            try
            {
                if (pro.ProductCategory == null)
                {
                    pro.ProductCategory = 0;
                }
                pro.CreatedBy = "System";
                pro.CreatedDate = DateTime.Now;
                _dbcontext.Products.Add(pro);
                _dbcontext.SaveChanges();
                TempData["SMessage"] = "Product Added Succesfully.";
            }
            catch (Exception ex)
            {
                TempData["EMessage"] = "Some Error Occuried While Adding Please Try Again!!";
            }
            return RedirectToAction(nameof(HomeController.AllProducts));
        }
        [HttpGet]
        public IActionResult AllProducts()
        {
            try
            {
                ViewBag.SMessage = TempData["SMessage"];
                IList<ProductView> IProduct = (from product in _dbcontext.Products
                                               from category in _dbcontext.ProductCategories.Where(m => m.Id == product.ProductCategory).DefaultIfEmpty()
                                               select new ProductView
                                               {
                                                   ProductName = product.ProductName,
                                                   ProductCode = product.ProductCode,
                                                   CategoryName = category.CategoryName,
                                                   CategoryCode = category.CategoryCode,
                                                   Brand = product.Brand,
                                                   ProductSerial = product.ProductSerial,
                                                   ProductQuantity = (double?)product.ProductQuantity ?? 0,
                                               }).ToList();
                return View(IProduct);
            }
            catch (Exception ex)
            {
                ViewBag.EMessage = TempData["EMessage"];
            }
            return View();
        }
        [HttpGet]
        public IActionResult DetailProduct(int Id)
        {
            try
            {
                Product product = _dbcontext.Products.Find(Id);
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["EMessage"] = "Some Error Occuried While Fatching Please Try Again!!";
                return View();
            }
        }
        //public ActionResult DeleteProduct(int Id)
        //{
        //    try
        //    {
        //        var objCat = _dbcontext.Products.Find(Id);
        //        if (objCat != null)
        //        {
        //            _dbcontext.Products.Remove(objCat);
        //            _dbcontext.SaveChanges();
        //            TempData["SMessage"] = "Deleted Successfully";
        //        }
        //        else
        //        {
        //            TempData["EMessage"] = "Category not found";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["EMessage"] = "Some error occured";
        //    }
        //    return RedirectToAction("Home", "DeleteProduct");
        //}

        [HttpGet]
        public JsonResult DeleteProduct()
        {
            int id = Int32.Parse(Request["Id"]);

            var myTableTest = _dbcontext.Products.Where(x => x.Id == id).FirstOrDefault();
            _dbcontext.Products.Remove(myTableTest);

            _dbcontext.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public IActionResult EditProduct(int Id)
        //{
        //    try
        //    {
        //        TempData["SMessage"] = "Product Editing Succesfully.";
        //        Product product = _dbcontext.Products.Find(Id);
        //        return View(product);
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["EMessage"] = "Some Error Occuried While Modifing Please Try Again!!";
        //        return View();
        //    }
        //}
        //[HttpPost]
        //public IActionResult EditProduct(Product product)
        //{
        //    try
        //    {
        //        product.ModifyBy = "System";
        //        product.ModifyDate = DateTime.Now;
        //        _dbcontext.Products.Update(product);
        //        _dbcontext.SaveChanges();
        //        TempData["SMessage"] = "Data Updated Successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["EMessage"] = "Some Error Occuried While Modifing Please Try Again!!";
        //    }
        //    return RedirectToAction(nameof(HomeController.AllProducts), new { product.Id });
        //}
        //public IActionResult DeleteProduct(int id)
        //{
        //    try
        //    {
        //        var objCat = _dbcontext.Products.Find(id);
        //        if (objCat != null)
        //        {
        //            _dbcontext.Products.Remove(objCat);
        //            _dbcontext.SaveChanges();
        //            TempData["SMessage"] = "Deleted Successfully";
        //        }
        //        else
        //        {
        //            TempData["EMessage"] = "Category not found";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["EMessage"] = "Some error occured";
        //    }
        //    return RedirectToAction(nameof(HomeController.AllProducts));
        //}
    }
}
