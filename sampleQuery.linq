<Query Kind="Expression">
  <Connection>
    <ID>75dff7d5-83bc-4894-b819-35a0c7af6396</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//sample of query syntax to dump the artist data 
/*from x in Artists 
where x.ArtistId==1
select x*/

//sample of method sytax to dump the Artist data
Artists
	.Select(x => x)
//sort datainfo.Sort((x,y) => x.AttributeName.CompareTo(y.AttributeName))

//find any artist whos name contains the string 'son'
from x in Artists
where x.Name.Contains("son")
select x

Artists
	.Where(x => x.Name.Contains("son")) 
	
//create a list of albums released in 1970
//order by title
from x in Albums
where x.ReleaseYear==1970
orderby x.Title
select x

Albums
	.Where(x => x.ReleaseYear == 1970)
	.OrderBy(x => x.Title)
	.Select(x => x)
	
//create a list of albums release between 2007 and 2018
//order by releaseyear then by title

from x in Albums
where x.ReleaseYear <= 2018 && x.ReleaseYear >=2007
orderby x.ReleaseYear descending, x.Title
select x

//note the difference in method names using method syntax
//a descending orderby is .OrderByDescending
//secondery and beyong  ordering is .Thenby
Albums
   .Where (x => ((x.ReleaseYear <= 2018) && (x.ReleaseYear >= 2007)))
   .OrderByDescending (x => x.ReleaseYear)
   .ThenBy (x => x.Title)
   
 //Can navigational properties by used in queries
 //create a list of albums by Deep Purple
 //order by release year and title
 from x in Albums
 where x.Artist.Name.Contains("Deep Purple")
 orderby x.ReleaseYear, x.Title
 select new
 {
 	Id = x.AlbumId ,
	Title = x.Title ,
	ArtistName = x.Artist.Name,
	Year = x.ReleaseYear
 }