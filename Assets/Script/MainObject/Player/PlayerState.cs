using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
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

    private void Start()
    {
        blinkStack = blinkStackMax;
        blinkDelay = blinkDelayMax;
        reveralDelay = reveralDelayMax;
    }

    public enum PlayerFSM
    {
        Move,
        Blink,
        Reveral,
        Lapse
    }
    public PlayerFSM playerFSM;
}
