using System;
using System.Linq;
using System.Collections.Generic;

class Solution
{
	static void Main(string[] args)
	{
		Dictionary<int, Node> treeNode = new Dictionary<int, Node>();
		int n = int.Parse(Console.ReadLine());
		for (int i = 0; i < n; i++)
		{
			string[] inputs = Console.ReadLine().Split(' ');
			int x = int.Parse(inputs[0]);
			int y = int.Parse(inputs[1]);
			AddIfNotExisted(treeNode, x);
			AddIfNotExisted(treeNode, y);
			treeNode[x].AddConnection(treeNode[y]);
			treeNode[y].AddConnection(treeNode[x]);
		}

		Console.WriteLine(CalculateSolution(treeNode));
	}

	public static void AddIfNotExisted(Dictionary<int, Node> treeNode, int id)
	{
		Node node;
		if (!treeNode.TryGetValue(id, out node))
		{
			node = new Node(id);
			treeNode.Add(id, node);
		}
	}

	public static int CalculateSolution(Dictionary<int, Node> treeNode)
	{
		int distance = 0;

		while (treeNode.Count > 1)
		{
			RemoveLeafsAndRoots(treeNode);
			distance++;
		}

		return distance;
	}

	public static void RemoveLeafsAndRoots(Dictionary<int, Node> treeNode)
	{
		var Ileafs = from s in treeNode.Values 
			        where s.IsLeafOrRoot()
		                              select s;
		var leafs = Ileafs.ToList();
		foreach (Node leaf in leafs)
		{
			foreach (Node rem in leaf.connectedNodes)
			{
				rem.RemoveConnection(leaf);
			}
			treeNode.Remove(leaf.ID);
		}
		                      						
	}

}

class Node
{

	#region PROPERTY
	private int id;
	public int ID
	{
		get { return id; }
		set { id = value; }
	}
	#endregion

	public List<Node> connectedNodes = new List<Node>();


	public Node(int id)
	{
		this.ID = id;
	}

	public bool IsLeafOrRoot()
	{
		if (connectedNodes.Count < 2)
			return true;
		else
			return false;
	}

	public void AddConnection(Node adj)
	{
		connectedNodes.Add(adj);
	}

	public void RemoveConnection(Node adj)
	{
		connectedNodes.Remove(adj);
	}
}