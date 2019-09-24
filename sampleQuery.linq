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
from x in Artists 
where x.ArtistId==1
select x

//sample of method sytax to dump the Artist data
Artists
	.Select(x => x)
//sort datainfo.Sort((x,y) => x.AttributeName.CompareTo(y.AttributeName))