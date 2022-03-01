using UnityEngine;

public class MiniCell : MonoBehaviour
{
   protected Cell _cell;
    public void Init(Vector3 position, int id, bool isEmpty, float koeff = 1)
    {
        _cell = new Cell(position / koeff, id, isEmpty);
        transform.localPosition = _cell.Position;
    }
}
