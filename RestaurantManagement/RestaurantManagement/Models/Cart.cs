using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public int userId { get; set; }

        public int menuItemId { get; set; }

        public MenuItem MenuItem { get; set; }
    }
}