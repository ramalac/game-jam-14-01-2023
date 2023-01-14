using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Node
{
    public int x;
    public int y;
    public int gScore;
    public int fScore;
    public Node parent;

    public Node(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}


public class AStar : MonoBehaviour
{
    // The grid of nodes to be searched
    private Node[,] grid;

    // The start and goal nodes
    private Node startNode;
    private Node goalNode;

    // The open and closed lists
    private List<Node> openList = new List<Node>();
    private List<Node> closedList = new List<Node>();

    // The heuristic function to be used
    private Func<Node, Node, int> heuristic;

    public AStar(Node[,] grid, Node start, Node goal, Func<Node, Node, int> heuristic)
    {
        this.grid = grid;
        this.startNode = start;
        this.goalNode = goal;
        this.heuristic = heuristic;
    }

    // Find the path from the start to the goal using A*
    public List<Node> FindPath()
    {
        // Add the start node to the open list
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            // Find the node with the lowest F score
            Node currentNode = FindLowestFScoreNode(openList);

            // If the current node is the goal, return the path
            if (currentNode == goalNode)
                return ReconstructPath(currentNode);

            // Remove the current node from the open list and add it to the closed list
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            // Get all the current node's neighbors
            List<Node> neighbors = GetNeighbors(currentNode);

            // Iterate through the neighbors
            for (int i = 0; i < neighbors.Count; i++)
            {
                Node neighbor = neighbors[i];

                // If the neighbor is in the closed list, skip it
                if (closedList.Contains(neighbor))
                    continue;

                // Calculate the G score for the neighbor (the cost to reach the neighbor)
                int gScore = currentNode.gScore + GetCost(currentNode, neighbor);

                // If the neighbor is not in the open list or if the new G score is better
                if (!openList.Contains(neighbor) || gScore < neighbor.gScore)
                {
                    // Update the neighbor's G score
                    neighbor.gScore = gScore;

                    // Update the neighbor's F score
                    neighbor.fScore = neighbor.gScore + heuristic(neighbor, goalNode);

                    // Set the neighbor's parent to the current node
                    neighbor.parent = currentNode;

                    // If the neighbor is not in the open list, add it
                    if (!openList.Contains(neighbor))
                        openList.Add(neighbor);
                }
            }
        }

        // If the open list is empty, no path was found
        return new List<Node>();
    }

    // Find the node with the lowest F score
    private Node FindLowestFScoreNode(List<Node> list)
    {
        Node lowest = list[0];
        for (int i = 1; i < list.Count; i++)
        {
            if (list[i].fScore < lowest.fScore)
                lowest = list[i];
        }
        return lowest;
    }

    private List<Node> ReconstructPath(Node goalNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = goalNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        // Check the top neighbor
        if (node.x > 0)
            neighbors.Add(grid[node.x - 1, node.y]);

        // Check the bottom neighbor
        if (node.x < grid.GetLength(0) - 1)
            neighbors.Add(grid[node.x + 1, node.y]);

        // Check the left neighbor
        if (node.y > 0)
            neighbors.Add(grid[node.x, node.y - 1]);

        // Check the right neighbor
        if (node.y < grid.GetLength(1) - 1)
            neighbors.Add(grid[node.x, node.y + 1]);

        return neighbors;
    }

    private int GetCost(Node current, Node neighbor)
    {
        // The cost is the distance between the current node and the neighbor
        int cost = (int)Vector2.Distance(new Vector2(current.x, current.y), new Vector2(neighbor.x, neighbor.y));
        return cost;
    }

}
