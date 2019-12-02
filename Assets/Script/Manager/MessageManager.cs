using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public GameObject messageObject;
    public Text messageText;
    float delayTime;
    // Update is called once per frame
    void Update()
    {
        if(delayTime > 0)
        {
            delayTime -= Time.fixedDeltaTime;
        }
        else
        {
            delayTime = 0;
            messageObject.SetActive(false);
        }
    }

    public void TextSetUp(string input)
    {
        messageObject.SetActive(true);
        delayTime = 3;
        StartCoroutine(TextSlowSet(input));
    }

    IEnumerator TextSlowSet(string input)
    {
        messageText.text = "";
        for (int i = 0; i < input.Length; i++)
        {
            messageText.text += input[i];
            yield return new WaitForSecondsRealtime(0.02f);
        }
    }
}
