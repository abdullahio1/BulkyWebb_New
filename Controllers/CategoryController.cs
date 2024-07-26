using Microsoft.AspNetCore.Mvc;
using BulkyWebb_New.Models;
using BulkyWebb_New.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace BulkyWebb_New.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category categoryFromDb =  _unitOfWork.Category.Get(c => c.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
               _unitOfWork.Save();
                TempData["Success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category categoryFromDb = _unitOfWork.Category.Get(c => c.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            Category obj =  _unitOfWork.Category.Get(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}

// namespace BulkyWebb_New.Controllers
// {
//      public class CategoryController : Controller
//     {
//         private readonly ICategoryRepository _categoryRepo;

//         public CategoryController(ICategoryRepository categoryRepo)
//         {
//             _categoryRepo = categoryRepo;
//         }

//         public IActionResult Index()
//         {
//             List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
//             return View(objCategoryList);
//         }

//         public IActionResult Create()
//         {
//             return View();
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public IActionResult Create(Category obj)
//         {
//             if (obj.Name == obj.DisplayOrder.ToString())
//             {
//                 ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
//             }

//             if (ModelState.IsValid)
//             {
//                 _categoryRepo.Add(obj);
//                 _categoryRepo.Save();
//                 TempData["Success"] = "Category created successfully";
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

//             Category categoryFromDb = _categoryRepo.Get(c => c.Id == id);
//             if (categoryFromDb == null)
//             {
//                 return NotFound();
//             }

//             return View(categoryFromDb);
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public IActionResult Edit(Category obj)
//         {
//             if (ModelState.IsValid)
//             {
//                 _categoryRepo.Update(obj);
//                 _categoryRepo.Save();
//                 TempData["Success"] = "Category updated successfully";
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

//             Category categoryFromDb = _categoryRepo.Get(c => c.Id == id);
//             if (categoryFromDb == null)
//             {
//                 return NotFound();
//             }

//             return View(categoryFromDb);
//         }

//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public IActionResult DeletePost(int? id)
//         {
//             Category obj = _categoryRepo.Get(c => c.Id == id);
//             if (obj == null)
//             {
//                 return NotFound();
//             }

//             _categoryRepo.Remove(obj);
//             _categoryRepo.Save();
//             TempData["Success"] = "Category deleted successfully";
//             return RedirectToAction("Index");
//         }
//     }
// public class CategoryController : Controller
//   {
//     private readonly ICategoryRepository _categoryRepo;
//    internal CategoryController(ICategoryRepository db)
//     {
//         _categoryRepo = db;
//     }
//     public IActionResult Index()
//     {
//         List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
//         return View(objCategoryList);
//     }

//     public IActionResult Create()
//     {
//         return View();
//     }
//     [HttpPost]
//     public IActionResult Create(Category obj)
//     {
//         if(obj.Name == obj.DisplayOrder.ToString())
//         {
//             ModelState.AddModelError("Name","The DisplayOrder cannot exactly match the Name."); // NOT WORKING also All not working
//         }
//         if (ModelState.IsValid)
//         {
//             _categoryRepo.Add(obj);
//             _categoryRepo.Save();
//             TempData["Success"] = "Category created successfully";
//             return RedirectToAction("Index");
//         }
//            return View();
//     }
//      public IActionResult Edit(int? id)
//     {
//         if(id==null || id == 0){
//             return NotFound();
//         }
//      Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);
//         // Category? categoryFromDb = _db.Categories.Find(id);
//         // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
//         // Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
//         if(categoryFromDb == null)
//         {
//             return NotFound();
//         }
//         return View(categoryFromDb);
//     }
//     [HttpPost]
//      public IActionResult Edit(Category obj)
//     {
    
//         if (ModelState.IsValid)
//         {
//            _categoryRepo.Update(obj);
//             _categoryRepo.Save(); 
//             TempData["Success"] = "Category updated successfully";
//             return RedirectToAction("Index");
//         }
//            return View();
//     }
//      public IActionResult Delete(int? id)
//     {
//         if(id==null || id == 0){
//             return NotFound();
//         }
//         Category? categoryFromDb = _categoryRepo.Get(u=>u.Id==id);
//         // Category? categoryFromDb = _db.Categories.Find(id);
//         // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
//         // Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
//         if(categoryFromDb == null)
//         {
//             return NotFound();
//         }
//         return View(categoryFromDb);
//     }
//     [HttpPost, ActionName("Delete")]
//      public IActionResult DeletePost(int? id)
//     {
//             // Category? obj = _db.Categories.Find(id);
//             Category? obj = _categoryRepo.Get(u => u.Id == id);
//         if(obj == null)
//         {
//             return NotFound();
//         }
//         _categoryRepo.Remove(obj);
//         _categoryRepo.Save();
//         TempData["Success"] = "Category Deleted successfully";
//         return RedirectToAction("Index");
//     }
   
// }
