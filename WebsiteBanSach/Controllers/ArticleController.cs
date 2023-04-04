using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Controllers
{
    public class ArticleController : Controller
    {
        private ApplicationDbContext dbConect = new ApplicationDbContext();

        // GET: Article
        public ActionResult Index(string alias)
        {
            var item = dbConect.Posts.FirstOrDefault(x => x.Alias == alias);
            return View(item);
        }
    }
}