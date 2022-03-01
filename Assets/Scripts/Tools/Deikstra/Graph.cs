using System.Collections.Generic;
/// <summary>
/// Граф
/// </summary>
public class Graph
{
    /// <summary>
    /// Список вершин графа
    /// </summary>
    public List<GraphVertex> Vertices { get; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public Graph()
    {
        Vertices = new List<GraphVertex>();
    }

    /// <summary>
    /// Добавление вершины
    /// </summary>
    /// <param name="vertexId">Id вершины</param>
    public void AddVertex(int vertexId)
    {
        Vertices.Add(new GraphVertex(vertexId));
    }

    /// <summary>
    /// Поиск вершины
    /// </summary>
    /// <param name="vertexName">Название вершины</param>
    /// <returns>Найденная вершина</returns>
    public GraphVertex FindVertex(int vertexId)
    {
        foreach (var v in Vertices)
        {
            if (v.Id == vertexId)
            {
                return v;
            }
        }

        return null;
    }

    /// <summary>
    /// Добавление ребра
    /// </summary>
    /// <param name="firstId">Id первой вершины</param>
    /// <param name="secondId">Id второй вершины</param>
    /// <param name="weight">Вес ребра соединяющего вершины</param>
    public void AddEdge(int firstId, int secondId, float weight)
    {
        var v1 = FindVertex(firstId);
        var v2 = FindVertex(secondId);
        if (v2 != null && v1 != null)
        {
            v1.AddEdge(v2, weight);
            v2.AddEdge(v1, weight);
        }
    }
}