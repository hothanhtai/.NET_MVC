using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;
using WebsiteBanSach.Models.EF;

namespace WebsiteBanSach.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Staff")]

    public class NewsController : Controller
    {
        private ApplicationDbContext dbConect = new ApplicationDbContext();

        // GET: Admin/News 
        //public ActionResult Index(string Searchtext , int? page)
        //{
        //    var pageSize = 5;
        //    if(page == null)
        //    {
        //        page = 1;
        //    }
        //    IEnumerable<News> items = dbConect.News.OrderByDescending(x => x.Id);
        //    if (!string.IsNullOrEmpty(Searchtext))
        //    {
        //       items = items.Where(x =>x.Alias.Contains(Searchtext) || x.Title.Contains(Searchtext)).ToList();
        //    }
        //    var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1 ;
        //    items = items.ToPagedList(pageIndex,pageSize);
        //    ViewBag.PageSize = pageSize;
        //    ViewBag.Page = page;
        //    return View(items);
        //}
        public ActionResult Index(string Searchtext, int? page)
        {
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }

            IEnumerable<News> items = dbConect.News.OrderByDescending(x => x.Id);

            if (!string.IsNullOrEmpty(Searchtext))
            {
                Searchtext = Regex.Replace(Searchtext.ToLower(), @"\p{IsCombiningDiacriticalMarks}+", string.Empty);
                items = items.Where(x => Regex.Replace(x.Alias.ToLower(), @"\p{IsCombiningDiacriticalMarks}+", string.Empty).Contains(Searchtext) ||
                                         Regex.Replace(x.Title.ToLower(), @"\p{IsCombiningDiacriticalMarks}+", string.Empty).Contains(Searchtext));
            }

            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;

            return View(items);
        }






        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(News model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifierDate = DateTime.Now;
                model.CategoryId = 8;
                model.Alias = WebsiteBanSach.Models.Common.Filter.FilterChar(model.Title);
                dbConect.News.Add(model);
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var item = dbConect.News.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News model)
        {
            if (ModelState.IsValid)
            {
              
                model.ModifierDate = DateTime.Now;
               
                model.Alias = WebsiteBanSach.Models.Common.Filter.FilterChar(model.Title);
                dbConect.News.Attach(model);
                dbConect.Entry(model).State = System.Data.Entity.EntityState.Modified;
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = dbConect.News.Find(id);
            if (item != null)
            {
                dbConect.News.Remove(item);
                dbConect.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = dbConect.News.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                dbConect.Entry(item).State = System.Data.Entity.EntityState.Modified;
                dbConect.SaveChanges();
                return Json(new { success = true, IsActive = item.IsActive});
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if(items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var ojb = dbConect.News.Find(int.Parse(item));
                        dbConect.News.Remove(ojb);
                        dbConect.SaveChanges();
                    }
                }
                return Json(new { success = true });

            }
            return Json(new { success = false });
        }
    }
}