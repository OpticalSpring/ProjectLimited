using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private void Start()
    {
    }

    private void Update()
    {

    }

    public void SoundPlay(int i, int j)
    {
        gameObject.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().Play();
    }

    public void SoundPlay3D(int i, int j, Vector3 vec)
    {
        GameObject temp = Instantiate(gameObject.transform.GetChild(i).GetChild(j).gameObject);
        temp.transform.position = vec;
        temp.GetComponent<AudioSource>().Play();
        Destroy(temp, 10);
    }
    

    public void SoundStop(int i, int j)
    {
        StartCoroutine(I1(i, j));
    }
    
    public void RandomPlay(int i, int x, int y)
    {
        int j = Random.Range(x, y + 1);
        SoundPlay(i, j);
    }

    public void RandomPlayNew(int i, int x, int y)
    {
        int j = Random.Range(x, y + 1);
        SoundPlay3D(i, j,Vector3.zero);
    }


    IEnumerator I1(int i, int j)
    {
        for (int k = 100; k > 0; --k)
        {
            if (gameObject.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().volume > (float)k / 100)
                gameObject.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().volume = (float)k / 100;

            yield return new WaitForSeconds(0.01f);
        }
    }
}
