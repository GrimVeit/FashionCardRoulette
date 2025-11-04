using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNumberModel
{
    private INumberSelectionEventsProvider _numberSelectionEventsProvider;

    private List<int> _numbers = new();
    private int _option = 4;

    public StoreNumberModel(INumberSelectionEventsProvider numberSelectionEventsProvider)
    {
        _numberSelectionEventsProvider = numberSelectionEventsProvider;
        _numberSelectionEventsProvider.OnChooseSevenNumbers += SetNumbers;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _numberSelectionEventsProvider.OnChooseSevenNumbers -= SetNumbers;
    }

    public void SetSector(int sector)
    {
        _option = sector;
    }

    public int GetRandomNumber()
    {
        return _option switch
        {
            0 => _numbers[3],
            1 => _numbers[Random.Range(2, 5)],
            2 => _numbers[Random.Range(1, 6)],
            3 => _numbers[Random.Range(0, 7)],
            4 => Random.Range(0, 37),
            _ => Random.Range(0, 37),
        };
    }

    private void SetNumbers(List<int> numbers)
    {
        _numbers = numbers;
    }
}
