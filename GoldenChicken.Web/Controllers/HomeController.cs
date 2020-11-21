using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoldenChicken.Core.Models;
using GoldenChicken.Core.Utility;
using GoldenChicken.Infrastructure.Repositories;
using GoldenChicken.Web.ViewModels;

namespace GoldenChicken.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly StaticContentDetailsRepository _contentRepo;
        private readonly GalleriesRepository _galleryRepo;
        private readonly TestimonialsRepository _testimonialRepo;
        private readonly ContactFormsRepository _contactFormRepo;

        public HomeController(StaticContentDetailsRepository contentRepo, GalleriesRepository galleryRepo, TestimonialsRepository testimonialRepo, ContactFormsRepository contactFormRepo)
        {
            _contentRepo = contentRepo;
            _galleryRepo = galleryRepo;
            _testimonialRepo = testimonialRepo;
            _contactFormRepo = contactFormRepo;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Navbar()
        {
            ViewBag.Phone = _contentRepo.GetStaticContentDetail((int) StaticContents.Phone).ShortDescription;
            return PartialView();
        }
        public ActionResult HomeSlider()
        {
            var sliderContent = _contentRepo.GetContentByTypeId((int)StaticContentTypes.Slider);
            return PartialView(sliderContent);
        }
        public ActionResult Gallery()
        {
            var galleryContent = _galleryRepo.GetAll();
            return PartialView(galleryContent);
        }
        public ActionResult CompanyHistory()
        {
            var content = _contentRepo.GetContentByTypeId((int)StaticContentTypes.CompanyHistory).FirstOrDefault();
            return PartialView(content);
        }
        public ActionResult Testimonials()
        {
            var content = _testimonialRepo.GetAll();
            return PartialView(content);
        }
        public ActionResult GallerySlider()
        {
            var galleryContent = _galleryRepo.GetAll();
            return PartialView(galleryContent);
        }
        public ActionResult ContactUsForm()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult ContactUsForm(ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                _contactFormRepo.Add(contactForm);
                return RedirectToAction("ContactUsSummary");
            }
            return View(contactForm);
        }

        public ActionResult ContactUsSummary()
        {
            return View();
        }
        public ActionResult Footer(bool isContactUsPage)
        {
            if (isContactUsPage)
                ViewBag.ContactUsPage = true;

            var footerContent = new FooterViewModel();
            footerContent.Map = _contentRepo.Get((int) StaticContents.Map);
            footerContent.Email = _contentRepo.Get((int) StaticContents.Email);
            footerContent.Address = _contentRepo.Get((int) StaticContents.Address);
            footerContent.Phone = _contentRepo.Get((int) StaticContents.Phone);
            footerContent.Youtube = _contentRepo.Get((int) StaticContents.Youtube);
            footerContent.Instagram = _contentRepo.Get((int) StaticContents.Instagram);
            footerContent.Twitter = _contentRepo.Get((int) StaticContents.Twitter);
            footerContent.Pinterest = _contentRepo.Get((int) StaticContents.Pinterest);
            footerContent.Facebook = _contentRepo.Get((int) StaticContents.Facebook);
            return PartialView(footerContent);
        }
        [Route("Gallery")]
        public ActionResult GalleryPage()
        {
            var galleryContent = _galleryRepo.GetAll();
            return View(galleryContent);
        }
        [Route("AboutUs")]
        public ActionResult About()
        {
            return View();
        }

        [Route("ContactUs")]
        public ActionResult Contact()
        {
            ViewBag.ContactUsPage = true;
            var contactUsContent = new ContactUsViewModel();
            contactUsContent.ContactInfo = _contentRepo.Get((int)StaticContents.ContactInfo);
            contactUsContent.Email = _contentRepo.Get((int)StaticContents.Email);
            contactUsContent.Address = _contentRepo.Get((int)StaticContents.Address);
            contactUsContent.Phone = _contentRepo.Get((int)StaticContents.Phone);
            contactUsContent.Youtube = _contentRepo.Get((int)StaticContents.Youtube);
            contactUsContent.Instagram = _contentRepo.Get((int)StaticContents.Instagram);
            contactUsContent.Twitter = _contentRepo.Get((int)StaticContents.Twitter);
            contactUsContent.Pinterest = _contentRepo.Get((int)StaticContents.Pinterest);
            contactUsContent.Facebook = _contentRepo.Get((int)StaticContents.Facebook);
            return View(contactUsContent);
        }
        public ActionResult UploadImage(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string vImagePath = String.Empty;
            string vMessage = String.Empty;
            string vFilePath = String.Empty;
            string vOutput = String.Empty;
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var vFileName = DateTime.Now.ToString("yyyyMMdd-HHMMssff") +
                                    Path.GetExtension(upload.FileName).ToLower();
                    var vFolderPath = Server.MapPath("/Upload/");
                    if (!Directory.Exists(vFolderPath))
                    {
                        Directory.CreateDirectory(vFolderPath);
                    }
                    vFilePath = Path.Combine(vFolderPath, vFileName);
                    upload.SaveAs(vFilePath);
                    vImagePath = Url.Content("/Upload/" + vFileName);
                    vMessage = "Image was saved correctly";
                }
            }
            catch
            {
                vMessage = "There was an issue uploading";
            }
            vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + vImagePath + "\", \"" + vMessage + "\");</script></body></html>";
            return Content(vOutput);
        }
    }
}