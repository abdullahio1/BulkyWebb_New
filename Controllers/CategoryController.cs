using Microsoft.AspNetCore.Mvc;
using BulkyWebb_New.Models;
using BulkyWebb_New.Data;
using BulkyWebb_New.Repository.IRepository;
namespace BulkyWebb_New.Controllers
{
public class CategoryController : Controller
  {
    private readonly ICategoryRepository _categoryRepo;
    public CategoryController(ICategoryRepository db)
    {
        _categoryRepo = db;
    }
    public IActionResult Index()
    {
        List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
        return View(objCategoryList);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Category obj)
    {
        if(obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("Name","The DisplayOrder cannot exactly match the Name."); // NOT WORKING also All not working
        }
        if (ModelState.IsValid)
        {
            _categoryRepo.Add(obj);
            _categoryRepo.Save();
            TempData["Success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
           return View();
    }
    // public IActionResult Edit(int? id)
    //{
    //    if(id==null || id == 0){
    //        return NotFound();
    //    }
    //     Category? categoryFromDb = _categoryRepo.Get(u=>u.id==id);
    //    // Category? categoryFromDb = _db.Categories.Find(id);
    //    // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
    //    // Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
    //    if(categoryFromDb == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(categoryFromDb);
    //}
    [HttpPost]
     public IActionResult Edit(Category obj)
    {
    
        if (ModelState.IsValid)
        {
           _categoryRepo.Update(obj);
            _categoryRepo.Save(); 
            TempData["Success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }
           return View(obj);
    }
    // public IActionResult Delete(int? id)
    //{
    //    if(id==null || id == 0){
    //        return NotFound();
    //    }
    //    Category? categoryFromDb = _categoryRepo.Get(u=>u.id==id);
    //        Category? categoryFromDb = _db.Categories.Find(id);
    //        Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
    //        Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
    //        if (categoryFromDb == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(categoryFromDb);
    //}
    //[HttpPost, ActionName("Delete")]
    // public IActionResult DeletePost(int? id)
    //{
    //     Category? categoryFromDb = _categoryRepo.Get(u=>u.id==id);
    //    if(obj == null)
    //    {
    //        return NotFound();
    //    }
    //    _db.Categories.Remove(obj);
    //    _db.SaveChanges();
    //    TempData["Success"] = "Category Deleted successfully";
    //    return RedirectToAction("Index");
    //    if (ModelState.IsValid)
    //    {
    //        _categoryRepo.Remove(obj);
    //        _categoryRepo.Save(); 
    //        return RedirectToAction("Index");
    //    }
    //}
   
}
}