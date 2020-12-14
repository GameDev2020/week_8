using System.Collections.Generic;

public class Dijkstra
{

    public static Dictionary<NodeType,NodeType> FindPath<NodeType>(WGraph<NodeType> graph, NodeType startNode)
    {
        Dictionary<NodeType, int> weights = new Dictionary<NodeType, int>(); //node weights
        Dictionary<NodeType, bool> hasVisited = new Dictionary<NodeType, bool>();
        Dictionary<NodeType, NodeType> path = new Dictionary<NodeType, NodeType>();

        Queue<NodeType> q = new Queue<NodeType>();
        q.Enqueue(startNode);
        weights.Add(startNode, 0);
        while (q.Count != 0)
        {
            NodeType n = q.Peek();
            if (!hasVisited.ContainsKey(n))
            {
                hasVisited.Add(n, true);
                q.Dequeue();

                foreach (var neighbor in graph.Neighbors(n))
                {
                    if (weights.ContainsKey(neighbor))
                    {
                        if (weights[neighbor] > weights[n] + graph.getW(neighbor))
                        {
                            weights[neighbor] = weights[n] + graph.getW(neighbor);
                            path[neighbor] = n;

                        }
                    }
                    else
                    {
                        weights[neighbor] = weights[n] + graph.getW(neighbor);
                        path[neighbor] = n;
                    }
                    if (!hasVisited.ContainsKey(neighbor))
                    {
                        q.Enqueue(neighbor);
                    }
                }
            }
            else
            {
                q.Dequeue();
            }
        }
        return path;

    }

    public static List<NodeType> GetPath<NodeType>(WGraph<NodeType> graph, NodeType startNode, NodeType endNode)
    {
        List<NodeType> path = new List<NodeType>();
        Dictionary<NodeType, NodeType> full_path = FindPath<NodeType>(graph, startNode);
        if (full_path.ContainsKey(endNode))
        {
            NodeType td =endNode;
            path.Insert(0, td); //

            while (!path[0].Equals(startNode))
            {
                td = full_path[td];
                path.Insert(0, td);
            }
        }
        return path;
    }


}