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
        fadeOutPanel.color = new Color(0, 0, 0, 0);
        for (int i = 0; i < 100; i++)
        {
            fadeOutPanel.color = Vector4.Lerp(fadeOutPanel.color, new Color(0, 0, 0, 1.5f), Time.fixedDeltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        SceneManager.LoadSceneAsync(nextScene);
    }

    IEnumerator FadeIn()
    {
        fadeOutPanel.color = new Color(0, 0, 0, 1.5f);
        for (int i = 0; i < 100; i++)
        {
            fadeOutPanel.color = Vector4.Lerp(fadeOutPanel.color, new Color(0, 0, 0, 0), Time.fixedDeltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }
}
