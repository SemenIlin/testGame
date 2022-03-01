using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MiniChip : MonoBehaviour
{
    protected Chip _chip;
    protected MeshRenderer _meshRenderer;
    public void Init(Vector3 position, int id, bool isSelect, Color color, float koeff = 1)
    {
        _chip = new Chip(position / koeff, id, isSelect);
        transform.localPosition = _chip.Position;

        SetColor(color);
    }
    protected void SetColor(Color color)
    {
        _meshRenderer.material.color = color;
    }
}
