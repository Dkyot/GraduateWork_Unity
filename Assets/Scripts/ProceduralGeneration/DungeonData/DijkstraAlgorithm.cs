using System;
using UnityEngine;

public class DijkstraAlgorithm 
{
    private int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount) {
        int min = int.MaxValue;
        int minIndex = 0;
        for (int v = 0; v < verticesCount; ++v) {
            if (shortestPathTreeSet[v] == false && distance[v] <= min) {
                min = distance[v];
                minIndex = v;
            }
        }
        return minIndex;
    }
 
    private void Print(int[] distance, int verticesCount) {
        Debug.Log("Вершина    Расстояние от источника");
        for (int i = 0; i < verticesCount; ++i)
            Debug.Log(i+"--"+distance[i]);
    }

    public void RunAlgorithm(int[,] graph, int source, int verticesCount) {
        int[] distance = new int[verticesCount];
        bool[] shortestPathTreeSet = new bool[verticesCount];
        for (int i = 0; i < verticesCount; ++i) {
            distance[i] = int.MaxValue;
            shortestPathTreeSet[i] = false;
        }
        distance[source] = 0;
        for (int count = 0; count < verticesCount - 1; ++count) {
            int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);
            shortestPathTreeSet[u] = true;
            for (int v = 0; v < verticesCount; ++v)
                if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v]) && distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
                    distance[v] = distance[u] + graph[u, v];
        }
        Print(distance, verticesCount);
    }
}
