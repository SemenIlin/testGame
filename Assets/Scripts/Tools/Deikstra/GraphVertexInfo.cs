/// <summary>
/// ���������� � �������
/// </summary>
public class GraphVertexInfo
{
    /// <summary>
    /// �������
    /// </summary>
    public GraphVertex Vertex { get; set; }

    /// <summary>
    /// �� ���������� �������
    /// </summary>
    public bool IsUnvisited { get; set; }

    /// <summary>
    /// ����� ����� �����
    /// </summary>
    public float EdgesWeightSum { get; set; }

    /// <summary>
    /// ���������� �������
    /// </summary>
    public GraphVertex PreviousVertex { get; set; }

    /// <summary>
    /// �����������
    /// </summary>
    /// <param name="vertex">�������</param>
    public GraphVertexInfo(GraphVertex vertex)
    {
        Vertex = vertex;
        IsUnvisited = true;
        EdgesWeightSum = int.MaxValue;
        PreviousVertex = null;
    }
}