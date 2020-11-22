using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoldenChicken.Infrastructure.Repositories;
using GoldenChicken.Core.Models;
using System.Net;
using System.IO;
using GoldenChicken.Infrastructure.Helpers;

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
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        public ActionResult Create()
        {
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
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(Image.FileName);
                    Image.SaveAs(Server.MapPath("/Files/FoodImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(800, 600, true);
                    image.Resize(Server.MapPath("/Files/FoodImages/Temp/" + newFileName),
                        Server.MapPath("/Files/FoodImages/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/FoodImages/Temp/" + newFileName));

                    foodGallery.Image = newFileName;
                }
                #endregion
                _repo.Add(foodGallery);
                return RedirectToAction("Index");
            }
            //ViewBag.FoodId = foodGallery.FoodId;
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
                    if (System.IO.File.Exists(Server.MapPath("/Files/FoodImages/" + foodGallery.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/FoodImages/Temp/" + foodGallery.Image));
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(Image.FileName);
                    Image.SaveAs(Server.MapPath("/Files/FoodImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(800, 600, true);
                    image.Resize(Server.MapPath("/Files/FoodImages/Temp/" + newFileName),
                        Server.MapPath("/Files/FoodImages/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/FoodImages/Temp/" + newFileName));
                    foodGallery.Image = newFileName;
                }
                #endregion
                _repo.Update(foodGallery);
                return RedirectToAction("Index");
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
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}