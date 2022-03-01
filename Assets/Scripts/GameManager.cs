using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ItemManager _itemManager;
    [SerializeField] private UI _ui;

    private void Start()
    {
        _itemManager.Victory = _ui.ShowBackground;
    }
}
