using System;
using UnityEngine;

public class CellComponent : MiniCell
{
    [SerializeField] private ItemOutline _outline;
  

    public Action<Cell> onSelect;

    public int ID => _cell.ID;
    public Vector3 Position => _cell.Position;
    public bool IsEmpty => _cell.IsEmpty;
    private void OnMouseDown()
    {
        onSelect?.Invoke(_cell);
        _outline.gameObject.SetActive(false);
    }

    public void UseForTransition()
    {
        _cell.CanUseForTransition = true;
        _outline.gameObject.SetActive(true);
    }

    public void Disactive() 
    {
        _outline.gameObject.SetActive(false);
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ChipComponent>())
        {
            _cell.IsEmpty = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ChipComponent>())
        {
            _cell.IsEmpty = true;
        }
    }
}
