using System;
using DependencyTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
	/*
		Lets clean up the code and make this look like professionals are doing the coding
		Using a mocking test framework the test can configure mocks for all needed variations
		of SearchService so we don't have to write all the class overloads.
	*/

	[TestClass]
	public class ProgramTests
	{
		private Mock<ISearchService> searchServiceMock;

		[TestInitialize]
		public void TestInitialize()
		{
			this.searchServiceMock = new Mock<ISearchService>();
		}

		// Use Mock to return all possible results from SearchService.PerformSearch to test Program.Run.
		// The function under test, Program.Run, will have 100% code coverage.
		[TestMethod]
		public void Program_CanRun()
		{
			// Configure mock
			this.searchServiceMock.Setup(m => m.PerformSearch(It.IsAny<string>())).Returns(SearchResult.Succeeded);

			// Run Test
			Program program = new Program(this.searchServiceMock.Object);
			var ret = program.Run(new string[] { "add table" });
			Assert.AreEqual(0, ret);

			// Configure mock
			this.searchServiceMock.Setup(m => m.PerformSearch(It.IsAny<string>())).Returns(SearchResult.BadQuery);

			// Run Test
			program = new Program(this.searchServiceMock.Object);
			ret = program.Run(new string[] { "add table" });
			Assert.AreEqual(0, ret);

			// Configure mock
			this.searchServiceMock.Setup(m => m.PerformSearch(It.IsAny<string>())).Returns(SearchResult.IDontKnow);

			// run test
			program = new Program(this.searchServiceMock.Object);
			ret = program.Run(new string[] { "add table" });
			Assert.AreEqual(0, ret);
		}
	}
}
