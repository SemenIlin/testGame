using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinderPath 
{
    private float [,] matrix;
    private float[] minDistance;
    private int[] visitetVertex;
    int minIndex = int.MaxValue;
    float min = float.MaxValue;

    public List<int> FindPath(int selectedChip, int selectedCell, int amountCells, (int,int)[] connections, List<CellComponent> cells)
    {
        var stepsOfPath = new List<int>();
        matrix = new float[amountCells, amountCells];
        minDistance = new float[amountCells];
        visitetVertex = new int[amountCells];

        OnNullMatrix(amountCells);
        FillDataMatrix(selectedChip, connections, cells);
        
        var begin_index = selectedChip;
        for (int i = 0; i < amountCells; i++)
        {
            minDistance[i] = float.MaxValue;
            visitetVertex[i] = 1;
        }
        minDistance[begin_index] = 0;

        do
        {
            minIndex = int.MaxValue;
            min = float.MaxValue;
            for (int i = 0; i < amountCells; i++)
            {
                if (visitetVertex[i] == 1 && minDistance[i] < min)
                {
                    min = minDistance[i];
                    minIndex = i;
                }
            }

            if (minIndex != int.MaxValue)
            {
                for (int i = 0; i < amountCells; i++)
                {
                    if (matrix[minIndex, i] > 0)
                    {
                        var distance = min + matrix[minIndex, i];
                        if (distance < minDistance[i])
                        {
                            minDistance[i] = distance;
                        }
                    }
                }

                visitetVertex[minIndex] = 0;
            }
        } while (minIndex < int.MaxValue);

        stepsOfPath.Add(selectedCell);        
        var end = selectedCell;
        var weight = minDistance[selectedCell];

        while (end != begin_index)
        {
            for (int i = 0; i < amountCells; i++)
            {
                if (matrix[i, end] != 0)
                {
                    var temp = weight - matrix[i, end];
                    if (temp == minDistance[i])
                    {
                        weight = temp;
                        end = i;
                        stepsOfPath.Add(i);
                    }
                }

                if (stepsOfPath.Count > 1000)
                {
                    return new List<int>();
                }
            }
        }

        return stepsOfPath; 
    }

    private void OnNullMatrix(int amountCells)
    {
        for (int i = 0; i < amountCells; i++)
        {
            matrix[i, i] = 0;
            for (int j = i + 1; j < amountCells; j++)
            {
                matrix[i, j] = 0;
                matrix[j, i] = 0;
            }
        }
    }

    private void FillDataMatrix(int selectedChip, (int, int)[] connections, List<CellComponent> cells)
    {
        for (int i = 0; i < connections.Length; i++)
        {
            var index1 = connections[i].Item1 - 1;
            var index2 = connections[i].Item2 - 1;

            var cell1 = cells[index1];
            var cell2 = cells[index2];
            if (cell1.IsEmpty && cell2.IsEmpty ||
                index1 == selectedChip && cell2.IsEmpty ||
                index2 == selectedChip && cell1.IsEmpty)
            {
                var distance = CalculateDistance(cell1.Position, cell2.Position);
                matrix[index1, index2] = distance;
                matrix[index2, index1] = distance;
            }
        }
    }

    private float CalculateDistance(Vector3 point1, Vector3 point2)
    {
        var deltaX = point1.x - point2.x;
        var deltaY = point1.y - point2.y;

        return Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }
    
}
