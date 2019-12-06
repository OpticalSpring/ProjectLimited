using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayTitle());
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(0, Time.fixedDeltaTime * 150, 0);
    }

    IEnumerator DelayTitle()
    {
        yield return new WaitForSecondsRealtime(20);
        SceneManager.LoadSceneAsync(0);
    }
}
