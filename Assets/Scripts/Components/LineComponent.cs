using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineComponent : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private void OnEnable()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawLine(Vector3 point1, Vector3 point2, Vector3 delta, float koeff = 1)
    {
        _lineRenderer.SetPositions(new Vector3[] { point1 / koeff + delta, point2 / koeff + delta });
    }
}
