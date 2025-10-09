using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIEffect : MonoBehaviour
{
    public virtual void Initialize() { }
    public virtual void Dispose() { }
    public virtual void ActivateEffect() { }
    public virtual void DeactivateEffect() { }
    public virtual void ResetEffect() { }
}
