using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
   
    public int eventNumber;

    public GameObject mobGroup;
    public GameObject wallGroup;
    public GameObject checkPos;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;                    
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadSceneAsync(1);
        }



        SetEvent();

    }

    void SetEvent()
    {
        switch (eventNumber)
        {
            case 0:
                StartCoroutine("Event_0");
                break;
            case 1:
                if(mobGroup.transform.GetChild(0).childCount == 0)
                {
                    StartCoroutine("Event_1");
                }
                break;
            case 2:
                if (mobGroup.transform.GetChild(1).childCount == 0)
                {
                    StartCoroutine("Event_2");
                }
                break;
            case 3:
                break;
            case 4:
                    StartCoroutine("Event_4");
                break;
            case 5:
                if (mobGroup.transform.GetChild(2).childCount == 0)
                {
                    StartCoroutine("Event_5");
                }
                break;
            case 6:
                if (mobGroup.transform.GetChild(3).childCount == 0)
                {
                    StartCoroutine("Event_6");
                }
                break;
            case 7:
                if (mobGroup.transform.GetChild(4).childCount == 0)
                {
                    StartCoroutine("Event_7");
                }
                break;
            case 8:
                break;
            case 9:
                StartCoroutine("Event_9");
                break;
            case 10:
                if (mobGroup.transform.GetChild(5).childCount == 0 && mobGroup.transform.GetChild(6).childCount == 0)
                {
                    eventNumber++;
                }
                break;
            case 11:
                eventNumber++;
                break;
            case 12:
                eventNumber++;
                break;
            case 13:
                StartCoroutine("Event_13");
                break;
            case 14:
                break;
            case 15:
                StartCoroutine("Event_15");
                break;
            case 16:
                break;
            case 17:
                break;
            case 18:
                break;
            case 19:
                break;

        }
    }

    IEnumerator Event_0()
    {
        eventNumber++;
        mobGroup.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0);
    }

    IEnumerator Event_1()
    {
        eventNumber++;
        mobGroup.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0);
    }

    IEnumerator Event_2()
    {
        eventNumber++;
        for (int i = 0; i < wallGroup.transform.GetChild(0).childCount; i++)
        {
            wallGroup.transform.GetChild(0).GetChild(i).GetChild(0).gameObject.GetComponent<DestroyMaterial>().on = false;
        }
        yield return new WaitForSecondsRealtime(1);
        wallGroup.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(0);
    }

    IEnumerator Event_4()
    {
        eventNumber++;
        mobGroup.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0);
    }

    IEnumerator Event_5()
    {
        eventNumber++;
        mobGroup.transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0);
    }

    IEnumerator Event_6()
    {
        eventNumber++;
        mobGroup.transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0);
    }

    IEnumerator Event_7()
    {
        eventNumber++;
        for (int i = 0; i < wallGroup.transform.GetChild(1).childCount; i++)
        {
            wallGroup.transform.GetChild(1).GetChild(i).GetChild(0).gameObject.GetComponent<DestroyMaterial>().on = false;
        }
        yield return new WaitForSecondsRealtime(1);
        wallGroup.transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(0);
    }

    IEnumerator Event_9()
    {
        eventNumber++;
        mobGroup.transform.GetChild(5).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(10);
        mobGroup.transform.GetChild(6).gameObject.SetActive(true);
    }
    

    IEnumerator Event_13()
    {
        eventNumber++;
        for (int i = 0; i < wallGroup.transform.GetChild(3).childCount; i++)
        {
            wallGroup.transform.GetChild(3).GetChild(i).GetChild(0).gameObject.GetComponent<DestroyMaterial>().on = false;
        }
        yield return new WaitForSecondsRealtime(1);
        wallGroup.transform.GetChild(3).gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(0);
        
    }

    IEnumerator Event_15()
    {
        eventNumber++;
        mobGroup.transform.GetChild(7).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0);

    }
}
