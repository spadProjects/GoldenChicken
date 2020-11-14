using System;
using System.Net;
using System.Web.Mvc;
using GoldenChicken.Core.Models;
using GoldenChicken.Infrastructure.Repositories;

namespace GoldenChicken.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class FoodTypesController : Controller
    {
        private readonly FoodTypesRepository _repo;
        public FoodTypesController(FoodTypesRepository repo)
        {
            _repo = repo;
        }
        // GET: Admin/FoodTypes
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        // GET: Admin/FoodTypes/Create
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodType foodType)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(foodType);
                return RedirectToAction("Index");
            }

            return View(foodType);
        }

        // GET: Admin/FoodTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodType foodType = _repo.Get(id.Value);
            if (foodType == null)
            {
                return HttpNotFound();
            }
            return PartialView(foodType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FoodType foodType)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(foodType);
                return RedirectToAction("Index");
            }
            return View(foodType);
        }

        // GET: Admin/FoodTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodType foodType = _repo.Get(id.Value);
            if (foodType == null)
            {
                return HttpNotFound();
            }
            return PartialView(foodType);
        }

        // POST: Admin/FoodTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
