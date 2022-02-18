using System;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(BoxCollider))]
public class ChipComponent : MonoBehaviour
{
    [SerializeField, Range(0.2f, 3)] private float _duration = 1f;
    [SerializeField] private ItemOutline _outline;

    private Chip _chip;
    private MeshRenderer _meshRenderer;
    private BoxCollider _collider;

    public Action<Chip> onSelect;

    private int _numberCell;

    private void OnEnable()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<BoxCollider>();
    }
    private void SetColor(Color color)
    {
        _meshRenderer.material.color = color;
    }

    private void OnMouseDown()
    {
        onSelect?.Invoke(_chip);
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
    public void Disactive()
    {
        _chip.IsSelect = false;
        _outline.gameObject.SetActive(false);
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
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
        siquence.Append(transform.DOMove(endPositions[endPositions.Count - 1], _duration).OnComplete(()=> 
        {
            _chip.IsMove = false;
            _collider.enabled = true;
        }));
    }
}
