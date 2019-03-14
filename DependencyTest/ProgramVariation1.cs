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
			BadQUery,
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
		Program controls the creation of SearchService and uses it in Program.Run
		SearchService has a concrete implementation of PerformSearch
		SearchService.PerformSearch has a lot dependencies:
			* Authentication certificate installed on the test machine
			* Network is up on the test machine
			* Remote 'search service' can be reached from the test machine
			* Remote search service always performs predictably for the test.
		In order to test anything that uses SearchService, all of SearchService's dependencies must be met. This is expensive
		  and heavy handed for a unit test

		Even if a unit test of Program.Run was willing to live with the requirements of SearchService above, the test
		would be required to somehow pass in inputs to Program.Run that would cause SearchService.PerformSearch
		to predictably return all three possible return values (e.g. SearchResult.Succeeded, SearchResult.BadQuery & SearchResult.IDontKnow)

		The result of this dependency chain is a fragile unit test that has a lot of external dependencies. Because of the
		complexity developers might skip unit testing Program.Run all together.
	*/

	public class Program
	{
		private readonly SearchService searchService;

		public Program()
		{
			this.searchService = new SearchService();
		}
		public int Run(string[] args)
		{
			var result = this.searchService.PerformSearch(args[0]);
			switch (result)
			{
				case SearchService.SearchResult.Succeeded:
					Console.WriteLine("search succeeded");
					break;
				case SearchService.SearchResult.BadQUery:
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

