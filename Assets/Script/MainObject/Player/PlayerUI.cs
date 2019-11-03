using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    PlayerState playerState;
    public Text blinkStack;
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
    }

    void SetText()
    {
        blinkStack.text = ""+playerState.blinkStack;
        blinkDelay.text = "" + playerState.blinkDelay;
        reveralDelay.text = "" + playerState.reveralDelay;
        lapseDelay.text = "" + playerState.lapseDelay;
    }
}
