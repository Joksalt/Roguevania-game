using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) 
        : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseAttackInput();

        Collider2D[] collisions = player.CheckHit();

        // Send event to all collisions
        foreach (Collider2D hit in collisions)
        {
            hit.transform.gameObject.SendMessage("OnPlayerHit", player, SendMessageOptions.DontRequireReceiver);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished) 
        {
            isAbilityDone = true;
        }
    }
}
