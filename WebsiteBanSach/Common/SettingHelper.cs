using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Common
{
    public class SettingHelper
    {
        private static ApplicationDbContext dbConnect = new ApplicationDbContext();

        public static string GetValue(string key)
        {
            var item = dbConnect.SystemSettings.SingleOrDefault(x => x.SettingKey == key);
            if (item != null)
            {
                return item.SettingValue;
            }
            return "";
        }
    }
}