using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� ��������
/// </summary>
public class Dijkstra
{
    readonly Graph _graph;

    List<GraphVertexInfo> _infos;
   HashSet<int> _cellForMove = new HashSet<int>();

    /// <summary>
    /// �����������
    /// </summary>
    /// <param name="graph">����</param>
    public Dijkstra(Graph graph)
    {
        this._graph = graph;
    }

    /// <summary>
    /// ������������� ����������
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
    /// ��������� ���������� � ������� �����
    /// </summary>
    /// <param name="v">�������</param>
    /// <returns>���������� � �������</returns>
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
    /// ����� ������������ ������� � ����������� ��������� �����
    /// </summary>
    /// <returns>���������� � �������</returns>
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
    /// ����� ����������� ���� �� ��������� ������
    /// </summary>
    /// <param name="startId">Id ��������� �������</param>
    /// <param name="finishId">Id �������� �������</param>
    /// <returns>���������� ����</returns>
    public List<int> FindShortestPath(int startId, int finishId)
    {
        return FindShortestPath(_graph.FindVertex(startId), _graph.FindVertex(finishId));
    }

    /// <summary>
    /// ����� ����������� ���� �� ��������
    /// </summary>
    /// <param name="startVertex">��������� �������</param>
    /// <param name="finishVertex">�������� �������</param>
    /// <returns>���������� ����</returns>
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
    /// ���������� ����� ����� ����� ��� ��������� �������
    /// </summary>
    /// <param name="info">���������� � ������� �������</param>
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
    /// ������������ ����
    /// </summary>
    /// <param name="startVertex">��������� �������</param>
    /// <param name="endVertex">�������� �������</param>
    /// <returns>����</returns>
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