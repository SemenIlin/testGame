using System.Collections.Generic;
/// <summary>
/// ����
/// </summary>
public class Graph
{
    /// <summary>
    /// ������ ������ �����
    /// </summary>
    public List<GraphVertex> Vertices { get; }

    /// <summary>
    /// �����������
    /// </summary>
    public Graph()
    {
        Vertices = new List<GraphVertex>();
    }

    /// <summary>
    /// ���������� �������
    /// </summary>
    /// <param name="vertexId">Id �������</param>
    public void AddVertex(int vertexId)
    {
        Vertices.Add(new GraphVertex(vertexId));
    }

    /// <summary>
    /// ����� �������
    /// </summary>
    /// <param name="vertexName">�������� �������</param>
    /// <returns>��������� �������</returns>
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
    /// ���������� �����
    /// </summary>
    /// <param name="firstId">Id ������ �������</param>
    /// <param name="secondId">Id ������ �������</param>
    /// <param name="weight">��� ����� ������������ �������</param>
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