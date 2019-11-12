﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject player;
    PlayerState playerState;
    public Text blinkStack;
    public Image[] blinkStackImage;
    public Text blinkDelay;
    public Text reveralDelay;
    public Text lapseDelay;
    public GameObject[] reveralOutline;
    public GameObject[] lapseOutline;
    public GameObject smartWatchMask;
    public Vector3 maskTargetPos;
    // Start is called before the first frame update
    void Start()
    {
        playerState = player.GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        SetText();
        SetBlink();
        SmartWatchMaskSet();
    }

    void SmartWatchMaskSet()
    {
        switch (playerState.playerFSM)
        {
            case PlayerState.PlayerFSM.Move:
                maskTargetPos.x = -150;
                break;
            case PlayerState.PlayerFSM.Reveral:
                maskTargetPos.x = 0;
                break;
            case PlayerState.PlayerFSM.Lapse:
                maskTargetPos.x = 150;
                break;

        }
        smartWatchMask.transform.localPosition = Vector3.Lerp(smartWatchMask.transform.localPosition, maskTargetPos, 10 * Time.fixedDeltaTime);
    }

    void SetText()
    {
        blinkStack.text = ""+playerState.blinkStack;
        blinkDelay.text = "" + playerState.blinkDelay;
        reveralDelay.text = "" + (int)playerState.reveralDelay;
        lapseDelay.text = "" + (int)playerState.lapseDelay;

        if(playerState.reveralDelay <= 0)
        {
            reveralOutline[0].SetActive(false);
            reveralOutline[1].SetActive(true);
            reveralOutline[2].SetActive(true);
        }
        else
        {
            reveralOutline[0].SetActive(true);
            reveralOutline[1].SetActive(false);
            reveralOutline[2].SetActive(false);
        }
        if(playerState.lapseDelay <= 0)
        {
            lapseOutline[0].SetActive(false);
            lapseOutline[1].SetActive(true);
            lapseOutline[2].SetActive(true);
        }
        else
        {
            lapseOutline[0].SetActive(true);
            lapseOutline[1].SetActive(false);
            lapseOutline[2].SetActive(false);
        }
    }

    void SetBlink()
    {
        switch (playerState.blinkStack)
        {
            case 0:
                blinkStackImage[0].fillAmount = (playerState.blinkDelayMax - playerState.blinkDelay) / playerState.blinkDelayMax;
                blinkStackImage[1].fillAmount = 0;
                blinkStackImage[2].fillAmount = 0;
                break;
            case 1:
                blinkStackImage[0].fillAmount = 1;
                blinkStackImage[1].fillAmount = (playerState.blinkDelayMax - playerState.blinkDelay) / playerState.blinkDelayMax;
                blinkStackImage[2].fillAmount = 0;
                break;
            case 2:
                blinkStackImage[0].fillAmount = 1;
                blinkStackImage[1].fillAmount = 1;
                blinkStackImage[2].fillAmount = (playerState.blinkDelayMax - playerState.blinkDelay) / playerState.blinkDelayMax;
                break;
            case 3:
                blinkStackImage[0].fillAmount = 1;
                blinkStackImage[1].fillAmount = 1;
                blinkStackImage[2].fillAmount = 1;
                break;
        }
    }
}
