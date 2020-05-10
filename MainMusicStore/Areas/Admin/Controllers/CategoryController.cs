using MainMusicStore.DataAccess.IMainRepository;
using MainMusicStore.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace MainMusicStore.Areas.Admin.Controllers
{
    [Area("Admin")] //hangi area da olduğu belirtilir.
    public class CategoryController : Controller
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region CTOR
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Actions
        public IActionResult Index()
        {

            return View();
        }
        #endregion

        #region API CALLS
        public IActionResult GetAll()
        {
            //jquery datatable verileri listelemek için json tipinde bir veri dönmelidir.
            var allObj = _unitOfWork.Category.GetAll();
            return Json(new { data = allObj });
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var deleteData = _unitOfWork.Category.GetById(id);
            if (deleteData == null)
                return Json(new { success = false, message = "Data Not Found" });
            
            _unitOfWork.Category.Remove(deleteData);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Operation Successfully" });

        }


        #endregion
        

        /// <summary>
        /// İnsert update için get işlemi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null)
            {
                //This is for insert
                return View(category);
            }

            category = _unitOfWork.Category.GetById((int)id);
            //Zorunlu cast işlemidir.Burada direk olarak id int olarak istenir getbyid kısmında
            //o kısımdaki işlemi zorunlu olarak inte çevrilmesini isteriz.
            if (category != null)
            {
                return View(category);
            }

            return NotFound();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    //Create
                    _unitOfWork.Category.Add(category);
                }
                else
                {
                    //Update
                    _unitOfWork.Category.Update(category);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(category);
        }


    }
}