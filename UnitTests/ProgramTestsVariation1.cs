using System;
using DependencyTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	[TestClass]
	public class ProgramTestsVariation1
	{
		[TestMethod]
		public void Program_CanRun()
		{
			Program program = new Program();
			var ret = program.Run(new string[] { "add table" });
			Assert.AreEqual(0, ret);
		}
	}
}
