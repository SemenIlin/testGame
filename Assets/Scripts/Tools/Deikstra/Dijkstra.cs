using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Алгоритм Дейкстры
/// </summary>
public class Dijkstra
{
    readonly Graph _graph;

    List<GraphVertexInfo> _infos;
   HashSet<int> _cellForMove = new HashSet<int>();

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="graph">Граф</param>
    public Dijkstra(Graph graph)
    {
        this._graph = graph;
    }

    /// <summary>
    /// Инициализация информации
    /// </summary>
    void InitInfo()
    {
        _infos = new List<GraphVertexInfo>();
        foreach (var v in _graph.Vertices)
        {
            _infos.Add(new GraphVertexInfo(v));
        }
    }

    /// <summary>
    /// Получение информации о вершине графа
    /// </summary>
    /// <param name="v">Вершина</param>
    /// <returns>Информация о вершине</returns>
    GraphVertexInfo GetVertexInfo(GraphVertex v)
    {
        foreach (var i in _infos)
        {
            if (i.Vertex.Equals(v))
            {
                return i;
            }
        }

        return null;
    }

    /// <summary>
    /// Поиск непосещенной вершины с минимальным значением суммы
    /// </summary>
    /// <returns>Информация о вершине</returns>
    public GraphVertexInfo FindUnvisitedVertexWithMinSum()
    {
        var minValue = float.MaxValue;
        GraphVertexInfo minVertexInfo = null;
        foreach (var i in _infos)
        {
            if (i.IsUnvisited && i.EdgesWeightSum < minValue)
            {
                minVertexInfo = i;
                minValue = i.EdgesWeightSum;
            }
        }

        return minVertexInfo;
    }

    /// <summary>
    /// Поиск кратчайшего пути по названиям вершин
    /// </summary>
    /// <param name="startId">Id стартовой вершины</param>
    /// <param name="finishId">Id финишной вершины</param>
    /// <returns>Кратчайший путь</returns>
    public List<int> FindShortestPath(int startId, int finishId)
    {
        return FindShortestPath(_graph.FindVertex(startId), _graph.FindVertex(finishId));
    }

    /// <summary>
    /// Поиск кратчайшего пути по вершинам
    /// </summary>
    /// <param name="startVertex">Стартовая вершина</param>
    /// <param name="finishVertex">Финишная вершина</param>
    /// <returns>Кратчайший путь</returns>
    public List<int> FindShortestPath(GraphVertex startVertex, GraphVertex finishVertex)
    {
        InitInfo();
        var first = GetVertexInfo(startVertex);
        first.EdgesWeightSum = 0;
        while (true)
        {
            var current = FindUnvisitedVertexWithMinSum();
            if (current == null)
            {
                break;
            }

            SetSumToNextVertex(current);
        }

        return GetPath(startVertex, finishVertex);
    }

    public void FindCellForMove(int startId)
    {
        var startVertex = _graph.FindVertex(startId);
        
        if (startVertex.Edges.Count == 0 || startVertex.IsVisited)
        {           
            return;
        }
        startVertex.IsVisited = true;

        for (int i = 0; i < startVertex.Edges.Count; i++)
        {
            var connectedVertex = startVertex.Edges[i].ConnectedVertex;
            if (connectedVertex.IsVisited)
            {
                continue;
            }
            var id = connectedVertex.Id;

            _cellForMove.Add(id);
           
            FindCellForMove(id);
        }        
    }

    public HashSet<int> CellForMove => _cellForMove;

    /// <summary>
    /// Вычисление суммы весов ребер для следующей вершины
    /// </summary>
    /// <param name="info">Информация о текущей вершине</param>
    void SetSumToNextVertex(GraphVertexInfo info)
    {
        info.IsUnvisited = false;
        foreach (var e in info.Vertex.Edges)
        {
            var nextInfo = GetVertexInfo(e.ConnectedVertex);
            var sum = info.EdgesWeightSum + e.EdgeWeight;
            if (sum < nextInfo.EdgesWeightSum)
            {
                nextInfo.EdgesWeightSum = sum;
                nextInfo.PreviousVertex = info.Vertex;
            }
        }
    }

    /// <summary>
    /// Формирование пути
    /// </summary>
    /// <param name="startVertex">Начальная вершина</param>
    /// <param name="endVertex">Конечная вершина</param>
    /// <returns>Путь</returns>
    List<int> GetPath(GraphVertex startVertex, GraphVertex endVertex)
    {
        var path = new List<int>
        {
            endVertex.Id
        };
        while (startVertex != endVertex)
        {
            endVertex = GetVertexInfo(endVertex).PreviousVertex;
            if (endVertex == null)
            {
                return null;
            }
            path.Add(endVertex.Id);
        }

        return path;
    }
}