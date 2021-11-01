﻿using HRS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Core.ViewModels
{
    public class ContentChangeLogViewModel
    {
        public DateTime ChangeAt { get; set; }
        public ContentStatus Old { get; set; }
        public ContentStatus New { get; set; }
    }
}
