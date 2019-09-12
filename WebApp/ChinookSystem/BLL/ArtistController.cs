using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChinookSystem.DAL;
using ChinookSystem.Data.Entities;

namespace ChinookSystem.BLL
{
    public class ArtistController
    {
        public List<Artist> Artist_List()
        {
            using (var context = new ChinookContext())
            {
                return context.Artists.ToList();
            }
        }

        public Artist Artist_Get(int artistid)
        {
            using(var context = new ChinookContext())
            {
                return context.Artists.Find(artistid);
            }
        }
    }
}
