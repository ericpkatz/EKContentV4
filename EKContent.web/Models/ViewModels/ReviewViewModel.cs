﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EKContent.bus.Entities;

namespace EKContent.web.Models.ViewModels
{
    public class ReviewViewModel
    {
        public ContentWrapper ContentWrapper { get; set; }
        public Message Message { get; set; }
    }
}