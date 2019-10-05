<Query Kind="Statements">
  <Connection>
    <ID>75dff7d5-83bc-4894-b819-35a0c7af6396</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

from x in Tracks
group x by x.GenreId

//group recofd collection using multiple fields on the record
//the ultiple fiedlds become a group key instance
//referring to the property in the gourp key instance is by key.property
 
from x in Tracks
group x by new { x.GenreId,x.MediaTypeId}

//place the grouping of the large data collection into a temporary data collection
//ANY further reporting on the groups within the temporary data collection
//	will use the temporary data collection name  as its data source

from x in Tracks
group x by x.GenreId into gGenre
select (gGenre)

//details on each group
from x in Tracks
group x by x.GenreId into gGenre
select new 
{
	gruopid= gGenre.Key,
	tracks = gGenre.ToList()
}

//select fields from each group
from x in Tracks
group x by x.GenreId into gGenre
select new 
{
	groupid = gGenre.Key,
	tracks = from x in gGenre
	select new 
	{
		trackid = x.TrackId,
		song =x.Name,
		songlength = x.Milliseconds/1000000
	}
	
}

//refer to a specific key property
from x in Tracks
group x by new {x.GenreId, x.MediaTypeId} into gTracks
select new 
{	
	genre = gTracks.Key.GenreId,
	media = gTracks.Key.MediaTypeId,
	trackcount =gTracks.Count()
}

//you can also group by class
from x in Tracks
group x by x.Genre into gTracks
select (gTracks)


from x in Tracks
group x by x.Album into gTracks
select new 
{
	name = gTracks.Key.Title,
	artist = gTracks.Key.Artist.Name,
	trackcount = gTracks.Count()
}

//create a list of albums group by ReleaseYear
//showing the year, album title, count of tracks for each album,count of tracks
//list of albums showing album title, count of tracks for each album


from x in Albums
group x by x.ReleaseYear into gRYear
select new 
{
	year = gRYear.Key,
	albumcount = gRYear.Count(),
	AnAlbum = from y in gRYear 
	select new 
	{
		Title = y.Title,
		countTracks = (from t in y.Tracks
						select t).Count()
	}
}

//order the preview report by number of albums per year descending


from x in Albums
where x.ReleaseYear > 1990
group x by x.ReleaseYear into gRYear
orderby gRYear.Count() descending , gRYear.Key
select new 
{
	year = gRYear.Key,
	albumcount = gRYear.Count(),
	AnAlbum = from y in gRYear 
	select new 
	{
		Title = y.Title,
		countTracks = (from t in y.Tracks
						select t).Count()
	}
}
