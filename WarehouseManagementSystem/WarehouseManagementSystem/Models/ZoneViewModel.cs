using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystem.Models
{
    public class ZoneViewModel
    {
        public int ZoneId { get; set; }
        public string Name { get; set; }
    }
}
