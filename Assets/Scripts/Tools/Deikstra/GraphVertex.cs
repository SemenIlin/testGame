using System.Collections.Generic;
/// <summary>
/// Вершина графа
/// </summary>
public class GraphVertex
{
    /// <summary>
    /// Номер вершины
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Список ребер
    /// </summary>
    public List<GraphEdge> Edges { get; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="vertexName">Название вершины</param>

    public bool IsVisited { get; set; }
    
    public GraphVertex(int vertexId)
    {
        Id = vertexId;
        Edges = new List<GraphEdge>();
        IsVisited = false;
    }

    /// <summary>
    /// Добавить ребро
    /// </summary>
    /// <param name="newEdge">Ребро</param>
    public void AddEdge(GraphEdge newEdge)
    {
        Edges.Add(newEdge);
    }

    /// <summary>
    /// Добавить ребро
    /// </summary>
    /// <param name="vertex">Вершина</param>
    /// <param name="edgeWeight">Вес</param>
    public void AddEdge(GraphVertex vertex, float edgeWeight)
    {
        AddEdge(new GraphEdge(vertex, edgeWeight));
    }
}