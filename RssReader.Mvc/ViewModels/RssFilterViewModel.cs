using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RssReader.Mvc.ViewModels
{
    public class RssFilterViewModel
    {
        [Required]
        public string Source { get; set; }

        [Required]
        public int Sort { get; set; }
    }
}