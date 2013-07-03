﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EKContent.bus.Entities;

namespace EKContent.web.Models.ViewModels
{
    public class FileListViewModel : BaseViewModel
    {
        public List<EKFile> Files { get; set; }
    }
}