using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StoreClothesModel
{
    public event Action<Clothes> OnChooseOpenClothes;
    public event Action<Clothes> OnChooseCloseClothes;
    public event Action<ClothesType> OnChangeChooseClothes;
    public event Action OnEndChangeChooseClothes;

    public event Action<Clothes> OnSelectClothes;
    public event Action<Clothes> OnDeselectClothes;



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
                    if(j == 0)
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
        for (int i = 0; i < clothesGroupDatas.Count; i++)
        {
            for (int j = 0; j < clothesGroupDatas[i].Datas.Length; j++)
            {
                _clothesAllGroup.Groups[i].Clothes[j].SetData(clothesGroupDatas[i].Datas[j]);

                if (clothesGroupDatas[i].Datas[j].IsSelect)
                {
                    OnSelectClothes?.Invoke(_clothesAllGroup.Groups[i].Clothes[j]);
                }
            }
        }
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(new AllClothesDatas(clothesGroupDatas.ToArray()));
        File.WriteAllText(FilePath, json);
    }

    public void ChooseByClothesTypeForShop(ClothesType clothesType)
    {
        OnChangeChooseClothes?.Invoke(clothesType);

        _currentClothes = _clothesAllGroup.Groups.FirstOrDefault(data => data.ClothesType == clothesType).Clothes.ToList();

        _currentClothes.ForEach(data =>
        {
            if (data.Data.IsOpen)
            {
                OnChooseOpenClothes?.Invoke(data);
            }
            else
            {
                OnChooseCloseClothes?.Invoke(data);
            }
        });

        OnEndChangeChooseClothes?.Invoke();
    }

    public void ChooseByClothesTypeForWardrobe(ClothesType clothesType)
    {
        OnChangeChooseClothes?.Invoke(clothesType);

        _currentClothes = _clothesAllGroup.Groups.FirstOrDefault(data => data.ClothesType == clothesType).Clothes.ToList();

        _currentClothes.ForEach(data =>
        {
            if (data.Data.IsOpen)
            {
                if (data.Data.IsSelect)
                {
                    OnSelectClothes?.Invoke(data);
                }
                else
                {
                    OnDeselectClothes?.Invoke(data);
                }
            }
        });

        OnEndChangeChooseClothes?.Invoke();
    }

    public void OpenClothes(int id)
    {
        var clothes = _currentClothes.FirstOrDefault(data => data.Id == id);

        if(clothes == null)
        {
            Debug.LogError("Not found clothes for open with id - " + id);
            return;
        }

        clothes.Data.IsOpen = true;
        OnChooseOpenClothes?.Invoke(clothes);
    }

    public void SelectClothes(int id)
    {
        _currentClothes.ForEach(data =>
        {
            if (data.Data.IsSelect)
            {
                data.Data.IsSelect = false;
                OnDeselectClothes?.Invoke(data);
            }
        });

        var clothes = _currentClothes.FirstOrDefault(data => data.Id == id);

        if (clothes == null)
        {
            Debug.LogError("Not found clothes for select with id - " + id);
            return;
        }

        clothes.Data.IsSelect = true;
        OnSelectClothes?.Invoke(clothes);
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
    //    _currentClothes.ForEach(data =>
    //    {
    //        if (data.Data.IsSelect)
    //        {
    //            data.Data.IsSelect = false;
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
