using System;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(BoxCollider))]
public class ChipComponent : MiniChip
{
    [SerializeField, Range(0.2f, 3)] private float _duration = 1f;
    [SerializeField] private ItemOutline _outline;
    
    private BoxCollider _collider;

    public Action<ChipComponent> onSelect;
    public Action Finish;

    private int _numberCell;

    private void OnEnable()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<BoxCollider>();
    }

    private void OnMouseDown()
    {
        onSelect?.Invoke(this);
        _chip.IsSelect = true;
        _outline.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CellComponent cell))
        {
            _numberCell = cell.ID;
        }
    }

    public int NumberCell => _numberCell;

    public int ID => _chip.ID;

    public bool IsMove => _chip.IsMove;
    public void Active()
    {
        _chip.IsSelect = true;
    }
    public void Disactive()
    {
        _chip.IsSelect = false;
        _outline.gameObject.SetActive(false);
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
    }

    public void EnableCollider()
    {
        _collider.enabled = true;
    }
    public void Init(Vector3 position, int id, bool isSelect, Color color)
    {
        _chip = new Chip(position, id, isSelect);
        transform.position = position;
        SetColor(color);
    }
    public void MoveToCell(List<Vector3> endPositions)
    {
        _chip.IsMove = true;
        var siquence = DOTween.Sequence();
        for (int i = 0; i < endPositions.Count - 1; i++)
        {
            siquence.Append(transform.DOMove(endPositions[i], _duration));
        }
        if (endPositions.Count > 0)
        {
            siquence.Append(transform.DOMove(endPositions[endPositions.Count - 1], _duration).OnComplete(() =>
            {
                _chip.IsMove = false;
                _outline.gameObject.SetActive(false);                

                Finish?.Invoke();
            }));
        }
    }
}
