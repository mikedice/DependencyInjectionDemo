using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyTest
{
	public enum SearchResult
	{
		Succeeded,
		BadQuery,
		IDontKnow
	}

	public interface ISearchService
	{
		SearchResult PerformSearch(string query);
	}

	public class SearchService : ISearchService
	{
		public virtual SearchResult PerformSearch(string query)
		{
			Console.WriteLine("Load certificates");
			Console.WriteLine("Make HTTP call to external service using cert authentication");
			Console.WriteLine("External service returns what it returns. Maybe 200, maybe 404, maybe 503, maybe xxxxx. Who knows");
			return SearchResult.Succeeded;
		}
	}

	/*
		We will refine our code to follow container friendly patterns and common convention.
			Move abstractions to an interface

		Program now can take an instance of the abstract interface as a constructor parameter. 
		The Program class does not care how ISearchService is implemented. Tests are free to 
		mock the interface however they see fit.

		Introduce a dependency injection container (Castle.Windsor nuget package Nuget.org)
			Register the things we want the container to create for us
			Call container.Resolve to get the container to find or create instances of our 
				object for us. Along the way the container will create and resolve all necessary
				dependencies so our code does not need to be bothered with this.
	*/

	public class Program
	{
		private readonly ISearchService searchService;

		public Program(ISearchService searchService)
		{
			this.searchService = searchService;
		}

		public int Run(string[] args)
		{
			var result = this.searchService.PerformSearch(args[0]);
			switch (result)
			{
				case SearchResult.Succeeded:
					Console.WriteLine("search succeeded");
					break;
				case SearchResult.BadQuery:
					Console.WriteLine("search bad query");
					break;
				case SearchResult.IDontKnow:
					Console.WriteLine("search don't know");
					break;
			}
			return 0;
		}

		static int Main(string[] args)
		{
			// Create a container and register instances
			var container = new WindsorContainer();
			container.Register(Component.For<ISearchService>().ImplementedBy<SearchService>());
			container.Register(Component.For<Program>().ImplementedBy<Program>());

			// Ask the container for a Program instance and run the program	
			var program = container.Resolve<Program>();
			var result = program.Run(args);
			return result;
		}
	}
}

