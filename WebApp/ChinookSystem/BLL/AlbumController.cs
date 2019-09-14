using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using ChinookSystem.DAL;
using ChinookSystem.Data.Entities;

namespace ChinookSystem.BLL
{
    [DataObject]
    public class AlbumController
    {
        public List<Album> Album_List()
        {
            using (var context = new ChinookContext())
            {
                return context.Albums.ToList();
            }
        }

        public Album Album_Get(int Albumid)
        {
            using (var context = new ChinookContext())
            {
                return context.Albums.Find(Albumid);
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<Album> Album_FindByArtist(int artistid)
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.Albums
                              where x.ArtistId == artistid
                              select x;
                return results.ToList();
            }
        }

    }
}
