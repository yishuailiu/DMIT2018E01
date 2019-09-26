<Query Kind="Statements">
  <Connection>
    <ID>75dff7d5-83bc-4894-b819-35a0c7af6396</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//when using the language 

var results = from x in Albums
				 where x.Artist.Name.Contains("Deep Purple")
				 orderby x.ReleaseYear, x.Title
				 select new
				 {
				 	Id = x.AlbumId ,
					Title = x.Title ,
					ArtistName = x.Artist.Name,
					Year = x.ReleaseYear
				 };
				 
//to display contents of a variable in LinqPad
//using the method .Dump()
//this method is nonly used in Linqpad
				 
				 
				 