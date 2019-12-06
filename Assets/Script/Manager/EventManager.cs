using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
   
    public int eventNumber;
    public PlayerState playerState;
    public GameObject mobGroup;
    public GameObject wallGroup;
    MessageManager messageManager;
    public GameObject messageObject;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;                    
        Cursor.lockState = CursorLockMode.Locked;
        messageManager = messageObject.GetComponent<MessageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            GetComponent<FadeOutManager>().FadeOut(1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<FadeOutManager>().FadeOut(0);
        }



        SetEvent();


        if(playerState.HP.x <= 0)
        {
            GetComponent<FadeOutManager>().FadeOut(0);
        }
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
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("에버 첫 임무를 환영합니다.");
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("본대는 도심 방위만으로 벅차 당신의 임무가 중요합니다.");
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("먼저 간단히 전술교범을 설명해드릴게요.");
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("WASD를 통해 움직여보세요.");
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("Ctrl과 Shift를 통해 속도 조절이 가능합니다.");
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("마우스 좌, 우클릭을 번갈아가면서 연타를 통해 공격할 수 있습니다.");
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("Type.10 Rush 다수가 식별되었습니다.");
        yield return new WaitForSecondsRealtime(3);
        mobGroup.transform.GetChild(0).gameObject.SetActive(true);
        messageManager.TextSetUp("해당 타입은 돌진을 조심하세요.");
        yield return new WaitForSecondsRealtime(0);
    }

    IEnumerator Event_1()
    {
        eventNumber++;
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("잘 하셨습니다. 이번엔 특수 스킬들을 설명드리겠습니다.");
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("Space를 통해 TimeBlink를 사용할 수 있습니다.");
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("Q를 통해 TimeReveral을 사용할 수 있습니다.");
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("E를 통해 TimeLapse을 사용할 수 있습니다.");
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("Type.20 Bullpub 다수가 식별되었습니다.");
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("해당 타입은 원거리 투사체를 조심하세요.");
        mobGroup.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0);
    }

    IEnumerator Event_2()
    {
        eventNumber++;
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("이제 다음 구역으로 이동합니다.");
        for (int i = 0; i < wallGroup.transform.GetChild(0).childCount; i++)
        {
            wallGroup.transform.GetChild(0).GetChild(i).GetChild(0).gameObject.GetComponent<DestroyMaterial>().on = false;
        }
        yield return new WaitForSecondsRealtime(1);
        wallGroup.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("도시 외곽을 정리합니다.");
        yield return new WaitForSecondsRealtime(0);
    }

    IEnumerator Event_4()
    {
        eventNumber++;
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("습격입니다.");
        mobGroup.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0);
    }

    IEnumerator Event_5()
    {
        eventNumber++;
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("전방 적 식별");
        mobGroup.transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0);
    }

    IEnumerator Event_6()
    {
        eventNumber++;
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("조심하세요.");
        mobGroup.transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0);
    }

    IEnumerator Event_7()
    {
        eventNumber++;
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("외곽도로쪽으로 TimeBlink로 건너가십시오.");
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
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("근방 다수의 적 식별");
        mobGroup.transform.GetChild(5).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("적 본대 곧 도착합니다.");
        yield return new WaitForSecondsRealtime(6);
        mobGroup.transform.GetChild(6).gameObject.SetActive(true);
    }
    

    IEnumerator Event_13()
    {
        eventNumber++;
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("대로변 게이트가 열렸습니다. 도심방향으로 전진하세요.");
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
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("Type.Zero 零式 SteelRain입니다.");
        mobGroup.transform.GetChild(7).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("위협레벨 최상위등급입니다.");
        yield return new WaitForSecondsRealtime(3);
        messageManager.TextSetUp("도심 방위를 위해 꼭 섬멸하십시오.");
        yield return new WaitForSecondsRealtime(0);

    }
}
