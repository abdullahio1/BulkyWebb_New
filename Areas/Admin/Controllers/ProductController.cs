using Microsoft.AspNetCore.Mvc;
using BulkyWebb_New.Data;
using BulkyWebb_New.Models;
using Microsoft.AspNetCore.Authorization;
using BulkyWebb_New.Repository.IRepository;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using BulkyWebb_New.Models.ViewModels;
using System.IO; // Ensure you have this using directive for Path
using Microsoft.AspNetCore.Hosting; // Ensure you have this using directive for IWebHostEnvironment
using System.Data;


namespace BulkyWebb_New.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
        //  _webHostEnvironment = webHostEnvironment;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(IncludeProperties:"Category").ToList();
            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            ProductViewModel productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select( u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
              //Create
              return View(productVM);
            }
            else
            {
                //Edit
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.Name);
                    string productPath = Path.Combine(wwwRootPath, @"images/product");

                    if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        // delete the old image
                        var oldImagePath = 
                        Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('/'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // save file
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"/images/product/" + fileName;
                }
                if (productVM.Product.Id == 0)
                {
                    // Create
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    // Edit
                    _unitOfWork.Product.Update(productVM.Product);
                }
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["Success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
               productVM.CategoryList = _unitOfWork.Category.GetAll().Select( u=> new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            return View(productVM);
            }
        }
        
        // public IActionResult Edit(int? id)
        // {
        //     if (id == null || id == 0)
        //     {
        //         return NotFound();
        //     }

        //     Product ProductFromDb =  _unitOfWork.Product.Get(c => c.Id == id);
        //     if (ProductFromDb == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(ProductFromDb);
        // }

        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public IActionResult Edit(Product obj)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _unitOfWork.Product.Update(obj);
        //        _unitOfWork.Save();
        //         TempData["Success"] = "Product updated successfully";
        //         return RedirectToAction("Index");
        //     }

        //     return View();
        // }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product ProductFromDb = _unitOfWork.Product.Get(c => c.Id == id);
            if (ProductFromDb == null)
            {
                return NotFound();
            }

            return View(ProductFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            Product obj =  _unitOfWork.Product.Get(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}

// namespace BulkyWebb_New.Controllers
// {
//      public class ProductController : Controller
//     {
//         private readonly IProductRepository _ProductRepo;

//         public ProductController(IProductRepository ProductRepo)
//         {
//             _ProductRepo = ProductRepo;
//         }

//         public IActionResult Index()
//         {
//             List<Product> objProductList = _ProductRepo.GetAll().ToList();
//             return View(objProductList);
//         }

//         public IActionResult Create()
//         {
//             return View();
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public IActionResult Create(Product obj)
//         {
//             if (obj.Name == obj.DisplayOrder.ToString())
//             {
//                 ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
//             }

//             if (ModelState.IsValid)
//             {
//                 _ProductRepo.Add(obj);
//                 _ProductRepo.Save();
//                 TempData["Success"] = "Product created successfully";
//                 return RedirectToAction("Index");
//             }

//             return View();
//         }

//         public IActionResult Edit(int? id)
//         {
//             if (id == null || id == 0)
//             {
//                 return NotFound();
//             }

//             Product ProductFromDb = _ProductRepo.Get(c => c.Id == id);
//             if (ProductFromDb == null)
//             {
//                 return NotFound();
//             }

//             return View(ProductFromDb);
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public IActionResult Edit(Product obj)
//         {
//             if (ModelState.IsValid)
//             {
//                 _ProductRepo.Update(obj);
//                 _ProductRepo.Save();
//                 TempData["Success"] = "Product updated successfully";
//                 return RedirectToAction("Index");
//             }

//             return View();
//         }

//         public IActionResult Delete(int? id)
//         {
//             if (id == null || id == 0)
//             {
//                 return NotFound();
//             }

//             Product ProductFromDb = _ProductRepo.Get(c => c.Id == id);
//             if (ProductFromDb == null)
//             {
//                 return NotFound();
//             }

//             return View(ProductFromDb);
//         }

//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public IActionResult DeletePost(int? id)
//         {
//             Product obj = _ProductRepo.Get(c => c.Id == id);
//             if (obj == null)
//             {
//                 return NotFound();
//             }

//             _ProductRepo.Remove(obj);
//             _ProductRepo.Save();
//             TempData["Success"] = "Product deleted successfully";
//             return RedirectToAction("Index");
//         }
//     }
// public class ProductController : Controller
//   {
//     private readonly IProductRepository _ProductRepo;
//    internal ProductController(IProductRepository db)
//     {
//         _ProductRepo = db;
//     }
//     public IActionResult Index()
//     {
//         List<Product> objProductList = _ProductRepo.GetAll().ToList();
//         return View(objProductList);
//     }

//     public IActionResult Create()
//     {
//         return View();
//     }
//     [HttpPost]
//     public IActionResult Create(Product obj)
//     {
//         if(obj.Name == obj.DisplayOrder.ToString())
//         {
//             ModelState.AddModelError("Name","The DisplayOrder cannot exactly match the Name."); // NOT WORKING also All not working
//         }
//         if (ModelState.IsValid)
//         {
//             _ProductRepo.Add(obj);
//             _ProductRepo.Save();
//             TempData["Success"] = "Product created successfully";
//             return RedirectToAction("Index");
//         }
//            return View();
//     }
//      public IActionResult Edit(int? id)
//     {
//         if(id==null || id == 0){
//             return NotFound();
//         }
//      Product? ProductFromDb = _ProductRepo.Get(u => u.Id == id);
//         // Product? ProductFromDb = _db.Categories.Find(id);
//         // Product? ProductFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
//         // Product? ProductFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
//         if(ProductFromDb == null)
//         {
//             return NotFound();
//         }
//         return View(ProductFromDb);
//     }
//     [HttpPost]
//      public IActionResult Edit(Product obj)
//     {
    
//         if (ModelState.IsValid)
//         {
//            _ProductRepo.Update(obj);
//             _ProductRepo.Save(); 
//             TempData["Success"] = "Product updated successfully";
//             return RedirectToAction("Index");
//         }
//            return View();
//     }
//      public IActionResult Delete(int? id)
//     {
//         if(id==null || id == 0){
//             return NotFound();
//         }
//         Product? ProductFromDb = _ProductRepo.Get(u=>u.Id==id);
//         // Product? ProductFromDb = _db.Categories.Find(id);
//         // Product? ProductFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
//         // Product? ProductFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
//         if(ProductFromDb == null)
//         {
//             return NotFound();
//         }
//         return View(ProductFromDb);
//     }
//     [HttpPost, ActionName("Delete")]
//      public IActionResult DeletePost(int? id)
//     {
//             // Product? obj = _db.Categories.Find(id);
//             Product? obj = _ProductRepo.Get(u => u.Id == id);
//         if(obj == null)
//         {
//             return NotFound();
//         }
//         _ProductRepo.Remove(obj);
//         _ProductRepo.Save();
//         TempData["Success"] = "Product Deleted successfully";
//         return RedirectToAction("Index");
//     }
   
// }
