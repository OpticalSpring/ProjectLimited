using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOutManager : MonoBehaviour
{
    public Image fadeOutPanel;
    bool on;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeOut(int nextScene)
    {
        if (on == false)
        {
            on = true;
            StartCoroutine(FadeOutSceneChange(nextScene));
        }
    }

    IEnumerator FadeOutSceneChange(int nextScene)
    {
        for (int i = 0; i < 256; i++)
        {
            fadeOutPanel.color = new Color(0, 0, 0,i / 255f);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        SceneManager.LoadSceneAsync(nextScene);
    }

    IEnumerator FadeIn()
    {
        for (int i = 0; i < 256; i++)
        {
            fadeOutPanel.color = new Color(0, 0, 0, (255 - i) / 255f);
           yield return new WaitForSecondsRealtime(0.01f);
        }
    }
}
