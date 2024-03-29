﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.POCOs;
using DMIT2018Common.UserControls;
using WebApp.Security;
#endregion

namespace WebApp.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole("Customers")||User.IsInRole("Customer Service"))
                {
                    var username = User.Identity.Name;
                    SecurityController securitymgr = new SecurityController();
                    int? customerid = securitymgr.GetCurrentUserCustomerId(username);
                    if (customerid.HasValue)
                    {
                        MessageUserControl.TryRun(() =>
                        {
                            CustomerController sysmgr = new CustomerController();
                            Customer info = sysmgr.Customer_Get(customerid.Value);
                            CustomerName.Text = info.FullName;
                        });
                    }
                    else
                    {
                        MessageUserControl.ShowInfo("Unregistered User", "This user is not a registered customer");
                        CustomerName.Text = "Unregistered User";
                    }
                }
                else
                {
                    //redirect to a page that states no authorization fot the request action
                    Response.Redirect("~/Security/AccessDenied.aspx");
                }
            }
            else
            {
                //redirect to login page
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void ArtistFetch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ArtistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter a partial artist name.");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    SearchArg.Text = ArtistName.Text;
                    TracksBy.Text = "Artist";
                    TracksSelectionList.DataBind();//cause the ODS to execute
                },"TrackSearch","Select from the following list to add to your playlist");
            }
               

          }

        protected void MediaTypeFetch_Click(object sender, EventArgs e)
        {

            MessageUserControl.TryRun(() =>
            {
                SearchArg.Text = MediaTypeDDL.SelectedValue;
                TracksBy.Text = "MediaType";
                TracksSelectionList.DataBind();//cause the ODS to execute
            }, "TrackSearch", "Select from the following list to add to your playlist");

        }

        protected void GenreFetch_Click(object sender, EventArgs e)
        {

            MessageUserControl.TryRun(() =>
            {
                SearchArg.Text = GenreDDL.SelectedValue;
                TracksBy.Text = "Genre";
                TracksSelectionList.DataBind();//cause the ODS to execute
            }, "TrackSearch", "Select from the following list to add to your playlist");

        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(AlbumTitle.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter a partial album name.");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    SearchArg.Text = AlbumTitle.Text;
                    TracksBy.Text = "Album";
                    TracksSelectionList.DataBind();//cause the ODS to execute
                }, "TrackSearch", "Select from the following list to add to your playlist");
            }

        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Required Data",
                    "Play list name is required to fetch a play list");
            }
            else
            {
                string playlistname = PlaylistName.Text;
                //until we do security, we will use a hard coded username
                string username = User.Identity.Name;

                //do a standard query lookup to your control
                //use MessageUserControl for error handling
                MessageUserControl.TryRun(() =>
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(
                        playlistname, username);
                    PlayList.DataSource = datainfo;
                    PlayList.DataBind();
                }, "Playlist Tracks", "See current tracks on playlist below");
            }

        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            List<string> reasons = new List<string>();
            //is there a play list?
            if (PlayList.Rows.Count == 0)
            {
                reasons.Add("There is no playlist present.");
            }
            //no msg

            //is there a playlist name?
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                reasons.Add("You must have a playlist name");
            }
            //no msg

            //traverse playlist to collect selected row(s)
            //> 1 row selected
            //bad message
            int trackid = 0;
            int tracknumber = 0;
            int rowsSelected = 0;
            CheckBox playlistSelection = null;
            for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
            {
                //access the control on the indexed GridViewRow
                //set the CheckBox pointer to this checkbox control
                playlistSelection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                if (playlistSelection.Checked)
                {
                    rowsSelected++;

                    //gather the data needed for the BLL call
                    trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                    tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);
                }
                
                
            }
            if (rowsSelected !=1)
            {
                
                    reasons.Add("Select only one track to move");
                
            }
            //check if last track bad msg
            if (tracknumber == PlayList.Rows.Count)
            {
                reasons.Add("The selected track is already at the bottom");
            }
            //valiation good move track
            // yes: move the track
            if (reasons.Count==0)
            {
                MoveTrack(trackid, tracknumber, "down");
            }
            //no:display message
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    throw new BusinessRuleException("Track Move Errors",reasons);
                });
            }

 
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            List<string> reasons = new List<string>();
            //is there a play list?
            if (PlayList.Rows.Count == 0)
            {
                reasons.Add("There is no playlist present.");
            }
            //no msg

            //is there a playlist name?
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                reasons.Add("You must have a playlist name");
            }
            //no msg

            //traverse playlist to collect selected row(s)
            //> 1 row selected
            //bad message
            int trackid = 0;
            int tracknumber = 0;
            int rowsSelected = 0;
            CheckBox playlistSelection = null;
            for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
            {
                //access the control on the indexed GridViewRow
                //set the CheckBox pointer to this checkbox control
                playlistSelection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                if (playlistSelection.Checked)
                {
                    rowsSelected++;

                    //gather the data needed for the BLL call
                    trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                    tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);
                }


            }
            if (rowsSelected != 1)
            {

                reasons.Add("Select only one track to move");

            }
            //check if last track bad msg
            if (tracknumber == 1)
            {
                reasons.Add("The selected track is already at the top");
            }
            //valiation good move track
            // yes: move the track
            if (reasons.Count == 0)
            {
                MoveTrack(trackid, tracknumber, "up");
            }
            //no:display message
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    throw new BusinessRuleException("Track Move Errors", reasons);
                });
            }

        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move the track
            MessageUserControl.TryRun(() => {
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                sysmgr.MoveTrack(User.Identity.Name, PlaylistName.Text, trackid, tracknumber, direction);
                List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(PlaylistName.Text, User.Identity.Name);
                PlayList.DataSource = datainfo;
                PlayList.DataBind();
            }, "Success", "Track has been moved");
 
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Required data", "Play list name is required to add a track");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Required Data", "No playlist is avaiblis.");
                }
                else
                {
                    //traverse the gridview and collect the list of tracks to revmoce
                    List<int> trackstodelete = new List<int>();
                    int rowselected = 0;
                    CheckBox playlistselection = null;
                    for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
                    {
                        playlistselection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                        if (playlistselection.Checked)
                        {
                            rowselected++;
                            trackstodelete.Add(int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text));
                        }
                    }
                    if (rowselected == 0)
                    {
                        MessageUserControl.ShowInfo("Required data", "Selected at least one track to remove");
                    }
                    else
                    {
                        //send list of tracks to be removed by BLL
                        MessageUserControl.TryRun(() =>
                        {
                            PlaylistTracksController sysmgr = new PlaylistTracksController();
                            sysmgr.DeleteTracks(User.Identity.Name,PlaylistName.Text,trackstodelete);
                            List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(PlaylistName.Text, User.Identity.Name);
                            PlayList.DataSource = datainfo;
                            PlayList.DataBind();
                        }, "Removing a Track", "Track has been removed from playlist");
                    }

                }
            }

        }

        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Required data", "Play list name is required to add a track");
            }
            else
            {
                //collect the required data for the event
                string playlistname = PlaylistName.Text;
                //the username will come from the security
                //so untill security is added, we will use HansenB
                string username = User.Identity.Name;
                //obtain the track id from the ListView
                //the track id will be in the commandArg property of the ListViewCommandEventArgs e instance
                //the commandarg in e is return as an object
                int trackid = int.Parse(e.CommandArgument.ToString());

                MessageUserControl.TryRun(() =>
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    sysmgr.Add_TrackToPLaylist(playlistname, username, trackid);
                    List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(playlistname, username);
                    PlayList.DataSource = datainfo;
                    PlayList.DataBind();
                },"Adding a Track","Track has been added to the playlist");
            }
            
        }

    }
}