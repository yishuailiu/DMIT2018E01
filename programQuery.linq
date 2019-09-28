<Query Kind="Program">
  <Connection>
    <ID>75dff7d5-83bc-4894-b819-35a0c7af6396</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

public class AlbumsOfArtist {
	public string Title {get;set;}
	public string ArtistName {get;set;}
	public int Year {get;set;}
	public string Rlabel {get;set;}
}
public class CustomerUSYahoo{
	public string Name {get;set;}
	public string City {get;set;}
	public string State {get;set;}
	public string Email {get;set;}
	
}
void Main()
{
	var results = from x in Albums
				 where x.Artist.Name.Contains("Deep Purple")
				 orderby x.ReleaseYear, x.Title
				 select new AlbumsOfArtist
				 {				 	
					Title = x.Title ,
					ArtistName = x.Artist.Name,
					Year = x.ReleaseYear,
					Rlabel = x.ReleaseLabel
				 };
	var resultsUS = from x in Customers
					where x.Country.Equals("USA") &&  x.Email.Contains("@yahoo") 
					orderby x.LastName, x.FirstName
					select new CustomerUSYahoo 
					{
						Name = x.LastName + " " + x.FirstName,
						City = x.City,
						State = x.State,
						Email = x.Email
					};
//	var artistSingRagDoll = from x in Artists
//							join y in Albums on x.ArtistId equals y.ArtistId
//							join z in Tracks on y.AlbumId equals z.AlbumId
//							where z.Name.Contains("Rag Doll")
//							select new 
//							{
//								Artist = x.Name,
//								Album = y.Title,
//								Year = y.ReleaseYear,
//								Label = y.ReleaseLabel,
//								Composer = z.Composer								
//							};
	var sing = from x in Tracks
				where x.Name.Equals("Rag Doll")				 
				select new {
				 	 ArtistName = x.Album.Artist.Name,
					 Album = x.Album.Title,
					 Year = x.Album.ReleaseYear,
					 Label = x.Album.ReleaseLabel,
					 Composer = x.Composer
				 };
				 
	//Create a list of album released in 2001.
	//list the album title, artist name , label,

	var albums2001 = from x in Albums
			where x.ReleaseYear.Equals(2001)
			select new {
				Title = x.Title,
				Artist = x.Artist.Name,
				Label = x.ReleaseLabel == null? "unknown" : x.ReleaseLabel
			};
			
	
	//list of all albums specifing if they were released in the 70's 80's 90's or moden music(2000 and later)
	//list the title and decades
	
	var newMusic = from x in Albums
					where x.ReleaseYear >= 1970
					select new {
						Title = x.Title,
						Decades = x.ReleaseYear >= 2000 ? "Moden" 
						: (x.ReleaseYear >= 1990? "90s"
							:(x.ReleaseYear >=1980 ? "80s"
								: "70s" ))
					};

			
	//create a list of all albums containing the album title and artist along with all the tracks of that album 
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
					 
}




//define other methods and classes