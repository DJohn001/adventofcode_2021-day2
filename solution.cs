using System;
using System.Collections.Generic;
using System.Linq;
					
public class Program
{
	public static void Main()
	{
		var content = @"
Forward 5
Down 5
Forward 8
Up 3
Down 8
Forward 2
		";
		
		Console.WriteLine($"Solve a: {SolveA(content)}");
		Console.WriteLine($"Solve b: {SolveB(content)}");
	}
	
	public static int SolveA(string content){
		if(string.IsNullOrWhiteSpace(content))
			return -1;
		var lineSplittedContent = SplitContent(content);
		
		var agg = lineSplittedContent.Aggregate((horizontal: 0, dept: 0), (current, next) => next switch
									  {
										  KeyValuePair<string, int> item when item.Key == "Forward" => (item.Value + current.horizontal		, current.dept),
										  KeyValuePair<string, int> item when item.Key == "Up" 		=> (current.horizontal					, current.dept - item.Value),
										  KeyValuePair<string, int> item when item.Key == "Down" 	=> (current.horizontal					, current.dept + item.Value),
										  _ => (0,0)
									  });
		
		return agg.horizontal * agg.dept;
	}
	
	public static int SolveB(string content){
		if(string.IsNullOrWhiteSpace(content))
			return -1;
		var lineSplittedContent = SplitContent(content);
		
		var agg = lineSplittedContent.Aggregate((horizontal: 0, dept: 0, aim:0), (current, next) => next switch
									  {
										  KeyValuePair<string, int> item when item.Key == "Forward" => (item.Value + current.horizontal		, current.dept + (item.Value * current.aim)	, current.aim ),
										  KeyValuePair<string, int> item when item.Key == "Up" 		=> (current.horizontal					, current.dept								, current.aim - item.Value ),	
										  KeyValuePair<string, int> item when item.Key == "Down" 	=> (current.horizontal					, current.dept								, current.aim + item.Value ),
										  _ => (0, 0, 0)
									  });
		
		return agg.horizontal * agg.dept;
	}
	
	public static IEnumerable<KeyValuePair<string, int>> SplitContent(string content)
	{	
		return content
			.Split(System.Environment.NewLine)
			?.Where(line => !string.IsNullOrWhiteSpace(line))
			?.Select(line => StringLineSplit(line, ' '))
			?.Select(lineCollection => mapLineCollectionToPair(lineCollection)) ?? Enumerable.Empty<KeyValuePair<string, int>>();
	}
	
	public static KeyValuePair<string, int> mapLineCollectionToPair(string[] lineCollection, int maxComponents = 2)
	{
		if( lineCollection.Length != maxComponents)
			throw new InvalidOperationException("We got more than the expected components");
		
		return new KeyValuePair<string, int>( lineCollection.First(), Int32.Parse(lineCollection.Last()));	
	}
	
	public static string[] StringLineSplit(String content, char sepparator, int maxComponents = 2)
	{
		string[] lineCollection = content.Split(sepparator);
		if( lineCollection.Length != maxComponents)
			throw new InvalidOperationException("We got more than the expected components");
		return lineCollection;
	}
}
