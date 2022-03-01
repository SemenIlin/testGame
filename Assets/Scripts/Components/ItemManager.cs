using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private ChipComponent _chip;
    [SerializeField] private CellComponent _cell;
    [SerializeField] private LineComponent _line;

    [SerializeField] private Colors _colors;

    [SerializeField] private MiniField _miniField;
    private List<MiniChip> _chipsMini;
    private List<MiniCell> _cellsMini;
    private List<LineComponent> _linesMini;

    private List<ChipComponent> _chips;
    private List<CellComponent> _cells;
    private List<LineComponent> _lines;
    private Parser _parser;

    private ChipComponent _selectChip;

    private NewFinderPath find;

    public Action Victory;

    private void Awake()
    {
        _parser = new Parser();
        find = new NewFinderPath();
        
    }
    public void Play(string fileName)
    {
        DestoyField();
        _parser.OnParse(fileName);
        InitItems();        
    }    

    public void InitItems()
    {
        InitCells();
        InitChips();
        InitLines();

        InitMiniCells();
        InitMiniChips();
        InitMiniLines();
    }

    private void InitChips()
    {
        _chips = new List<ChipComponent>();
        for (int i = 0; i < _parser.AmountChips; i++)
        {
            var chip = Instantiate(_chip, transform).GetComponent<ChipComponent>();
            chip.onSelect = DisactiveAllChips;
            
            chip.Init(_parser.PositionsCells[_parser.StartNumbers[i]], i, false, _colors.AllColors[i]);
            
            _chips.Add(chip);
        }
        _chips.ForEach(chip => chip.Finish = EnableAllChips);
    }

    private void InitMiniChips()
    {
        _chipsMini = new List<MiniChip>();
        for (int i = 0; i < _parser.AmountChips; i++)
        {
            var chip = Instantiate(_chip, _miniField.transform).GetComponent<MiniChip>();            

            chip.Init(_parser.PositionsCells[_parser.WinNumbers[i]], i, false, _colors.AllColors[i], MiniField.KOEFF);
            StaticBatchingUtility.Combine(_miniField.gameObject);
            _chipsMini.Add(chip);
        }
    }
    private void InitCells()
    {
        _cells = new List<CellComponent>();
        for (int i = 0; i < _parser.AmountCells; i++)
        {
            var cell = Instantiate(_cell, transform).GetComponent<CellComponent>();
            cell.onSelect = OnSelectCell;
            cell.Init(_parser.PositionsCells[i], i, true);
            _cells.Add(cell);
        }
    }

    private void InitMiniCells()
    {
        _cellsMini = new List<MiniCell>();
        for (int i = 0; i < _parser.AmountCells; i++)
        {
            var cell = Instantiate(_cell, _miniField.transform).GetComponent<MiniCell>();

            cell.Init(_parser.PositionsCells[i], i, true, MiniField.KOEFF);
            _cellsMini.Add(cell);
        }
    }

    private void InitLines()
    {
        _lines = new List<LineComponent>();
        for (int i = 0; i < _parser.AmountConnections; i++)
        {
            var line = Instantiate(_line, transform).GetComponent<LineComponent>();

            line.DrawLine(_parser.PositionsCells[_parser.PairConnections[i].Item1],
                          _parser.PositionsCells[_parser.PairConnections[i].Item2], 
                          transform.position);
            _lines.Add(line);
        }
    }

    private void InitMiniLines()
    {
        _linesMini = new List<LineComponent>();
        var miniFieldTransform = _miniField.transform;
        for (int i = 0; i < _parser.AmountConnections; i++)
        {

            var line = Instantiate(_line, miniFieldTransform).GetComponent<LineComponent>();

            line.DrawLine(_parser.PositionsCells[_parser.PairConnections[i].Item1],
                _parser.PositionsCells[_parser.PairConnections[i].Item2],
                miniFieldTransform.position,
                MiniField.KOEFF);
            _linesMini.Add(line);
        }
    }

    private void DisactiveAllChips(ChipComponent chip)
    {
        _chips.ForEach(chip => chip.Disactive());
        _selectChip = chip;

        DisactiveAllCells();
        ShowCellForMove();
    }

    private void DisactiveAllCells()
    {             
        _cells.ForEach(cell => cell.Disactive());
    }

    private void EnableAllChips()
    {
        _chips.ForEach(chip => chip.EnableCollider());
        OnWin();
    }

    private void ShowCellForMove()
    {
        var cells = find.FindCells(_selectChip.NumberCell, _parser.PairConnections, _cells);
        for (int i = 0; i < cells.Count; i++)
        {
            _cells[cells[i]].UseForTransition();
        }       
    }

    private void OnSelectCell(Cell cell)
    {
        _cells.ForEach(cell => cell.Disactive());

        if (_selectChip == null)
        {
            return;
        }
        if (!cell.IsEmpty ||
            _selectChip.IsMove
            )
        {
            _selectChip.Disactive();

            return;
        }

        var points = find.FindPath(_selectChip.NumberCell, cell.ID, _parser.PairConnections, _cells);
        
        if (points == null)
        {
            _chips[_selectChip.ID].Disactive();
            return;
        }
        for (int i = 0; i < _chips.Count; i++)
        {
            if (_selectChip == _chips[i])
            {
                continue;
            }
            _chips[i].DisableCollider();
        }

        var positions = new List<Vector3>();
        for (int i = points.Count - 1; i >= 0 ; i--)
        {
            positions.Add(_cells[points[i]].Position);
        }

        if ( _cells[cell.ID].IsEmpty)
        {
            _chips[_selectChip.ID].MoveToCell(positions);
        }
        _selectChip = null;
        
    }    
    
    private void OnWin()
    {
        for (int i = 0; i < _chips.Count; i++)
        {
            if (_chips[i].NumberCell != _parser.WinNumbers[i])
            {
                return;
            }
        }
        Victory?.Invoke();
        DestoyField();
    }

    private void DestoyField()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < _miniField.transform.childCount; i++)
        {
            Destroy(_miniField.transform.GetChild(i).gameObject);
        }
    }
}
