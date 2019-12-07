using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundPlay(3, 3);
        StartCoroutine(DelayTitle());
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(0, Time.fixedDeltaTime * 150, 0);
    }

    IEnumerator DelayTitle()
    {
        yield return new WaitForSecondsRealtime(18);
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundStop(3, 3);
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadSceneAsync(0);
    }
}
