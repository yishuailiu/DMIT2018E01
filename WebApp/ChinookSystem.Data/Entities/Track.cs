using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChinookSystem.Data.Entities
{
    [Table("Tracks")]
    public class Track
    {
        [Key]
        public int TrackId { get; set; }
        public string Name { get; set; }
        public int? AlbumId { get; set; }
        public int MediaTypeId { get; set; }
        public int GenreId { get; set; }
        private string _Composer;
        public string Composer
        {
            get
            {
                return _Composer;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _Composer = null;
                }
                else
                {
                    _Composer = value;
                }//eof
            }
        }//eop
        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }
        public double UnitPrice { get; set; }

        public virtual MediaType MediaTypes { get; set; }
        public virtual Genre Genres { get; set; }
        public virtual Album Albums { get; set; }
    }
}
