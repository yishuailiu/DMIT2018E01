﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.DTOs;
using ChinookSystem.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
using DMIT2018Common.UserControls;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookContext())
            {
                //what would happen if there is no match for the
                //   incoming parameter value
                //we need to ensure that the results have a valid value
                //this value will need to resolve to either a null OR
                //  an IEnumerable<T> collection
                //to achieve a valid value you will need to determine
                // using .FirstOrDefault() whether data exists or not
                var results = (from x in context.Playlists
                               where x.UserName.Equals(username)
                                && x.Name.Equals(playlistname)
                               select x).FirstOrDefault();

                //if the playlist does NOT exist .FirstOrDefault returns null
                if (results == null)
                {
                    return null;
                }
                else
                {
                    //if the playlist does exists, query for the playlist tracks
                    var theTracks = from x in context.PlaylistTracks
                                    where x.PlaylistId.Equals(results.PlaylistId)
                                    orderby x.TrackNumber
                                    select new UserPlaylistTrack
                                    {
                                        TrackID = x.TrackId,
                                        TrackNumber = x.TrackNumber,
                                        TrackName = x.Track.Name,
                                        Milliseconds = x.Track.Milliseconds,
                                        UnitPrice = x.Track.UnitPrice
                                    };
                    return theTracks.ToList();
                }
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            using (var context = new ChinookContext())
            {
                //code to go here
                //use the businessRuleException to throw errors to the web page
                List<string> reasons = new List<string>();
                PlaylistTrack newTrack = null;
                int tracknumber = 0;
                //Part One
                //determine if the playlist exists
                //query the table using the playlistname and username
                //if the playlist exist,one will get
                //
                //
                Playlist exists = context.Playlists
                    .Where(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && x.Name.Equals(playlistname, StringComparison.OrdinalIgnoreCase))
                    .Select(x => x).FirstOrDefault();
                //does the playlist exist
                if (exists == null)
                {
                    // this is a new playlist 
                    //need to create the playlist record
                    exists = new Playlist();
                    exists.Name = playlistname;
                    exists.UserName = username;
                    //stage the add
                    exists = context.Playlists.Add(exists);
                    //since this is new playlist
                    //the trucknumber will be 1
                    tracknumber = 1;
                }
                else
                {
                    //since the playlist exists, so may the t rack exist on t he playlisttracks
                    newTrack = exists.PlaylistTracks.SingleOrDefault(
                        x => x.TrackId == trackid);
                    if (newTrack == null)
                    {
                        tracknumber = exists.PlaylistTracks.Count() + 1;
                    }
                    else
                    {
                        reasons.Add("Track already exists on playlist");
                    }
                }

                //Part Two
                //create the playlistTrack entry
                //if there are anyreasons NOT to create then
                //throw the BusinessRuleException
                if (reasons.Count() > 0)
                {
                    //issue with adding the track
                    throw new BusinessRuleException("Adding track to playlist", reasons);
                }
                else
                {
                    //use the PlayList navigation to PlaylistTracks to do the add to PlayListTracks
                    newTrack = new PlaylistTrack();
                    newTrack.TrackId = trackid;
                    newTrack.TrackNumber = tracknumber;

                    //how to fill the PlayListId if the playlist is brand new?
                    //a brand new playlist DOES NOT YET have an id
                    //Note: the pkey for PlaylistID may not yet exist
                    //  using the navigation property one the PlayList entity
                    //  one can let HasSet handle the PlaylistId pkey value to be properly created on PlayList AND placed correctly into the child record of the PlayListTracks

                    //what is wrong to the attempt:
                    //  newTrack.PlaylistId = exists.PlayListId;
                    exists.PlaylistTracks.Add(newTrack); //playlist track staging

                    //physically add any/all data to the databse (do the commit)
                    context.SaveChanges();

                }
             
            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookContext())
            {
                //get playlistID
                var exists = (from x in context.Playlists
                              where x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && x.Name.Equals(playlistname,StringComparison.OrdinalIgnoreCase)
                              select x
                              ).FirstOrDefault();
                if (exists == null)
                {
                    throw new Exception("Playlist does not exists.");
                }
                else
                {
                    PlaylistTrack moveTrack = (from x in exists.PlaylistTracks
                                               where x.TrackId == trackid
                                               select x).FirstOrDefault();
                    if (moveTrack == null)
                    {
                        throw new Exception("Playlist track does not exist.");
                    }
                    else
                    {
                        PlaylistTrack otherTrack = null;
                        if (direction.Equals("up"))
                        {
                            if (tracknumber ==1)
                            {
                                throw new Exception("Track is already on the top");
                            }
                            else
                            {
                                //find another track
                                otherTrack = (from x in exists.PlaylistTracks
                                             where x.TrackNumber == moveTrack.TrackNumber-1
                                             select x).FirstOrDefault();
                                if (otherTrack == null)
                                {
                                    throw new Exception("Track is already on the top,refresh the playlist");
                                }
                                else
                                {
                                    moveTrack.TrackNumber -= 1;
                                    otherTrack.TrackNumber += 1;
                                    
                                }
                            }
                        }
                        else
                        {
                            if (tracknumber == exists.PlaylistTracks.Count())
                            {
                                throw new Exception("Track is already on the bottom");
                            }
                            else
                            {
                                //find another track
                                otherTrack = (from x in exists.PlaylistTracks
                                             where x.TrackNumber == moveTrack.TrackNumber + 1
                                             select x).FirstOrDefault();
                                if (otherTrack == null)
                                {
                                    throw new Exception("Track is already on the bottom,refresh the playlist");
                                }
                                else
                                {
                                    moveTrack.TrackNumber += 1;
                                    otherTrack.TrackNumber -= 1;
                                }
                            }
                        }//eoif up down
                        context.Entry(moveTrack).Property(y => y.TrackNumber).IsModified = true;
                        context.Entry(otherTrack).Property(y => y.TrackNumber).IsModified = true;
                        context.SaveChanges();
                    }
                    
                }
               
            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookContext())
            {
                //code to go here
                //play list exists?



                
                
                
                //commit
                //no:message
                var exists = (from x in context.Playlists
                              where x.UserName.Equals(username,StringComparison.OrdinalIgnoreCase)&& x.Name.Equals(playlistname,StringComparison.OrdinalIgnoreCase)
                              select x).FirstOrDefault();
                if (exists == null)
                {
                    throw new Exception("Play list has been removed from the system");
                }
                //yes: create a list of playlistTracks that are to be kept
                else
                {
                    var tracksKept = exists.PlaylistTracks
                        .Where(tr => !trackstodelete.Any(ttd => tr.TrackId == ttd))// !to keep the id that is not match
                        .Select(tr => tr).ToList();
                    PlaylistTrack item = null;
                    // stage the removal of tracks
                    foreach (var dtrackid in trackstodelete)
                    {
                        item = exists.PlaylistTracks.Where(tr => tr.TrackId == dtrackid).FirstOrDefault();
                        if (item != null)
                        {
                            exists.PlaylistTracks.Remove(item);
                        }
                    }

                    int number = 1;
                    // renumbering of kept tracks
                    tracksKept.Sort((x, y) => x.TrackNumber.CompareTo(y.TrackNumber));
                    foreach (var tKept in tracksKept)
                    {
                        tKept.TrackNumber = number;
                        context.Entry(tKept).Property(y => y.TrackNumber).IsModified = true;
                        number++;
                    }
                    context.SaveChanges();
                }

            }
        }//eom
    }
}
