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
    public Text bossHPText;
    public Text bossNameText;
    public Vector3 maskTargetPos;
    public Vector3 smartWatchScale;
    public float HPValue;
    public float enemyHPValue;
    public float bossHPValue;
    Animator[] blinkStackAnimator = new Animator[3];
    Image[] skillEffectImage = new Image[3];
    float[] skillEffectValue = new float[3];
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerState = player.GetComponent<PlayerState>();
        blinkStackAnimator[0] = blinkStackImage[0].gameObject.GetComponent<Animator>();
        blinkStackAnimator[1] = blinkStackImage[1].gameObject.GetComponent<Animator>();
        blinkStackAnimator[2] = blinkStackImage[2].gameObject.GetComponent<Animator>();
        skillEffectImage[0] = blinkOutlineEffect.GetComponent<Image>();
        skillEffectImage[1] = reveralOutline[0].GetComponent<Image>();
        skillEffectImage[2] = lapseOutline[0].GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        SetText();
        SetBlink();
        SetHP();
        SetSmartWatch();
        SetEnemyHP();
        SetSkillEffectColor();
    }

    void SetSkillEffectColor()
    {
        for (int i = 0; i < 3; i++)
        {
            skillEffectImage[i].color = Vector4.Lerp(skillEffectImage[i].color, new Vector4(1, 1, 1, skillEffectValue[i]), Time.fixedDeltaTime * 2);
        }
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

        if (playerState.bossTarget != null)
        {
            int bossHPX = (int)playerState.bossTarget.GetComponent<Enemy>().HP.x;
            bossUI.SetActive(true);
            bossNameText.text = playerState.bossTarget.name;
            bossHPValue = Mathf.Lerp(bossHPValue, bossHPX % 1000 % 100 % 10, Time.fixedDeltaTime * 10);
            if (bossHPX % 1000 % 100 % 10 == 0)
            {
                bossHPValue = 10;
            }
            bossHP.fillAmount = bossHPValue / 10;
            bossHPText.text = "" + bossHPX / 10;
        }
        else
        {
            bossUI.SetActive(false);
        }
    }

    void SetHP()
    {
        HPValue = Mathf.Lerp(HPValue, playerState.HP.x, Time.fixedDeltaTime * 2);
        HP.fillAmount = HPValue / playerState.HP.y;
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

        if (playerState.reveralDelay <= 0)
        {
            skillEffectValue[1] = 1;
            reveralOutline[1].SetActive(false);
            reveralOutline[2].SetActive(true);
        }
        else
        {
            skillEffectValue[1] = 0;
            reveralOutline[1].SetActive(true);
            reveralOutline[2].SetActive(false);
        }
        if (playerState.lapseDelay <= 0)
        {
            skillEffectValue[2] = 1;
            lapseOutline[1].SetActive(false);
            lapseOutline[2].SetActive(true);
        }
        else
        {
            skillEffectValue[2] = 0;
            lapseOutline[1].SetActive(true);
            lapseOutline[2].SetActive(false);
        }
    }
    public int blinkStackLast = 0;
    void SetBlink()
    {
        switch (playerState.blinkStack)
        {
            case 0:
                blinkStackImage[0].fillAmount = (playerState.blinkDelayMax - playerState.blinkDelay) / playerState.blinkDelayMax;
                blinkStackImage[1].fillAmount = 0;
                blinkStackImage[2].fillAmount = 0;
                blinkStackAnimator[0].SetBool("Flash", false);
                blinkStackLast = 0;
                skillEffectValue[0] = 0;
                break;
            case 1:
                blinkStackImage[0].fillAmount = 1;
                blinkStackImage[1].fillAmount = (playerState.blinkDelayMax - playerState.blinkDelay) / playerState.blinkDelayMax;
                blinkStackImage[2].fillAmount = 0;
                if (blinkStackLast < 1)
                {
                    blinkStackAnimator[0].SetBool("Flash", true);
                    blinkStackAnimator[1].SetBool("Flash", false);
                }
                blinkStackLast = 1;
                skillEffectValue[0] = 1;
                break;
            case 2:
                blinkStackImage[0].fillAmount = 1;
                blinkStackImage[1].fillAmount = 1;
                blinkStackImage[2].fillAmount = (playerState.blinkDelayMax - playerState.blinkDelay) / playerState.blinkDelayMax;
                if (blinkStackLast < 2)
                {
                    blinkStackAnimator[0].SetBool("Flash", false);
                    blinkStackAnimator[1].SetBool("Flash", true);
                    blinkStackAnimator[2].SetBool("Flash", false);
                }
                blinkStackLast = 2;
                skillEffectValue[0] = 1;
                break;
            case 3:
                blinkStackImage[0].fillAmount = 1;
                blinkStackImage[1].fillAmount = 1;
                blinkStackImage[2].fillAmount = 1;
                if (blinkStackLast < 3)
                {
                    blinkStackAnimator[1].SetBool("Flash", false);
                    blinkStackAnimator[2].SetBool("Flash", true);
                }
                blinkStackLast = 3;
                skillEffectValue[0] = 1;
                break;
        }
    }
}
