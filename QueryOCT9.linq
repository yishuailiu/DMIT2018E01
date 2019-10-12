<Query Kind="Statements">
  <Connection>
    <ID>75dff7d5-83bc-4894-b819-35a0c7af6396</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//wwww.dotnetlearners.com/linq
//to get both the ablums with tracks and without tracks you can use a .Union()
// in a union you need to ensure cast typing is correct column cast types match identically each query has the same number of columns same order of columns
//create a list of allbums, show the Title, Number of tracks, total cost of tracks, average length(milliseconds) of the tracks

//problem exists for albums without any tracks. Summing and averages need data to work, if an album has no tracks, you work get an abort
//solution, create 2  queries: a)with tracks b) without tracks, then union the results

//sytax (query1).Union(query2).Union(queryn).Orderby(first sort).ThenBy(secondSord)

var unionSample1 = (from x in Albums
					where x.Tracks.Count() > 0
					select new
					{
						title = x.Title,
						trackcount = x.Tracks.Count(),
						priceoftracks = x.Tracks.Sum(y => y.UnitPrice),
						avglengthA = x.Tracks.Average(y => y.Milliseconds)/1000.0,
						avglengthB = x.Tracks.Average(y => y.Milliseconds/1000.0)
					}).Union(				
					from x in Albums
					where x.Tracks.Count() == 0
					select new
					{
						title = x.Title,
						trackcount = 0,
						priceoftracks = 0.00m,
						avglengthA = 0.00,
						avglengthB = 0.00
					});
				//unionSample2.Dump();		

					
		unionSample1.Dump();
	//boolean filter .All() or .Any()
	//, Any() method iterates throught the entire collection to see if any of the items match the specific condition
	//return a true of false
	
	Genres.OrderBy(x => x.Name).Dump();
	
	var genretrack = from x in Genres
						where x.Tracks.Any(tr => tr.PlaylistTracks.Count() ==0)
						orderby x.Name
						select new {
						name = x.Name
						};
		genretrack.Dump();
		//.All() method iterates through the entire collection to see if all of the items match the specified condition
		//return a true of false
		//an instance of the coollection that receives a true is sleected for processing
		
		//list genres that have all their tracks appearing at least onece on playlist
		
		var genretrackall = from x in Genres
						where x.Tracks.All(tr => tr.PlaylistTracks.Count() > 0)
						orderby x.Name
						select new {
							name = x.Name,
							thetracks = (from y in x.Tracks
											where y.PlaylistTracks.Count()>0
											select new {
												song =y.Name,
												count = y.PlaylistTracks.Count()
											})
						};
		genretrackall.Dump();
		
		//sometime you have two lists that need to be compared. 
		//Usually you are looking for items that are the same (in both collections) or you are looking for items that are different
		//in either case: you are comparing one collection to a second collection
		
		//obtain a distince list of all playlist tracks for Roberto Almeida(username AlmeidaR)
		
		var almeida = (from x in PlaylistTracks
						where x.Playlist.UserName.Contains("Almeida")
						orderby x.Track.Name
						select new 
						{
							genre = x.Track.Genre.Name,
							id = x.TrackId,
							song = x.Track.Name
						}).Distinct();
		//almeida.Dump();
		//obtain a distince list of all playlist tracks for Michelle Brooks (username BrookSm)
		var Brooks = (from x in PlaylistTracks
						where x.Playlist.UserName.Contains("Brooks")
						orderby x.Track.Name
						select new 
						{
							genre = x.Track.Genre.Name,
							id = x.TrackId,
							song = x.Track.Name
						}).Distinct();
		//Brooks.Dump();
		//list tracks that both Rebort and Michelle like
		
		var likes = almeida.Where(a => Brooks.Any(b => b.id == a.id)).OrderBy(a => a.genre).Select(a => a);
		//likes.Dump();
		
		//list the rebert's tracks that michelle does not have 
		var almeidadif = almeida.Where(a => !Brooks.Any(b => b.id == a.id))
								.OrderBy(a => a.genre)
								.Select(a => a);
		//almeidadif.Dump();
		
		
		var brooksdif = Brooks.Where(a => !almeida.Any(b => b.id == a.id))
								.OrderBy(a => a.genre)
								.Select(a => a);
								
		//brooksdif.Dump();
		
		//Joins
		//joins can be used where navigational properties DONOT exist
		//joins can be used between associate entities
		//scenario pkey = fkey
		
		//left side of t he join should be the support data
		//right side of the join is the record collection to be processed
		
		
		//list albums showing title, releaseyear, label, artistname and track count
		var results = from xrside in Albums
						join ylside in Artists
						on xrside.ArtistId equals ylside.ArtistId
						select new 
						{
							title= xrside.Title,
							year =xrside.ReleaseYear,
							label =xrside.ReleaseLabel == null? "Unknown" : xrside.ReleaseLabel,
							artist = ylside.Name,
							artistnav =xrside.Artist.Name,
							trackcount =xrside.Tracks.Count()
						};
						results.Dump();