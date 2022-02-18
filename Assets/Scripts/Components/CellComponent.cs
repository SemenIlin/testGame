using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellComponent : MonoBehaviour
{
    [SerializeField] private ItemOutline _outline;
    private Cell _cell;

    public Action<Vector3, int> onSelect;

    public int ID => _cell.ID;
    public Vector3 Position => _cell.Position;
    public bool IsEmpty => _cell.IsEmpty;
    private void OnMouseDown()
    {
        onSelect?.Invoke(transform.position, _cell.ID);
        _outline.gameObject.SetActive(false);
    }

    public void UseForTransition()
    {
        _cell.CanUseForTransition = true;
        _outline.gameObject.SetActive(true);
    }

    public void Init(Vector3 position, int id, bool isEmpty)
    {
        _cell = new Cell(position, id, isEmpty);
        transform.position = position;        
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
