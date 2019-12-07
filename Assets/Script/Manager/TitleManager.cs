using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundPlay(3, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundStop(3, 0);
        GetComponent<FadeOutManager>().FadeOut(1);
    }

    public void GameEnd()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundStop(3, 0);
        Application.Quit();
    }
}
