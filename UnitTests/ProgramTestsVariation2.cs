using System;
using DependencyTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	/*
		Its messy and a lot of typing to have to have three instances of the SearchService
		class so we can have three variations of SearchService.PerformSearch. However, all 
		of the dependencies and external variables have been removed from SearchService.PerformSearch.
		For the purpose of our test, we can assume that now, SearchService.PerformSearch will
		perform 100% reliably and predictably for our test so we can test all the variations
		we need to test.
	*/
	public class SearchServiceOK : SearchService
	{
		public override SearchResult PerformSearch(string query)
		{
			return SearchResult.Succeeded;
		}
	}

	public class SearchServiceBad : SearchService
	{
		public override SearchResult PerformSearch(string query)
		{
			return SearchResult.BadQuery;
		}
	}

	public class SearchServiceIdk : SearchService
	{
		public override SearchResult PerformSearch(string query)
		{
			return SearchResult.IDontKnow;
		}
	}

	[TestClass]
	public class ProgramTests
	{
		[TestMethod]
		public void Program_CanRun()
		{
			Program program = new Program(new SearchServiceOK());
			var ret = program.Run(new string[] { "add table" });
			Assert.AreEqual(0, ret);


			program = new Program(new SearchServiceBad());
			ret = program.Run(new string[] { "add table" });
			Assert.AreEqual(0, ret);

			program = new Program(new SearchServiceIdk());
			ret = program.Run(new string[] { "add table" });
			Assert.AreEqual(0, ret);
		}
	}
}
