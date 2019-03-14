using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyTest
{
	public class SearchService
	{
		public enum SearchResult
		{
			Succeeded,
			BadQuery,
			IDontKnow
		}

		public SearchResult PerformSearch(string query)
		{
			Console.WriteLine("Load certificates");
			Console.WriteLine("Make HTTP call to external service using cert authentication");
			Console.WriteLine("External service returns what it returns. Maybe 200, maybe 404, maybe 503, maybe xxxxx. Who knows");
			return SearchResult.Succeeded;
		}
	}

	/*
		What if Program didn't control the creation of its dependency, SearchService?

		Now the test itself could create a SearchService class instance. However, the current
		SearchService class still has all the same dependency problems and we will still have 
		all the same fragility issues as we did in variation one.

		There is a simple solution to this though, we can use object oriented design to solve the problem! See variation 3
	*/

	public class Program
	{
		private readonly SearchService searchService;

		public Program(SearchService searchService)
		{
			this.searchService = searchService;
		}

		public int Run(string[] args)
		{
			var result = this.searchService.PerformSearch(args[0]);
			switch (result)
			{
				case SearchService.SearchResult.Succeeded:
					Console.WriteLine("search succeeded");
					break;
				case SearchService.SearchResult.BadQuery:
					Console.WriteLine("search bad query");
					break;
				case SearchService.SearchResult.IDontKnow:
					Console.WriteLine("search don't know");
					break;
			}
			return 0;
		}

		static int Main(string[] args)
		{
			var program = new Program();
			return program.Run(args);
		}
	}
}

