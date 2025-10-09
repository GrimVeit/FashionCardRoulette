using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEffectCombination : MonoBehaviour
{
    [SerializeField] private List<UIEffect> uiEffects = new List<UIEffect>();
    [SerializeField] private float timeDelay;

    private IEnumerator scaleEffectCombimation_Coroutine;

    public void Initialize()
    {
        uiEffects.ForEach(data => data.Initialize());
    }

    public void Dispose()
    {
        uiEffects.ForEach(data => data.Dispose());
    }

    public void ActivateEffect()
    {
        uiEffects.ForEach(data => data.ResetEffect());

        if (scaleEffectCombimation_Coroutine != null)
            Coroutines.Stop(scaleEffectCombimation_Coroutine);

        scaleEffectCombimation_Coroutine = ActivateScaleEffect_Coroutine();
        Coroutines.Start(scaleEffectCombimation_Coroutine);
    }

    public void DeactivateEffect()
    {
        if (scaleEffectCombimation_Coroutine != null)
            Coroutines.Stop(scaleEffectCombimation_Coroutine);

        scaleEffectCombimation_Coroutine = DeactivateScaleEffect_Coroutine();
        Coroutines.Start(scaleEffectCombimation_Coroutine);
    }

    private IEnumerator ActivateScaleEffect_Coroutine()
    {
        for (int i = 0; i < uiEffects.Count; i++)
        {
            uiEffects[i].ActivateEffect();

            yield return new WaitForSeconds(timeDelay);
        }
    }

    private IEnumerator DeactivateScaleEffect_Coroutine()
    {
        for (int i = uiEffects.Count - 1; i >= 0; i--)
        {
            uiEffects[i].DeactivateEffect();

            yield return new WaitForSeconds(timeDelay);
        }
    }
}
