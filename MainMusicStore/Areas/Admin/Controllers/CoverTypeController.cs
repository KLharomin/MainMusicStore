using Dapper;
using MainMusicStore.DataAccess.IMainRepository;
using MainMusicStore.Models.DbModels;
using MainMusicStore.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace MainMusicStore.Areas.Admin.Controllers
{
    [Area("Admin")] //hangi area da olduğu belirtilir.
    public class CoverTypeController : Controller
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region CTOR
        public CoverTypeController(IUnitOfWork unitOfWork)
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
            var allCoverTypes = _unitOfWork.SPCall.List<CoverType>(ProjectConstant.Proc_CoverType_GetAll, null);
            return Json(new { data = allCoverTypes });
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //var deleteData = _unitOfWork.CoverType.GetById(id);
            //if (deleteData == null)
            //    return Json(new { success = false, message = "Data Not Found" });

            //_unitOfWork.CoverType.Remove(deleteData);
            //_unitOfWork.Save();
            //return Json(new { success = true, message = "Delete Operation Successfully" });

            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);

            var deletedData = _unitOfWork.SPCall.OneRecord<CoverType>(ProjectConstant.Proc_CoverType_Get, parameter);
            if (deletedData == null)
                return Json(new { success = false, message = "Data Not Found" });

            _unitOfWork.SPCall.Execute(ProjectConstant.Proc_CoverType_Delete, parameter);
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
            CoverType CoverType = new CoverType();
            if (id == null)
            {
                //This is for insert
                return View(CoverType);
            }

            CoverType = _unitOfWork.CoverType.GetById((int)id);
            //Zorunlu cast işlemidir.Burada direk olarak id int olarak istenir getbyid kısmında
            //o kısımdaki işlemi zorunlu olarak inte çevrilmesini isteriz.
            if (CoverType != null)
            {
                return View(CoverType);
            }

            return NotFound();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType CoverType)
        {
            if (ModelState.IsValid)
            {
                if (CoverType.Id == 0)
                {
                    //Create
                    _unitOfWork.CoverType.Add(CoverType);
                }
                else
                {
                    //Update
                    _unitOfWork.CoverType.Update(CoverType);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(CoverType);
        }


    }
}