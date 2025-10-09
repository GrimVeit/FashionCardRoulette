using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class BetModel
{
    public event Action OnWin;
    public event Action OnLose;

    public event Action OnAddBet;
     
    public event Action<int, Chip, int, TypeCell, Vector3> OnAddChip;
    public event Action<int, int> OnReturnChip;
    public event Action<int, int> OnFallenChip;

    public event Action<int> OnGetWin;

    private readonly Bets _bets;
    private readonly List<RouletteNumber> rouletteNumbers = new();

    private readonly List<BetInfo> _currentBets = new();
    private readonly List<BetInfo> _savedBets = new();

    private readonly List<IRouletteValueProvider> _rouletteValueProviders = new();

    private readonly HashSet<int> winningPosIndexes = new();

    private readonly IMoneyProvider _moneyProvider;
    private readonly ISoundProvider _soundProvider;

    private bool isNoneStartBet = false;

    public BetModel(Bets bets, List<IRouletteValueProvider> rouletteValueProviders, IMoneyProvider moneyProvider, ISoundProvider soundProvider)
    {
        _bets = bets;
        _rouletteValueProviders = rouletteValueProviders;
        _moneyProvider = moneyProvider;
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        _rouletteValueProviders.ForEach(rvp => rvp.OnGetRouletteSlotValue += SetRouletteNumber);
    }

    public void Dispose()
    {
        _rouletteValueProviders.ForEach(rvp => rvp.OnGetRouletteSlotValue -= SetRouletteNumber);
    }

    public void SetRouletteNumber(RouletteNumber rouletteNumber)
    {
        rouletteNumbers.Add(rouletteNumber);
    }

    public void AddChip(int id, Chip chip, List<int> positionIndexes, TypeCell typeCell, bool isNumber, Vector3 vector)
    {
        for (int i = 0; i < positionIndexes.Count; i++)
        {
            //RemoveChipFromStore(id);
            OnAddChip?.Invoke(id, chip, positionIndexes[i], typeCell, vector);

            _currentBets.Add(new BetInfo(id, chip, positionIndexes[i]));
        }

        if (_moneyProvider.GetMoney() <= 0)
        {
            isNoneStartBet = true;

            _moneyProvider.SendMoney(0);
        }
        else
        {
            isNoneStartBet = false;

            _moneyProvider.SendMoney(-1);
        }

        _soundProvider.PlayOneShot("ChipDrop");
        OnAddBet?.Invoke();
    }
    
    public void ReturnLastChip()
    {
        if (_currentBets.Count == 0)
        {
            //_soundProvider.PlayOneShot("Error");
            return;
        }

        var lastBet = _currentBets.Last();

        //AddChipInStore(lastBet.IdChipGroup);
        OnReturnChip?.Invoke(lastBet.IdChipGroup, lastBet.PosIndex);

        _currentBets.Remove(lastBet);

        if(!isNoneStartBet)
            _moneyProvider.SendMoney(lastBet.Chip.Nominal);

        _soundProvider.PlayOneShot("Whoosh");
    }

    public void ReturnAllChips()
    {
        if (_currentBets.Count == 0)
        {
            //_notificationProvider.SendMessage("All chips have already been removed", "<color=#ffccd4>Action Not Needed!</color>", 1);
            //_soundProvider.PlayOneShot("Error");
            return;
        }

        for (int i = 0; i < _currentBets.Count; i++)
        {
            //AddChipInStore(_currentBets[i].IdChipGroup);
            if (!isNoneStartBet)
                _moneyProvider.SendMoney(_currentBets[i].Chip.Nominal);
            OnReturnChip?.Invoke(_currentBets[i].IdChipGroup, _currentBets[i].PosIndex);
        }

        //_soundProvider.PlayOneShot("Whoosh");

        _currentBets.Clear();
    }

    public void ReturnAllBets()
    {
        if(_savedBets.Count == 0)
        {
            //_notificationProvider.SendMessage("No previous bets to repeat", "<color=#ffccd4>Action Not Needed!</color>", 1);
            _soundProvider.PlayOneShot("Error");
            return;
        }

        var requiredChips = new Dictionary<int, int>();

        foreach (var chip in _savedBets)
        {
            if (requiredChips.ContainsKey(chip.IdChipGroup))
            {
                requiredChips[chip.IdChipGroup] += 1;
            }
            else
            {
                requiredChips[chip.IdChipGroup] = 1;
            }
        }

        Rebet();
    }

    private void Rebet()
    {
        if (_moneyProvider.GetMoney() <= 0)
        {
            isNoneStartBet = true;
        }
        else
        {
            isNoneStartBet = false;
        }

        Dictionary<(int, int), int> currentBetCount = new();

        foreach(var bet in _currentBets)
        {
            var key = (bet.IdChipGroup, bet.PosIndex);

            if (currentBetCount.ContainsKey(key))
            {
                currentBetCount[key]++;
            }
            else
            {
                currentBetCount[key] = 1;
            }
        }

        List<BetInfo> betsToRemove = new List<BetInfo>();

        foreach (var savedBet in _savedBets)
        {
            var key = (savedBet.IdChipGroup, savedBet.PosIndex);

            int savedCount = _savedBets.Count(b => b.IdChipGroup == savedBet.IdChipGroup && b.PosIndex == savedBet.PosIndex);

            if (currentBetCount.ContainsKey(key))
            {
                int currentCount = currentBetCount[key];

                if(currentCount > savedCount)
                {
                    int toRemove = currentCount - savedCount;

                    for (int i = 0; i < toRemove; i++)
                    {
                        var betToRemove = _currentBets.FirstOrDefault(b => b.IdChipGroup == savedBet.IdChipGroup && b.PosIndex == savedBet.PosIndex);
                        betsToRemove.Add(betToRemove);
                    }
                }
            }
        }

        foreach (var betToRemove in betsToRemove)
        {
            _currentBets.Remove(betToRemove);
            OnReturnChip?.Invoke(betToRemove.IdChipGroup, betToRemove.PosIndex);
        }


        Dictionary<(int, int), int> savedBetCount = new();

        foreach (var savedBet in _savedBets)
        {
            var key = (savedBet.IdChipGroup, savedBet.PosIndex);

            if (savedBetCount.ContainsKey(key))
            {
                savedBetCount[key]++;
            }
            else
            {
                savedBetCount[key] = 1;
            }
        }

        foreach (var savedBet in savedBetCount)
        {
            int savedCount = savedBet.Value;
            int currentCount = _currentBets.Count(b => b.IdChipGroup == savedBet.Key.Item1 && b.PosIndex == savedBet.Key.Item2);

            if(currentCount < savedCount)
            {
                int toAdd = savedCount - currentCount;

                for (int i = 0; i < toAdd; i++)
                {
                    var betInfo = new BetInfo(savedBet.Key.Item1, _savedBets.FirstOrDefault(b => b.IdChipGroup == savedBet.Key.Item1 && b.PosIndex == savedBet.Key.Item2).Chip, savedBet.Key.Item2);
                    _currentBets.Add(betInfo);
                    OnAddChip?.Invoke(betInfo.IdChipGroup, betInfo.Chip, betInfo.PosIndex, TypeCell.Tracker, new Vector3());

                    if (!isNoneStartBet && i == 0)
                        _moneyProvider.SendMoney(-betInfo.Chip.Nominal);
                }
            }
        }
    }

    public void SearchWin()
    {
        float totalWin = 0;
        winningPosIndexes.Clear();

        Debug.Log(rouletteNumbers.Count);

        foreach (var number in rouletteNumbers)
        {
            for (int i = 0; i < _currentBets.Count; i++)
            {
                var betInfo = _currentBets[i];

                Bet bet = _bets.GetBetById(betInfo.PosIndex);

                if (bet.Numbers.Contains(number.Number))
                {
                    float win = betInfo.Chip.Nominal * bet.MultiplyPayout;
                    totalWin += win;
                }

                if (winningPosIndexes.Contains(betInfo.PosIndex))
                    continue;

                Debug.Log("Number:" + number.NumberVisual + "//" + bet + "//" + betInfo.PosIndex);

                if (bet.Numbers.Contains(number.Number))
                {
                    winningPosIndexes.Add(betInfo.PosIndex);
                }
            }
        }

        float winFloat = Mathf.Round(totalWin * 10f) / 10f;
        int winInt = (int)Math.Round(winFloat, 1);
        _moneyProvider.SendMoney(winInt);
        OnGetWin?.Invoke(winInt);

        if (winInt == 0)
        {
            OnLose?.Invoke();
        }
        else
        {
            OnWin?.Invoke();
        }

        isNoneStartBet = false;

        Debug.Log("Winnings:" + string.Join(", ", winningPosIndexes));
    }

    public void ClearTable()
    {
        foreach (var currentBet in _currentBets)
        {
            var listChips = _currentBets.Where(data => data.PosIndex == currentBet.PosIndex).ToList();

            if (!winningPosIndexes.Contains(currentBet.PosIndex))
            {
                for (int i = 0; i < listChips.Count; i++)
                {
                    OnFallenChip?.Invoke(listChips[i].IdChipGroup, listChips[i].PosIndex);
                }

                Debug.Log("Failure:" + string.Join(", ", listChips));
            }
        }

        foreach (var currentBetIndex in winningPosIndexes)
        {
            var listChips = _currentBets.Where(data => data.PosIndex == currentBetIndex).ToList();

            for (int i = 0; i < listChips.Count; i++)
            {
                OnReturnChip?.Invoke(listChips[i].IdChipGroup, listChips[i].PosIndex);
            }

            Debug.Log("Winnings:" + string.Join(", ", listChips));
        }

        _savedBets.Clear();
        _savedBets.AddRange(_currentBets);
        _currentBets.Clear();
        winningPosIndexes.Clear();
        rouletteNumbers.Clear();
    }

}

public record BetInfo
{
    public int IdChipGroup;
    public Chip Chip;
    public int PosIndex;

    public BetInfo(int idChipGroup, Chip chip, int posIndex)
    {
        IdChipGroup = idChipGroup;
        PosIndex = posIndex;
        Chip = chip;
    }
}
