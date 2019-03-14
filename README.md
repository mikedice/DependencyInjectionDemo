# DependencyInjectionDemo
Demo of dependency injection using containers in C#

I'm creating this demo to use as part of a presentation at work. I thought I would put it here on the GitHub in case other people wish to take a look

The Demo is in four parts. Each part has two C# files. The files are named ProgramVariation{1-4}.cs and ProgramTestsVariation{1-4}.cs. You can enable the variations one at a time by clicking on the file you want to enable in Solution Explorer and using the Properties window, change it's Build Action property from None to Compile. You can disable the variation by changing the Build Action back to None from Compile.

The simple theme of the demo is that a user is writing a console based program, implemented in the class Program. The Program class uses an instance of the SearchService class to perform queries. Program passes SearchService a query string and as a result SearchService sends the query to a remote endpoint over HTTP to fetch some search results for the query. The actual fetching of results is pretend in this example for demonstration purposes.

The four variations step through the problems encountered unit testing such a program and how those problems can be solved. The description of each variation is written in comments inside the Programvariation{1-4}.cs C# files. Apologies for the weak documenation but developers should be used to reading documentation in code :). I'll clean it up as time permits

