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
	