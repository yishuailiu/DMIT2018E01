using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChinookSystem.DAL;
using ChinookSystem.Data.Entities;
using System.ComponentModel;

namespace ChinookSystem.BLL
{
    [DataObject]
    public class ArtistController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
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
