using System.Collections.Generic;
using EKContent.bus.Entities;

namespace EKContent.web.Models.ViewModels
{
    public class MemberIndexViewModel : BaseViewModel
    {
        public List<EKRole> Roles { get; set; }
    }
}