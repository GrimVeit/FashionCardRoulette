using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StoreClothesModel
{
    public event Action<Clothes> OnChooseClothes;

    public event Action<Clothes> OnOpenClothes;

    //public event Action<Chip> OnOpenChip;
    //public event Action<Chip> OnOpenNewChip;
    //public event Action<Chip> OnCloseChip;

    //public event Action<Chip> OnDeselectChip;
    //public event Action<Chip> OnSelectChip;


    private readonly ClothesAllGroup _clothesAllGroup;

    private readonly List<ClothesGroupData> clothesGroupDatas = new();

    public readonly string FilePath = Path.Combine(Application.persistentDataPath, "Clothes.json");

    private List<Clothes> _currentClothes = new();

    public StoreClothesModel(ClothesAllGroup clothesAllGroup)
    {
        _clothesAllGroup = clothesAllGroup;

        if (File.Exists(FilePath))
        {
            string loadedJson = File.ReadAllText(FilePath);
            AllClothesDatas allClothesDatas = JsonUtility.FromJson<AllClothesDatas>(loadedJson);

            Debug.Log("Load data");

            this.clothesGroupDatas = allClothesDatas.Datas.ToList();
        }
        else
        {
            Debug.Log("New Data");

            clothesGroupDatas = new();

            for (int i = 0; i < _clothesAllGroup.Groups.Count; i++)
            {
                List<ClothesData> clothesDatas = new();

                for (int j = 0; j < _clothesAllGroup.Groups[i].Clothes.Count; j++)
                {
                    if(i == 0)
                    {
                        var data = new ClothesData(true, true);

                        clothesDatas.Add(data);

                        _clothesAllGroup.Groups[i].Clothes[j].SetData(data);
                    }
                    else
                    {
                        var data = new ClothesData(false, false);

                        clothesDatas.Add(data);

                        _clothesAllGroup.Groups[i].Clothes[j].SetData(data);
                    }
                }

                clothesGroupDatas.Add(new ClothesGroupData(clothesDatas.ToArray(), _clothesAllGroup.Groups[i].ClothesType));
            }
        }
    }

    public void Initialize()
    {
        for (int i = 0; i < _clothesAllGroup.Groups.Count; i++)
        {
            _clothesAllGroup.Groups[i].Clothes[0].Data.IsSelect = true;
            OnOpenClothes?.Invoke(_clothesAllGroup.Groups[i].Clothes[0]);
        }
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(new AllClothesDatas(clothesGroupDatas.ToArray()));
        File.WriteAllText(FilePath, json);
    }

    public void ChooseByClothesType(ClothesType clothesType)
    {
        _currentClothes = _clothesAllGroup.Groups.FirstOrDefault(data => data.ClothesType == clothesType).Clothes.ToList();

        _currentClothes.ForEach(data =>
        {
            OnChooseClothes?.Invoke(data);
        });
    }

    //public void SelectChip(int number)
    //{
    //    var chip = _clothesAllGroup.GetChipById(number);

    //    if (chip == null)
    //    {
    //        Debug.LogError($"Not found chip by id - {number}");
    //        return;
    //    }

    //    if (chip.ChipData.IsSelect)
    //    {
    //        chip.ChipData.IsSelect = false;
    //        OnDeselectChip?.Invoke(chip);
    //    }
    //    else
    //    {
    //        chip.ChipData.IsSelect = true;
    //        OnSelectChip?.Invoke(chip);
    //    }
    //}

    //public void UnselectAllChips()
    //{
    //    _clothesAllGroup.Chips.ForEach(data =>
    //    {
    //        if (data.ChipData.IsSelect)
    //        {
    //            data.ChipData.IsSelect = false;
    //            OnDeselectChip?.Invoke(data);
    //        }
    //    });
    //}

    //public void OpenChip(int number)
    //{
    //    var chip = _clothesAllGroup.GetChipById(number);

    //    if (chip == null)
    //    {
    //        Debug.LogError($"Not found chip by id - {number}");
    //        return;
    //    }

    //    if (chip.ChipData.IsOpen)
    //    {
    //        Debug.LogWarning($"Chip by id - {number} is already open");
    //    }
    //    else
    //    {
    //        chip.ChipData.IsOpen = true;
    //        OnOpenChip?.Invoke(chip);
    //        OnOpenNewChip?.Invoke(chip);
    //    }
    //}
}

[Serializable]
public class AllClothesDatas
{
    public ClothesGroupData[] Datas;

    public AllClothesDatas(ClothesGroupData[] datas)
    {
        Datas = datas;
    }
}

[Serializable]
public class ClothesGroupData
{
    public ClothesType ClothesType;
    public ClothesData[] Datas;

    public ClothesGroupData(ClothesData[] datas, ClothesType clothesType)
    {
        Datas = datas;
        ClothesType = clothesType;  
    }
}

[Serializable]
public class ClothesData
{
    public bool IsOpen;
    public bool IsSelect;

    public ClothesData(bool isOpen, bool isSelect)
    {
        this.IsOpen = isOpen;
        IsSelect = isSelect;
    }
}
