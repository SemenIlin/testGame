using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewFinderPath
{
    private Graph _graph = new Graph();
    private Dijkstra _dijkstra;
   
    private void UpdateGraph()
    {
        _graph = new Graph();
    }
    public List<int> FindCells(int selectedChip,  (int, int)[] connections, List<CellComponent> cells)
    {
        _graph = new Graph();
        FillGraph(selectedChip, connections, cells);

        _dijkstra = new Dijkstra(_graph);
        _dijkstra.FindCellForMove(selectedChip);

        return _dijkstra.CellForMove.ToList();
    }
    public List<int> FindPath(int selectedChip, int selectedCell, (int, int)[] connections, List<CellComponent> cells)
    {
        FillGraph(selectedChip, connections, cells);
        var path = _dijkstra.FindShortestPath(selectedChip, selectedCell);
        
        UpdateGraph();

        return path;

    }

    private void FillGraph(int selectedChip, (int, int)[] connections, List<CellComponent> cells)
    {
        //добавление вершин
        foreach (var cell in cells)
        {
            _graph.AddVertex(cell.ID);
        }

        //добавление ребер
        for (int i = 0; i < connections.Length; i++)
        {
            var index1 = connections[i].Item1;
            var index2 = connections[i].Item2;

            var cell1 = cells[index1];
            var cell2 = cells[index2];
            if (cell1.IsEmpty && cell2.IsEmpty ||
                index1 == selectedChip && cell2.IsEmpty ||
                index2 == selectedChip && cell1.IsEmpty)
            {
                var distance = CalculateDistance(cell1.Position, cell2.Position);
                _graph.AddEdge(index1, index2, distance);
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
