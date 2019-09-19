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
        #region query
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
        #endregion

        #region Add,Update,Delete
        public int Album_Add(Album item)
        {
            using(var context = new ChinookContext())
            {
                context.Albums.Add(item);
                context.SaveChanges();
                return item.AlbumId;
            }
        }
        public int Album_Update(Album item)
        {
            using(var context = new ChinookContext())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                return context.SaveChanges();
            }
        }
        public int Album_Delete(int albumid)
        {
            using(var context = new ChinookContext())
            {
                var existing = context.Albums.Find(albumid);
                if (existing ==null)
                {
                    throw new Exception("Album not find");
                }
                else
                {
                    context.Albums.Remove(existing);
                    return context.SaveChanges();
                }
            }
        }

        #endregion 
    }
}
