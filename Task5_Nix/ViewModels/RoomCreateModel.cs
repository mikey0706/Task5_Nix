using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task5_Nix.Models;

namespace Task5_Nix.ViewModels
{
    public class RoomCreateModel
    {
        public string RoomId { get; set; }
        public int RoomNumber { get; set; }

        public string CurrentCategory { get; set; }

        public string CategoryID { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        public RoomCreateModel() { }

        public RoomCreateModel(IEnumerable<CategoryViewModel> items)
        {
            Categories = items.Select(s => new SelectListItem { Text = s.CategoryName, Value = s.CategoryId.ToString()}); 
        }

        public RoomCreateModel(IEnumerable<CategoryViewModel> items, CategoryViewModel selected)
        {
            Categories = items.Select(s => new SelectListItem { Text = s.CategoryName, Value = s.CategoryId.ToString(), Selected = (s.CategoryId==selected.CategoryId)});
            
        }
    }
}
