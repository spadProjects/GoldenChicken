using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoldenChicken.Core.Models;
using GoldenChicken.Infrastructure;
using GoldenChicken.Infrastructure.Helpers;
using GoldenChicken.Infrastructure.Repositories;
using GoldenChicken.Web.ViewModels;

namespace GoldenChicken.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class FoodsController : Controller
    {
        private readonly FoodsRepository _repo;
        public FoodsController(FoodsRepository repo)
        {
            _repo = repo;
        }
        // GET: Admin/Foods
        public ActionResult Index()
        {
            return View(_repo.GetFoods());
        }
        // GET: Admin/Foods/Create
        public ActionResult Create()
        {
            ViewBag.FoodTypeId = new SelectList(_repo.GetFoodTypes(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Food food, HttpPostedFileBase FoodImage, string Tags)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (FoodImage != null)
                {
                    var newFileName = Guid.NewGuid() + Path.GetExtension(FoodImage.FileName);
                    FoodImage.SaveAs(Server.MapPath("/Files/FoodImages/Image/" + newFileName));

                    ImageResizer thumb = new ImageResizer();
                    thumb.Resize(Server.MapPath("/Files/FoodImages/Image/" + newFileName),
                        Server.MapPath("/Files/FoodImages/Thumb/" + newFileName));

                    food.Image = newFileName;
                }
                #endregion

                _repo.Add(food);
                return RedirectToAction("Index");
            }
            ViewBag.Tags = Tags;
            ViewBag.FoodTypeId = new SelectList(_repo.GetFoodTypes(), "Id", "Title", food.FoodTypeId);
            return View(food);
        }

        // GET: Admin/Foods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = _repo.Get(id.Value);
            if (food == null)
            {
                return HttpNotFound();
            }
            ViewBag.FoodTypeId = new SelectList(_repo.GetFoodTypes(), "Id", "Title", food.FoodTypeId);
            return View(food);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Food food, HttpPostedFileBase FoodImage, string Tags)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (FoodImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/FoodImages/Image/" + food.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/FoodImages/Image/" + food.Image));

                    if (System.IO.File.Exists(Server.MapPath("/Files/FoodImages/Thumb/" + food.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/FoodImages/Thumb/" + food.Image));

                    var newFileName = Guid.NewGuid() + Path.GetExtension(FoodImage.FileName);
                    FoodImage.SaveAs(Server.MapPath("/Files/FoodImages/Image/" + newFileName));

                    ImageResizer thumb = new ImageResizer();
                    thumb.Resize(Server.MapPath("/Files/FoodImages/Image/" + newFileName), Server.MapPath("/Files/FoodImages/Thumb/" + newFileName));
                    food.Image = newFileName;
                }
                #endregion

                _repo.Update(food);
                return RedirectToAction("Index");
            }
            ViewBag.Tags = Tags;
            ViewBag.FoodTypeId = new SelectList(_repo.GetFoodTypes(), "Id", "Title", food.FoodTypeId);
            return View(food);
        }

        [HttpPost]
        public ActionResult FileUpload()
        {
            var files = HttpContext.Request.Files;
            foreach (var fileName in files)
            {
                HttpPostedFileBase file = Request.Files[fileName.ToString()];
                var newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("/Files/FoodImages/" + newFileName));
                TempData["UploadedFile"] = newFileName;
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        // GET: Admin/Foods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = _repo.Get(id.Value);
            if (food == null)
            {
                return HttpNotFound();
            }
            return PartialView(food);
        }

        // POST: Admin/Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var food = _repo.Get(id);

            //#region Delete Food Image
            //if (food.Image != null)
            //{
            //    if (System.IO.File.Exists(Server.MapPath("/Files/FoodImages/Image/" + food.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/FoodImages/Image/" + food.Image));

            //    if (System.IO.File.Exists(Server.MapPath("/Files/FoodImages/Thumb/" + food.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/FoodImages/Thumb/" + food.Image));
            //}
            //#endregion

            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
