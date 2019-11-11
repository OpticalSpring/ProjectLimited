using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    PlayerState playerState;
    public Text blinkStack;
    public Image[] blinkStackImage;
    public Text blinkDelay;
    public Text reveralDelay;
    public Text lapseDelay;
    // Start is called before the first frame update
    void Start()
    {
        playerState = GameObject.Find("Player").GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        SetText();
        SetBlink();
    }

    void SetText()
    {
        blinkStack.text = ""+playerState.blinkStack;
        blinkDelay.text = "" + playerState.blinkDelay;
        reveralDelay.text = "" + playerState.reveralDelay;
        lapseDelay.text = "" + playerState.lapseDelay;
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
