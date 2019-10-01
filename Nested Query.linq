<Query Kind="Statements">
  <Connection>
    <ID>75dff7d5-83bc-4894-b819-35a0c7af6396</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var tracksnew = from x in Albums
						
						select new 
						{
							Title = x.Title,
							Artist = x.Artist.Name,
							tracks =  from y in x.Tracks										
										select new {
										TrackName = y.Name,
										Genre = y.Genre.Name,
										Length = y.Milliseconds}
							
						};
			tracksnew.Dump();