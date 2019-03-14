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

		public virtual SearchResult PerformSearch(string query)
		{
			Console.WriteLine("Pretend this service calls a remote endpoint over HTTP to fetch some search results for the passed in query");
			Console.WriteLine("Load certificates");
			Console.WriteLine("Make HTTP call to external service using cert authentication");
			Console.WriteLine("External service returns what it returns. Maybe 200, maybe 404, maybe 503, maybe xxxxx. Who knows");
			return SearchResult.Succeeded;
		}
	}

	/*
		In this variation Program still doesn't control the creation of its dependencies. However, now
		SearchService.PerformSearch is a virtual method and the virtual method is called by Program.Run

		Now the test can create an instance of a SearchService class that implements the virtual PerformSearch
		method to behave in a way that is 100% predicatble and doesn't have any dependencies. The unit test
		can create Progam instances three times, one each with a SearchServiceOK, SearchServiceBad & SearchServiceIdk
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

			var program = new Program(new SearchService());
			return program.Run(args);
		}
	}
}

