using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private ChipComponent _chip;
    [SerializeField] private CellComponent _cell;
    [SerializeField] private LineComponent _line;


    [SerializeField] private Colors _colors;

    private List<ChipComponent> _chips;
    private List<CellComponent> _cells;
    private List<LineComponent> _lines;
    private Parser _parser;
    private FinderPath _finderPath;

    private Chip _selectChip = null;

    private void Awake()
    {
        _parser = new Parser();
        _finderPath = new FinderPath();
        _parser.OnParse();
    }
    void Start()
    {
        InitItems();
    }    

    private void InitItems()
    {
        InitCells();
        InitChips();
        InitLines();
    }

    private void InitChips()
    {
        _chips = new List<ChipComponent>();
        for (int i = 0; i < _parser.AmountChips; i++)
        {
            var chip = Instantiate(_chip, gameObject.transform).GetComponent<ChipComponent>();
            chip.onSelect = DisactiveAllChips;
            chip.Init(_parser.PositionsCells[_parser.StartNumbers[i] - 1], i, false, _colors.AllColors[i]);
            _chips.Add(chip);
        }
    }
    private void InitCells()
    {
        _cells = new List<CellComponent>();
        for (int i = 0; i < _parser.AmountCells; i++)
        {
            var cell = Instantiate(_cell, gameObject.transform).GetComponent<CellComponent>();
            cell.onSelect = OnSelectCell;
            cell.Init(_parser.PositionsCells[i], i, true);
            _cells.Add(cell);
        }
    }

    private void InitLines()
    {
        _lines = new List<LineComponent>();
        for (int i = 0; i < _parser.AmountConnections; i++)
        {
            var line = Instantiate(_line, gameObject.transform).GetComponent<LineComponent>();

            line.DrawLine(_parser.PositionsCells[_parser.PairConnections[i].Item1 - 1],
                          _parser.PositionsCells[_parser.PairConnections[i].Item2 - 1]);
            _lines.Add(line);
        }
    }

    private void DisactiveAllChips(Chip chip)
    {
        _chips.ForEach(chip => chip.Disactive());
        _selectChip = chip;
    }

    private void OnSelectCell(Vector3 position, int id)
    {
        if (_selectChip == null)
        {
            return;
        }
        if (_selectChip.IsMove)
        {
            return;
        }

        var points = _finderPath.FindPath(_selectChip.ID, id, _parser.AmountCells, _parser.PairConnections, _cells);
        var positions = new List<Vector3>();
        for (int i = 0; i < points.Count; i++)
        {
            positions.Add(_cells[points[i]].Position);
        }

        if ( _cells[id].IsEmpty)
        {
            _chips[_selectChip.ID].MoveToCell(positions);
        }
    }
        
}
