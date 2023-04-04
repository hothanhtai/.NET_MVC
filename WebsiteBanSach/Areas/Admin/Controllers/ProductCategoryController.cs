using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;
using WebsiteBanSach.Models.EF;

namespace WebsiteBanSach.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
        private ApplicationDbContext dbConect = new ApplicationDbContext();

        // GET: Admin/ProductCategory
        public ActionResult Index(int? page)
        {
            var items = dbConect.ProductCategories;
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifierDate = DateTime.Now;
                model.Alias = WebsiteBanSach.Models.Common.Filter.FilterChar(model.Title);
                dbConect.ProductCategories.Add(model);
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var item = dbConect.ProductCategories.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory model)
        {
            if (ModelState.IsValid)
            {

                model.ModifierDate = DateTime.Now;
                dbConect.ProductCategories.Attach(model);
                dbConect.Entry(model).State = System.Data.Entity.EntityState.Modified;
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = dbConect.ProductCategories.Find(id);
            if (item != null)
            {
                dbConect.ProductCategories.Remove(item);
                dbConect.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var ojb = dbConect.ProductCategories.Find(int.Parse(item));
                        dbConect.ProductCategories.Remove(ojb);
                        dbConect.SaveChanges();
                    }
                }
                return Json(new { success = true });

            }
            return Json(new { success = false });
        }
    }
}