using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class RssSource
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<News> News { get; set; }

        public RssSource()
        {
            News = new List<News>();
        }
    }
}
