<Query Kind="Statements">
  <Connection>
    <ID>75dff7d5-83bc-4894-b819-35a0c7af6396</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//using multiple steps to obtain the required data query

//create a list showing whether a particular track length is greater than, less than or average track length
//var averageLength = (from x in Tracks 
//						select x.Milliseconds).Average();
//averageLength.Dump();
//problem, I need the average  track length before testing the individual track length against the avreage
//var resavg = Tracks.Average(x => x.Milliseconds);
//
//						
//var resTrackList = from x in Tracks
//					select new {
//						song = x.Name,
//						length = x.Milliseconds,
//						LongShortAverage = x.Milliseconds > resavg ? "Long" : ( x. Milliseconds== resavg ? "average" : "short") 
//					
//					};
//					
//resTrackList.Dump();

//list all the playlists which have track showing the playlist name,
//number of tracks on the play list, the cost of the playlist, and the total storage size for the playlist in MB

var trackscount = from x in Playlists
					where x.PlaylistTracks.Count() > 0
					select new {
						name = x.Name,
						trackcount = x.PlaylistTracks.Count(),
						cost = x.PlaylistTracks.Sum(z => z.Track.UnitPrice),
						size = x.PlaylistTracks.Sum(z => (z.Track.Bytes/1000000.0))
						//= (from plt in x.PlayListTracks select plt.Track.bytes/1000000.00).Sum()
						};
//trackscount.Dump();

//list all albums with tracks showing the album title, artist name, number of tracks and album cost

var albumtracks = from al in Albums
					where al.Tracks.Count() > 0
					select new 
					{
						name = al.Title,
						Artist = al.Artist.Name,
						trackcount = al.Tracks.Count(),
						cost = al.Tracks.Sum(x => x.UnitPrice)
					};
//what is the maxium album count for all the artists
var artistsalbum = Artists.Max(x => x.Albums.Count());
artistsalbum.Dump();			
			

