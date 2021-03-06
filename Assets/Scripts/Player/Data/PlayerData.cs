using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AudioOptions
{
    public string AudioName;
    public AudioClip Audio;
}

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Dash State")]
    public float dashCooldown = 0.5f;
    public float maxHoldTime = 1f;
    public float holdTimeScale = 0.25f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30f;
    public float drag = 10f;
    public float dashEndYMultiplier = 0.2f;
    public float distBetweenAfterImages = 0.5f;

    [Header("Combat")]
    public float AttackDamage = 10.0f;
    public float AttackRange = 0.5f;
    public float RangeDamage = 3.0f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;

    [Header("Gameplay")]
    public int CurrentHealth = 99;
    public int MaxHealth = 100;
    public int Gold = 100;

    [Header("Audio")]
    public AudioOptions[] AudioOptions;

    public AudioOptions AudioOption(string name)
    {
        for(int i = 0; i < AudioOptions.Length; i++)
        {
            if (AudioOptions[i].AudioName == name)
            {
                return AudioOptions[i];
            }
        }

        return null;
    }
}