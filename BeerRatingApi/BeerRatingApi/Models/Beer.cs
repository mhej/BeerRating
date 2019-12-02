using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeerRatingApi.Models
{
    public class Beer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string Name { get; set; }
        [StringLength(25)]
        public string Style { get; set; }
        [StringLength(25)]
        public string Brewery { get; set; }
        [StringLength(25)]
        public string Country { get; set; }
        [Required]
        public int Rating { get; set; }
        public string Image { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        public string UserId { get; set; }
    }
}