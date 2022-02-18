using UnityEngine;

public class Chip :Item
{
    private bool _isSelect = false;
    private bool _isMove = false;
    public Chip(Vector3 position, int id, bool isSelect) : base(position, id)
    {
        _isSelect = isSelect;
    }

    public bool IsSelect
    {
        get { return _isSelect; }
        set { _isSelect = value; }
    }

    public bool IsMove
    {
        get { return _isMove; }
        set { _isMove = value; }
    }
}
