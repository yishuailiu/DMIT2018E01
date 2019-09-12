using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChinookSystem.DAL;
using ChinookSystem.Data.Entities;

namespace ChinookSystem.BLL
{
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
    }
}
