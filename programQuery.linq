<Query Kind="Program">
  <Connection>
    <ID>75dff7d5-83bc-4894-b819-35a0c7af6396</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
	var results = from x in Albums
				 where x.Artist.Name.Contains("Deep Purple")
				 orderby x.ReleaseYear, x.Title
				 select new AlbumsOfArtist
				 {				 	
					Title = x.Title ,
					ArtistName = x.Artist.Name,
					Year = x.ReleaseYear
					Rlabel = x.ReleaseLabel
				 };
	
	
}
//define other methods and classes
public class AlbumsOfArtist {
	public string Title {get;set;}
	public string ArtistName {get;set;}
	public int Year {get;set;}
	public string Rlabel {get;set;}
}
