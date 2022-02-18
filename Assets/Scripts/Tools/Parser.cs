using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

public class Parser 
{
    private int _amountChips;
    private int _amountCells;
    private Vector3[] _positionsCells;
    private int[] _startNumbers;
    private int[] _winNumbers;
    private int _amountConnections;
    private (int, int)[] _pairConnections;

    public void OnParse()
    {
        var path = Application.streamingAssetsPath + "/data.txt";
        
        try
        {
            var dataFromFile = File.ReadAllLines(path).ToList();
            int.TryParse(dataFromFile[0], out _amountChips);
            int.TryParse(dataFromFile[1], out _amountCells);

            _positionsCells = new Vector3[_amountCells];
            for (int i = 2, j = 0; i < 2 + _amountCells; j++, i++)
            {
                var positions = dataFromFile[i].Split(',');
                _positionsCells[j] = new Vector3(
                    float.Parse(positions[0]) / 10,
                    float.Parse(positions[1]) / 10, 0);
            }

            var startPositions = dataFromFile[2 + _amountCells].Split(',');
            _startNumbers = new int[_amountChips];
            for (int i = 0; i < _amountChips; i++)
            {
                _startNumbers[i] = int.Parse(startPositions[i]);
            }

            var winPositions = dataFromFile[2 + _amountCells + 1].Split(',');
            _winNumbers = new int[_amountChips];
            for (int i = 0; i < _amountChips; i++)
            {
                _winNumbers[i] = int.Parse(winPositions[i]);
            }

            _amountConnections = int.Parse(dataFromFile[2 + _amountCells + 2]);
            _pairConnections = new (int, int)[_amountConnections];
            for (int i = 2 + _amountCells + 3, j = 0; i < 2 + _amountCells + 3 + _amountConnections; i++, j++)
            {
                var temp = dataFromFile[i].Split(',');
                _pairConnections[j] = (int.Parse(temp[0]), int.Parse(temp[1]));
            }

        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public int AmountChips => _amountChips;
    public int AmountCells => _amountCells;
    public Vector3[] PositionsCells => _positionsCells;

    public int[] StartNumbers => _startNumbers;
    public int[] WinNumbers => _winNumbers;

    public int AmountConnections => _amountConnections;
    public (int, int)[] PairConnections => _pairConnections;

}
