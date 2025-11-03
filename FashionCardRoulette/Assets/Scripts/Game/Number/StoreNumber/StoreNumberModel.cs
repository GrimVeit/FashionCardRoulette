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
        switch (_option)
        {
            case 0:
                return _numbers[3];
            case 1:
                return _numbers[Random.Range(2, 5)];
            case 2:
                return _numbers[Random.Range(1, 6)];
            case 3:
                return _numbers[Random.Range(0, 7)];
            case 4:
                return _numbers[Random.Range(0, 37)];
            default:
                return _numbers[Random.Range(0, 37)];
        }
    }

    private void SetNumbers(List<int> numbers)
    {
        _numbers = numbers;
    }
}
