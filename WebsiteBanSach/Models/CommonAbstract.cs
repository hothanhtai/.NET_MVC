﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanSach.Models.EF
{
    public class CommonAbstract
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifierDate { get; set; }

        public string ModifierBy { get; set; }


    }
}