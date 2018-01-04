using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class News
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        public string Url { get; set; }

        public long RssSourceId { get; set; }
        public RssSource RssSource { get; set; }
    }
}
