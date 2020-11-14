using System;
using System.Net;
using System.Web.Mvc;
using GoldenChicken.Core.Models;
using GoldenChicken.Infrastructure.Repositories;

namespace GoldenChicken.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class TestimonialsController : Controller
    {
        private readonly TestimonialsRepository _repo;
        public TestimonialsController(TestimonialsRepository repo)
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
        public ActionResult Create(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(testimonial);
                return RedirectToAction("Index");
            }

            return View(testimonial);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial testimonial = _repo.Get(id.Value);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return PartialView(testimonial);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(testimonial);
                return RedirectToAction("Index");
            }
            return View(testimonial);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial testimonial = _repo.Get(id.Value);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return PartialView(testimonial);
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