using UnityEngine;

public class Cell : Item
{
    private bool _isEmpty = false;
    public Cell(Vector3 position, int id, bool isEmpty ) : base(position, id)
    {
        _isEmpty = isEmpty;
    }

    public bool IsEmpty { get
        { 
            return _isEmpty;
        } 
        set
        {
            _isEmpty = value;
        } 
    }

    public bool CanUseForTransition { get; set; }
   
}
