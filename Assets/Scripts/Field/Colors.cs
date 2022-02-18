using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Colors", order = 1)]
public class Colors: ScriptableObject
{
    [SerializeField] private List<Color> _allColors = new List<Color>();

    public List<Color> AllColors => _allColors;
}
