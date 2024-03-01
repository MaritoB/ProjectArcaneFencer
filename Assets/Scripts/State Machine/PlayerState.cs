using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State
{
    protected PlayerController player;
    public PlayerState(StateMachine aStateMachine, PlayerController aPlayerController) : base(aStateMachine)
    {
        this.player = aPlayerController; 
    }

}
