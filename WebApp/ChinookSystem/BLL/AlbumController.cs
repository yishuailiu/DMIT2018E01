using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using ChinookSystem.DAL;
using ChinookSystem.Data.Entities;
using DMIT2018Common.UserControls;
using ChinookSystem.Data.POCOs;
using ChinookSystem.Data.DTOs;

namespace ChinookSystem.BLL
{
    [DataObject]
    public class AlbumController
    {
        #region CLass variables
        List<string> reasons = new List<string>();

        #endregion

        #region query
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Album_List()
        {
            using (var context = new ChinookContext())
            {
                return context.Albums.ToList();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Album Album_Get(int Albumid)
        {
            using (var context = new ChinookContext())
            {
                return context.Albums.Find(Albumid);
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
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
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<AlbumsOfArtist> Album_AlbumsOfArtist(string artistname)
        {
            using(var context = new ChinookContext())
            {
                var results =  from x in context.Albums
                                            where x.Artist.Name.Contains(artistname)
                                            orderby x.ReleaseYear, x.Title
                                            select new AlbumsOfArtist
                                            {
                                                Title = x.Title,
                                                ArtistName = x.Artist.Name,
                                                Year = x.ReleaseYear,
                                                Rlabel = x.ReleaseLabel
                                            };
                return results.ToList();
            }
        }

        #endregion

        #region Add,Update,Delete
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Album_Add(Album item)
        {
            if (CheckReleaseYear(item))
            {
                using (var context = new ChinookContext())
                {
                    context.Albums.Add(item);
                    context.SaveChanges();
                    return item.AlbumId;
                }
            }
            else
            {
                throw new BusinessRuleException("Validation Error", reasons);
                
            }
            
        }
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public int Album_Update(Album item)
        {
            using(var context = new ChinookContext())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                return context.SaveChanges();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public int Album_Delete(Album item)
        {
            return Album_Delete(item.AlbumId);
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
        //support methods
        private bool CheckReleaseYear(Album item)
        {
            bool isValid = true;
            int releaseyear;
            if (string.IsNullOrEmpty(item.ReleaseYear.ToString()))
            {
                isValid = false;
                reasons.Add("Reales Year is required");
            }
            else if (!int.TryParse(item.ReleaseYear.ToString(),out releaseyear))
            {
                isValid = false;
                reasons.Add("Reales Year is not a number");
            }
            else if (releaseyear < 1950 || releaseyear > DateTime.Today.Year)
            {
                isValid = false;
                reasons.Add(string.Format("Album release year of {0} invalid.year between 1950-2050", releaseyear));
            }
            return isValid;


        }

        public List<AlbumDTO> Album_AlbumAndTracks()
        {

        }
    }
}
