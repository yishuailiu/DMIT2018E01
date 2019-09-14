using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChinookSystem.Data.Entities
{
    [Table("MediaTypes")]
    public class MediaType
    {
        [Key]
        public int MediaTypeId { get; set; }

        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _Name = null;
                }
                else
                {
                    _Name = value;
                }
            }
        }

        public virtual ICollection<Track> Tracks
        {
            get;
            set;
        }

    }
}
