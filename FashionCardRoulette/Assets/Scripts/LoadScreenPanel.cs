using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreenPanel : MovePanel
{
    [SerializeField] private List<AnimationElement> animationElementIcons;
    [SerializeField] private UIEffectCombination effectCombination;
    [SerializeField] private float timeWait;

    private IEnumerator timer;

    private void Awake()
    {
        effectCombination.Initialize();

        animationElementIcons.ForEach(data => OnDeactivatePanel += data.Deactivate);

        Initialize();
    }

    private void OnDestroy()
    {
        if (timer != null) Coroutines.Stop(timer);

        effectCombination.Dispose();

        animationElementIcons.ForEach(data => OnDeactivatePanel -= data.Deactivate);

        Dispose();
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        if (timer != null) Coroutines.Stop(timer);

        effectCombination.ActivateEffect();

        timer = Timer();
        Coroutines.Start(timer);
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        effectCombination.DeactivateEffect();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeWait);

        animationElementIcons.ForEach(data => data.Activate(1));
    }
}
