using System.Collections.Generic;
/// <summary>
/// ������� �����
/// </summary>
public class GraphVertex
{
    /// <summary>
    /// ����� �������
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// ������ �����
    /// </summary>
    public List<GraphEdge> Edges { get; }

    /// <summary>
    /// �����������
    /// </summary>
    /// <param name="vertexName">�������� �������</param>

    public bool IsVisited { get; set; }
    
    public GraphVertex(int vertexId)
    {
        Id = vertexId;
        Edges = new List<GraphEdge>();
        IsVisited = false;
    }

    /// <summary>
    /// �������� �����
    /// </summary>
    /// <param name="newEdge">�����</param>
    public void AddEdge(GraphEdge newEdge)
    {
        Edges.Add(newEdge);
    }

    /// <summary>
    /// �������� �����
    /// </summary>
    /// <param name="vertex">�������</param>
    /// <param name="edgeWeight">���</param>
    public void AddEdge(GraphVertex vertex, float edgeWeight)
    {
        AddEdge(new GraphEdge(vertex, edgeWeight));
    }
}