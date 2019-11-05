using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public Vector2 HP;

    public float walkSpeed;
    public float slowRunSpeed;
    public float fastRunSpeed;
    public float rotateSpeed;
    public float blinkDistance;

    [HideInInspector]
    public int blinkStack;
    public int blinkStackMax;
    [HideInInspector]
    public float blinkDelay;
    public float blinkDelayMax;
    [HideInInspector]
    public float reveralDelay;
    public float reveralDelayMax;
    [HideInInspector]
    public float lapseDelay;
    public float lapseDelayMax;
    [HideInInspector]
    public float attackDelay;
    public float attackDelayMax;
    public float attackNoInput;
    [HideInInspector]
    public float autoMoveing;

    public bool stiffness;
    [HideInInspector]
    public float stiffnessDelay;
    [HideInInspector]
    public int attackState;


    public enum PlayerFSM
    {
        Move,
        Reveral,
        Lapse
    }
    public PlayerFSM playerFSM;


    private void Start()
    {
        blinkStack = blinkStackMax;
        blinkDelay = blinkDelayMax;
        reveralDelay = 0;
        lapseDelay = 0;
        attackDelay = 0;
    }

}
