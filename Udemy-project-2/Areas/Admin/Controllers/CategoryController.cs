
using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using utility;

namespace Udemy_project_2.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {    //Unit OF Work


        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
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
            category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);


            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(category obj)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";

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
            category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);


            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
        }



        ////replace applicationDb with category Repository 
        //// private readonly ApplicationDbContext _db;

        //private readonly ICategoryRepository _categoryRepository;

        //public CategoryController(ICategoryRepository db)
        //{
        //    _categoryRepository = db;
        //}
        //public IActionResult Index()
        //{

        //   // List<category> objCategoryList = _db.Categories.ToList();
        //    List<category> objCategoryList = _categoryRepository.GetAll().ToList();

        //    return View(objCategoryList);
        //}


        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Create(category obj)
        //{
        //    if (obj.Name == obj.DisplayOrder.ToString())
        //    {
        //        ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        //    }
        //    if (ModelState.IsValid)
        //    {

        //        //  _db.Categories.Add(obj);
        //        //   _db.SaveChanges();
        //        _categoryRepository.Add(obj);
        //        _categoryRepository.Save();
        //        TempData["success"] = "Category created successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View();

        //}

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    //  category? categoryFromDb = _db.Categories.Find(id);
        //    category? categoryFromDb = _categoryRepository.Get(u=>u.Id==id);

        //    //category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
        //    //category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

        //    if (categoryFromDb == null)
        //    {   
        //        return NotFound();
        //    }
        //    return View(categoryFromDb);
        //}


        //[HttpPost]
        //public IActionResult Edit(category obj)
        //{

        //if (ModelState.IsValid)
        //    {
        //        //_db.Categories.Update(obj);
        //        //_db.SaveChanges();
        //        _categoryRepository.Update(obj);
        //        _categoryRepository.Save();
        //        TempData["success"] = "Category updated successfully";

        //        return RedirectToAction("Index");
        //    }
        //    return View();

        //}
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    //  category? categoryFromDb = _db.Categories.Find(id);
        //    category? categoryFromDb = _categoryRepository.Get(u => u.Id == id);

        //    //category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
        //    //category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

        //    if (categoryFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(categoryFromDb);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? id)
        //{
        //   category? obj = _categoryRepository.Get(u => u.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    //_db.Categories.Remove(obj);
        //    //_db.SaveChanges();
        //    _categoryRepository.Remove(obj);
        //    _categoryRepository.Save();
        //    TempData["success"] = "Category deleted successfully";

        //    return RedirectToAction("Index");
        //}

    }
}
