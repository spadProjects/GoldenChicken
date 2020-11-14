using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoldenChicken.Infrastructure.Repositories;
using GoldenChicken.Core.Models;
using System.Net;
using System.IO;

namespace GoldenChicken.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class FoodGalleriesController : Controller
    {
        private readonly FoodGalleriesRepository _repo;
        public FoodGalleriesController(FoodGalleriesRepository repo)
        {
            _repo = repo;
        }
        public ActionResult Index(int foodId)
        {
            ViewBag.FoodName = _repo.GetFoodName(foodId);
            ViewBag.FoodId = foodId;
            return View(_repo.GetFoodGalleries(foodId));
        }

        public ActionResult Create(int foodId)
        {
            ViewBag.FoodId = foodId;
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodGallery foodGallery,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (Image != null)
                {
                    var newFileName = Guid.NewGuid() + Path.GetExtension(Image.FileName);
                    Image.SaveAs(Server.MapPath("/Files/FoodImages/FoodGallery/" + newFileName));
                    foodGallery.Image = newFileName;
                }
                #endregion
                _repo.Add(foodGallery);
                return RedirectToAction("Index", new { foodId = foodGallery.FoodId });
            }
            ViewBag.FoodId = foodGallery.FoodId;
            return View(foodGallery);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodGallery foodGallery = _repo.Get(id.Value);
            if (foodGallery == null)
            {
                return HttpNotFound();
            }
            return PartialView(foodGallery);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FoodGallery foodGallery,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (Image != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/FoodImages/FoodGallery/" + foodGallery.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/FoodImages/FoodGallery/" + foodGallery.Image));

                    var newFileName = Guid.NewGuid() + Path.GetExtension(Image.FileName);
                    Image.SaveAs(Server.MapPath("/Files/FoodImages/FoodGallery/" + newFileName));
                    foodGallery.Image = newFileName;
                }
                #endregion
                _repo.Update(foodGallery);
                return RedirectToAction("Index", new { foodId = foodGallery.FoodId });
            }
            return View(foodGallery);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodGallery foodGallery = _repo.Get(id.Value);
            if (foodGallery == null)
            {
                return HttpNotFound();
            }
            return PartialView(foodGallery);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var foodId = _repo.Get(id).FoodId;
            _repo.Delete(id);
            return RedirectToAction("Index", new { foodId });
        }
    }
}