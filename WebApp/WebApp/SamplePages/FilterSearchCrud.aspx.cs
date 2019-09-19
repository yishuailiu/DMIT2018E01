using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ChinookSystem.BLL;
using ChinookSystem.Data.Entities;

namespace WebApp.SamplePages
{
    public partial class FilterSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindArtistList();
                //set the maxvalue for the validation control

                RangeValidatorEditReleaseYear.MaximumValue = DateTime.Now.Year.ToString();

            }
        }

        protected void BindArtistList()
        {
            ArtistController sysmgr = new ArtistController();

            List<Artist> info = sysmgr.Artist_List();
            info.Sort((x, y) => x.Name.CompareTo(y.Name));
            ArtistList.DataSource = info;
            ArtistList.DataTextField = nameof(Artist.Name);
            ArtistList.DataValueField = nameof(Artist.ArtistId);

            ArtistList.DataBind();
            //ArtistList.Items.Insert(0, "select ...");
        }

        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void AlbumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //standard lookup
            GridViewRow agvrow = AlbumList.Rows[AlbumList.SelectedIndex];
            //retreive the value from a web control located within the GridView cell

            string albumid = (agvrow.FindControl("AlbumId") as Label).Text;

            //error handling will need to be added
            MessageUserControl.TryRun(() =>
           {
               AlbumController sysmgr = new AlbumController();
               Album datainfo = sysmgr.Album_Get(int.Parse(albumid));
               if (datainfo == null)
               {
                   //clear the controls
                   ClearControls();
                   //throw an exception
                   throw new Exception("Record no longer exists on file. ");
               }
               else
               {
                   EditAlbumID.Text = datainfo.AlbumId.ToString();
                   EditTitle.Text = datainfo.Title;
                   EditAlbumArtistList.SelectedValue = datainfo.ArtistId.ToString();
                   EditReleaseYear.Text = datainfo.ReleaseYear.ToString();
                   EditReleaseLabel.Text = datainfo.ReleaseLabel == null ? "" : datainfo.ReleaseLabel;
               }
           }, "Find Album", "AlbumFound");//strings on this line are success message
                                          //standard lookup

        }

        protected void ClearControls()
        {
            EditAlbumID.Text = "";
            EditTitle.Text = "";
            EditReleaseYear.Text = "";
            EditReleaseLabel.Text = "";
            EditAlbumArtistList.SelectedIndex = 0;
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string albumtitle = EditTitle.Text;
                int albumyear = int.Parse(EditReleaseYear.Text);
                string albumlabel = EditReleaseLabel.Text == "" ? null : EditReleaseLabel.Text;
                int albumartist = int.Parse(EditAlbumArtistList.SelectedValue);

                Album theAlbum = new Album();
                theAlbum.Title = albumtitle;
                theAlbum.ArtistId = albumartist;
                theAlbum.ReleaseYear = albumyear;
                theAlbum.ReleaseLabel = albumlabel;

                MessageUserControl.TryRun(() =>
                {
                    AlbumController sysmgr = new AlbumController();
                    int albumid = sysmgr.Album_Add(theAlbum);
                    EditAlbumID.Text = albumid.ToString();
                    if (AlbumList.Rows.Count > 0)
                    {
                        AlbumList.DataBind();
                    }

                }, "Successful", "Album added");
            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int editablumid = 0;
                string albumid = EditAlbumID.Text;
                if (string.IsNullOrEmpty(albumid))
                {
                    MessageUserControl.ShowInfo("Select the album before editing");
                }
                else if (!int.TryParse(albumid, out editablumid))
                {
                    MessageUserControl.ShowInfo("failed", "invalid album id");
                }
                else
                {



                    Album theAlbum = new Album();
                    theAlbum.AlbumId = editablumid;
                    theAlbum.Title = EditTitle.Text;
                    theAlbum.ArtistId = int.Parse(EditAlbumArtistList.SelectedValue);
                    theAlbum.ReleaseYear = int.Parse(EditReleaseYear.Text);
                    theAlbum.ReleaseLabel = EditReleaseLabel.Text == "" ? null : EditReleaseLabel.Text;

                    MessageUserControl.TryRun(() =>
                    {
                        AlbumController sysmgr = new AlbumController();
                        int rowsAffected = sysmgr.Album_Update(theAlbum);
                        EditAlbumID.Text = rowsAffected.ToString();
                        if (AlbumList.Rows.Count > 0)
                        {
                            AlbumList.DataBind();
                        }
                        else
                        {
                            throw new Exception("Album was not found. Repeat lookup and update again.");
                        }




                    }, "Successful", "Album Updated");
                }
            }
        }

        protected void Remove_Click(object sender, EventArgs e)
        {
            int editablumid = 0;
            string albumid = EditAlbumID.Text;
            if (string.IsNullOrEmpty(albumid))
            {
                MessageUserControl.ShowInfo("Select the album before deleting");
            }
            else if (!int.TryParse(albumid, out editablumid))
            {
                MessageUserControl.ShowInfo("failed", "invalid album id");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    AlbumController sysmgr = new AlbumController();
                    int rowsAffected = sysmgr.Album_Delete(editablumid);
                    EditAlbumID.Text = rowsAffected.ToString();
                    if (AlbumList.Rows.Count > 0)
                    {
                        AlbumList.DataBind();
                        ClearControls();
                    }
                    else
                    {
                        throw new Exception("Album was not found. Repeat lookup and update again.");
                    }




                }, "Successful", "Album Removed");
            }
        }
    }
}
