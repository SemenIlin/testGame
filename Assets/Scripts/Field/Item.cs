using UnityEngine;

public class Item 
{
    private Vector3 _position;
    private readonly int _id;

    public Item(Vector3 position, int id) 
    {
        _id = id;
        _position = position;    
    }

    public int ID => _id;

    public Vector3 Position { 
        get 
        {
            return _position;
        }
        set
        {
            _position = value;
        }
    }

}
