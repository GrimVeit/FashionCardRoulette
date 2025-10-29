using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouletteSpinCountView : View
{
    [SerializeField] private TextMeshProUGUI textSpinCounts;
    [SerializeField] private Button buttonSpin;
    [SerializeField] private UIEffect effect_ButtonSpin;

    public void Initialize()
    {
        effect_ButtonSpin.Initialize();
        effect_ButtonSpin.ActivateEffect();
    }

    public void Dispose()
    {
        effect_ButtonSpin.Dispose();
    }

    public void CloseSpin()
    {
        buttonSpin.enabled = false;

        effect_ButtonSpin.DeactivateEffect();
    }

    public void SetCount(int count)
    {
        textSpinCounts.text = $"Spins: {count}";
    }
}
