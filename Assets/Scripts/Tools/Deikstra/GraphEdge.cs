/// <summary>
/// ����� �����
/// </summary>
public class GraphEdge
{
    /// <summary>
    /// ��������� �������
    /// </summary>
    public GraphVertex ConnectedVertex { get; }

    /// <summary>
    /// ��� �����
    /// </summary>
    public float EdgeWeight { get; }

    /// <summary>
    /// �����������
    /// </summary>
    /// <param name="connectedVertex">��������� �������</param>
    /// <param name="weight">��� �����</param>
    public GraphEdge(GraphVertex connectedVertex, float weight)
    {
        ConnectedVertex = connectedVertex;
        EdgeWeight = weight;
    }
}
