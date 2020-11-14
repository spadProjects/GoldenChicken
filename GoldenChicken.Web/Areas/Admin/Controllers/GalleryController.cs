using System;
using System.Net;
using System.Web.Mvc;
using GoldenChicken.Core.Models;
using GoldenChicken.Infrastructure.Repositories;
using System.Web;
using System.IO;

namespace GoldenChicken.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        private readonly GalleriesRepository _repo;
        public GalleryController(GalleriesRepository repo)
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
        public ActionResult Create(Gallery image,HttpPostedFileBase GalleryImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (GalleryImage != null)
                {
                    var newFileName = Guid.NewGuid() + Path.GetExtension(GalleryImage.FileName);
                    GalleryImage.SaveAs(Server.MapPath("/Files/GalleryImages/" + newFileName));
                    image.Image = newFileName;
                }
                #endregion

                _repo.Add(image);
                return RedirectToAction("Index");
            }

            return View(image);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery image = _repo.Get(id.Value);
            if (image == null)
            {
                return HttpNotFound();
            }
            return PartialView(image);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Gallery gallery, HttpPostedFileBase GalleryImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (GalleryImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/" + gallery.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/" + gallery.Image));

                    var newFileName = Guid.NewGuid() + Path.GetExtension(GalleryImage.FileName);
                    GalleryImage.SaveAs(Server.MapPath("/Files/GalleryImages/" + newFileName));
                    gallery.Image = newFileName;
                }
                #endregion

                _repo.Update(gallery);
                return RedirectToAction("Index");
            }
            return View(gallery);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery image = _repo.Get(id.Value);
            if (image == null)
            {
                return HttpNotFound();
            }
            return PartialView(image);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var image = _repo.Get(id);

            //#region Delete Image
            //if (image.Image != null)
            //{
            //    if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/" + image.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/" + image.Image));

            //    if (System.IO.File.Exists(Server.MapPath("/Files/GalleryImages/" + image.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/GalleryImages/" + image.Image));
            //}
            //#endregion

            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}