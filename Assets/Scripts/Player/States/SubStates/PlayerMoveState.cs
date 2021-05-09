using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) 
        : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        base.player.AudioSource.clip = base.playerData.AudioOption("Move").Audio;
        base.player.AudioSource.Play();
    }

    public override void Exit()
    {
        base.Exit();
        base.player.AudioSource.Stop();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.CheckIfShouldFlip(xInput);

        player.SetVelocityX(playerData.movementVelocity * xInput);

        if (!isExitingState)
        {
            if (xInput == 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}