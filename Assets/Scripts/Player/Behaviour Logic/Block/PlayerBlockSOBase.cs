using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockSOBase : ScriptableObject
{
    protected PlayerController player;
    protected GameObject gameObject;

    public virtual void Initialize(GameObject aGameObject, PlayerController aPlayer)
    {
        this.gameObject = aGameObject;
        this.player = aPlayer;
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic()
    {
    }
    public virtual void DoPhysicsLogic() { }
    public virtual void ResetValues() { }
}
