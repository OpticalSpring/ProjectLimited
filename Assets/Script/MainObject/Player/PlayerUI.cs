using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerUI : MonoBehaviour
{
     GameObject player;
    PlayerState playerState;
    public GameObject blinkOutlineEffect;
    public Image[] blinkStackImage;
    public Text reveralDelay;
    public Text lapseDelay;
    public Text smartWatchTimeText;
    public Text smartWatchSpeedText;
    public Image HP;
    public GameObject[] reveralOutline;
    public GameObject[] lapseOutline;
    public GameObject smartWatchMask;
    public GameObject smartWatchObject;
    public GameObject enemyUI;
    public Image enemyHP;
    public Text enemyNameText;
    public GameObject bossUI;
    public Image bossHP;
    public Text bossNameText;
    public Vector3 maskTargetPos;
    public Vector3 smartWatchScale;
    public float HPValue;
    public float enemyHPValue;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerState = player.GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        SetText();
        SetBlink();
        SetHP();
        SetSmartWatch();
        SetEnemyHP();
    }

    void SetEnemyHP()
    {
        if (playerState.attackTarget != null)
        {
            enemyUI.SetActive(true);
            enemyNameText.text = playerState.attackTarget.name;
            enemyHPValue = Mathf.Lerp(enemyHPValue, playerState.attackTarget.GetComponent<Enemy>().HP.x, Time.fixedDeltaTime * 10);
            enemyHP.fillAmount = enemyHPValue / playerState.attackTarget.GetComponent<Enemy>().HP.y;
        }
        else
        {
            enemyUI.SetActive(false);
        }
    }

    void SetHP()
    {
        HPValue = Mathf.Lerp(HPValue, playerState.HP.x, Time.fixedDeltaTime * 2);
        HP.fillAmount = HPValue/ playerState.HP.y;
    }

    void SetSmartWatch()
    {
        switch (playerState.playerFSM)
        {
            case PlayerState.PlayerFSM.Move:
                maskTargetPos.x = -150;
                smartWatchScale = new Vector3(1, 1, 1);
                smartWatchSpeedText.text = "<size=30>X</size><size=15> </size><color=#ff006a>1</color>";
                break;
            case PlayerState.PlayerFSM.Reveral:
                maskTargetPos.x = 0;
                smartWatchScale = new Vector3(1.3f, 1.3f, 1.3f);
                smartWatchSpeedText.text = "<size=30>X</size><size=15> </size><color=#ff006a>-5</color>";
                break;
            case PlayerState.PlayerFSM.Lapse:
                maskTargetPos.x = 150;
                smartWatchScale = new Vector3(1.3f, 1.3f, 1.3f);
                smartWatchSpeedText.text = "<size=30>X</size><size=15> </size><color=#ff006a>0.2</color>";
                break;

        }
        smartWatchMask.transform.localPosition = Vector3.Lerp(smartWatchMask.transform.localPosition, maskTargetPos, 10 * Time.fixedDeltaTime);
        smartWatchObject.transform.localScale = Vector3.Lerp(smartWatchObject.transform.localScale, smartWatchScale, 10 * Time.fixedDeltaTime);
        string str = String.Format("{0:d2}:{1:d2}:{2:d2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        smartWatchTimeText.text = str;

    }

    void SetText()
    {
        reveralDelay.text = "" + (int)playerState.reveralDelay;
        lapseDelay.text = "" + (int)playerState.lapseDelay;

        if(playerState.reveralDelay <= 0)
        {
            reveralOutline[0].SetActive(false);
            reveralOutline[1].SetActive(true);
            reveralOutline[2].SetActive(true);
        }
        else
        {
            reveralOutline[0].SetActive(true);
            reveralOutline[1].SetActive(false);
            reveralOutline[2].SetActive(false);
        }
        if(playerState.lapseDelay <= 0)
        {
            lapseOutline[0].SetActive(false);
            lapseOutline[1].SetActive(true);
            lapseOutline[2].SetActive(true);
        }
        else
        {
            lapseOutline[0].SetActive(true);
            lapseOutline[1].SetActive(false);
            lapseOutline[2].SetActive(false);
        }
    }

    void SetBlink()
    {
        switch (playerState.blinkStack)
        {
            case 0:
                blinkStackImage[0].fillAmount = (playerState.blinkDelayMax - playerState.blinkDelay) / playerState.blinkDelayMax;
                blinkStackImage[1].fillAmount = 0;
                blinkStackImage[2].fillAmount = 0;
                blinkOutlineEffect.SetActive(false);
                break;
            case 1:
                blinkStackImage[0].fillAmount = 1;
                blinkStackImage[1].fillAmount = (playerState.blinkDelayMax - playerState.blinkDelay) / playerState.blinkDelayMax;
                blinkStackImage[2].fillAmount = 0;
                blinkOutlineEffect.SetActive(true);
                break;
            case 2:
                blinkStackImage[0].fillAmount = 1;
                blinkStackImage[1].fillAmount = 1;
                blinkStackImage[2].fillAmount = (playerState.blinkDelayMax - playerState.blinkDelay) / playerState.blinkDelayMax;
                blinkOutlineEffect.SetActive(true);
                break;
            case 3:
                blinkStackImage[0].fillAmount = 1;
                blinkStackImage[1].fillAmount = 1;
                blinkStackImage[2].fillAmount = 1;
                blinkOutlineEffect.SetActive(true);
                break;
        }
    }
}
