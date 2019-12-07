using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public Vector3[] oldPos;

    float horizontalValue;
    float VerticalValue;
    int blinkRotation;
    int blinkInputCount;
    float blinkInputDelay;
    public int nowPosCount;
    GameObject rotatePos;
    GameObject playerModel;
    GameObject rayObj;
    GameObject cam;
    public Vector3 targetPos;
    PlayerState playerState;
    PlayerAni ani;
    GlitchControl glitch;


    public GameObject blinkEffect1;
    public GameObject blinkEffect2;
    public GameObject reveralEffect1;
    public GameObject reveralEffect2;
    public GameObject attackEffect;
    public GameObject attackEffectPosition;

    public Material rimMat;
    
    // Start is called before the first frame update
    void Start()
    {
        rotatePos = GameObject.Find("RotatePos");
        playerModel = gameObject.transform.GetChild(0).gameObject;
        rayObj = gameObject.transform.GetChild(1).gameObject;
        playerState = GetComponent<PlayerState>();
        ani = GetComponent<PlayerAni>();
        cam = GameObject.Find("ThirdCamera");
        glitch = cam.transform.GetChild(0).GetChild(0).gameObject.GetComponent<GlitchControl>();
        StartCoroutine("PositionSave");

    }

    // Update is called once per frame
    void Update()
    {
        if (playerState.HP.x <= 0)
        {
            ani.aniState = 10;
            return;
        }
        DelayCheck();
        InputCheck();
    }

    void DelayCheck()
    {

        if (playerState.blinkStack < playerState.blinkStackMax)
        {
            playerState.blinkDelay -= Time.fixedDeltaTime;
            if (playerState.blinkDelay < 0)
            {
                playerState.blinkStack++;
                playerState.blinkDelay = playerState.blinkDelayMax;
                GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundPlay(2, 4);
            }
        }
        else
        {

        }

        if (playerState.reveralDelay > 0)
        {
            playerState.reveralDelay -= Time.fixedDeltaTime;
            if (playerState.reveralDelay < 0)
            {
                playerState.reveralDelay = 0;
                GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundPlay(2, 4);
            }
        }
        if (playerState.lapseDelay > 0)
        {
            playerState.lapseDelay -= Time.fixedDeltaTime;
            if (playerState.lapseDelay < 0)
            {
                playerState.lapseDelay = 0;
                GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundPlay(2, 4);
            }
        }
        if (playerState.attackDelay > 0)
        {
            playerState.attackDelay -= Time.fixedDeltaTime;
            if (playerState.attackDelay < 0)
            {
                playerState.attackDelay = 0;
                AttackBack();
            }
        }
        if (playerState.attackNoInput > 0)
        {
            playerState.attackNoInput -= Time.fixedDeltaTime;
            if (playerState.attackNoInput < 0)
            {
                playerState.attackNoInput = 0;
            }
        }
        if (playerState.autoMoveing > 0)
        {
            playerState.autoMoveing -= Time.fixedDeltaTime;
            AutoMove();
            if (playerState.autoMoveing < 0)
            {
                playerState.autoMoveing = 0;
            }
        }

        if (playerState.hitTime > 0)
        {
            playerState.hitTime -= Time.fixedDeltaTime;
        }

    }

    void InputCheck()
    {
        if (playerState.hitTime > 0.5f)
        {
            return;
        }
        ani.movement = 0;
        horizontalValue = Input.GetAxisRaw("Horizontal");
        VerticalValue = Input.GetAxisRaw("Vertical");

        if (playerState.reveralDelay <= 0 && Input.GetKeyDown(KeyCode.Q) && playerState.playerFSM == PlayerState.PlayerFSM.Move)
        {
            StartCoroutine("Reveral");
        }
        if (playerState.lapseDelay <= 0 && Input.GetKeyDown(KeyCode.E) && playerState.playerFSM == PlayerState.PlayerFSM.Move)
        {
            StartCoroutine("Lapse");
        }

        AttackCheck();
        BlinkCheck();

        if (playerState.playerFSM != PlayerState.PlayerFSM.Reveral)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                AttackBack();
            }

            if (horizontalValue != 0 || VerticalValue != 0)
            {
                if (playerState.attackState == 0)
                {
                    TurnPointCheck();
                    Move();
                }
            }
            Turn(playerModel, targetPos);
        }
    }

    IEnumerator PositionSave()
    {
        for (int i = 0; i < 30; i++)
        {
            oldPos[i] = gameObject.transform.position;
        }
        while (true)
        {
            if (playerState.playerFSM != PlayerState.PlayerFSM.Reveral)
            {
                oldPos[nowPosCount] = gameObject.transform.position;
                nowPosCount++;

                if (nowPosCount >= 30)
                {
                    nowPosCount = 0;
                }
            }
            yield return new WaitForSecondsRealtime(0.15f);
        }
    }

    IEnumerator Reveral()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundPlay(2, 1);
        GameObject effect = Instantiate(reveralEffect1);
        effect.transform.position = gameObject.transform.position;
        effect.transform.rotation = gameObject.transform.rotation;

        GameObject effect2 = Instantiate(reveralEffect2);
        effect2.transform.position = gameObject.transform.position;
        effect2.transform.rotation = gameObject.transform.rotation;
        effect2.transform.parent = gameObject.transform;

        playerState.playerFSM = PlayerState.PlayerFSM.Reveral;
        playerState.reveralDelay = playerState.reveralDelayMax;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1f);
        ani.aniState = 3;
        if (playerState.HP.x < playerState.HP.y)
        {
            playerState.HP.x++;
        }
        cam.GetComponent<CameraControl>().followSpeed = 10;
        int a = nowPosCount;
        for (int i = 0; i < 29; i++)
        {
            a--;
            if (a == -1)
            {
                a = 29;
            }
            for (int j = 0; j < 2; j++)
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, oldPos[a], 5 * Time.fixedDeltaTime);
                yield return new WaitForSecondsRealtime(0.01f);
            }

        }
        playerState.playerFSM = PlayerState.PlayerFSM.Move;
        cam.GetComponent<CameraControl>().followSpeed = 4;
        ani.aniState = 0;
        Time.timeScale = 1;
        Destroy(effect, 1);
        Destroy(effect2, 1);
    }

    IEnumerator Lapse()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundPlay(2, 2);
        playerState.lapseDelay = playerState.lapseDelayMax;
        playerState.playerFSM = PlayerState.PlayerFSM.Lapse;
        Time.timeScale = 0.2f;
        GameObject[] rimObj = new GameObject[10];
        for (int i = 0; i < 10; i++)
        {
            rimObj[i] = Instantiate(gameObject.transform.GetChild(0).gameObject);
            rimObj[i].transform.position = gameObject.transform.GetChild(0).position;
            rimObj[i].transform.rotation = gameObject.transform.GetChild(0).rotation;
            rimObj[i].transform.GetChild(2).gameObject.GetComponent<SkinnedMeshRenderer>().material = rimMat;
            rimObj[i].transform.GetChild(3).gameObject.GetComponent<SkinnedMeshRenderer>().material = rimMat;
            ani.SetCopyAni(rimObj[i].GetComponent<Animator>());
            yield return new WaitForSecondsRealtime(0.5f);
        }
        Time.timeScale = 1;
        playerState.playerFSM = PlayerState.PlayerFSM.Move;
        Vector4 col = new Vector4(1, 1, 1, 1);
        for (int j = 0; j < 20; j++)
        {
            for (int i = 0; i < 10; i++)
            {
                col = Vector4.Lerp(col, Vector4.zero, Time.fixedDeltaTime);
                rimObj[i].transform.GetChild(2).gameObject.GetComponent<SkinnedMeshRenderer>().material.color = col;
                rimObj[i].transform.GetChild(3).gameObject.GetComponent<SkinnedMeshRenderer>().material.color = col;
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }
        for (int i = 0; i < 10; i++)
        {
            Destroy(rimObj[i]);
        }
    }

    void AttackCheck()
    {
        if (playerState.playerFSM != PlayerState.PlayerFSM.Reveral && playerState.attackNoInput <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (playerState.attackState == 1)
                {
                    playerState.attackNoInput = 1;
                }
                else
                {
                    LeftAttack();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (playerState.attackState == 2)
                {
                    playerState.attackNoInput = 1;
                }
                else
                {
                    RightAttack();
                }
            }
        }
    }
    void LeftAttack()
    {
        Attack();
        ani.attackState = 1;
        playerState.attackState = 1;
    }

    void RightAttack()
    {
        Attack();
        ani.attackState = 2;
        playerState.attackState = 2;
    }

    void OnEffect()
    {
        GameObject eff = Instantiate(attackEffect);
        eff.transform.position = attackEffectPosition.transform.position;
        eff.transform.rotation = attackEffectPosition.transform.rotation;
    }

    void Attack()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().RandomPlayNew(1, 0, 5);
        OnEffect();
        ani.aniState = 1;
        playerState.attackDelay = playerState.attackDelayMax;
        playerState.autoMoveing = 0.3f;
        Collider[] colliderArray = Physics.OverlapBox(playerState.attackPoint.transform.position, new Vector3(1.5f, 1.5f, 1.5f), playerState.attackPoint.transform.rotation);
        for (int i = 0; i < colliderArray.Length; i++)
        {
            if (colliderArray[i].tag == "Enemy")
            {
                if (colliderArray[i].gameObject.GetComponent<EnemyType3>())
                {
                    playerState.bossTarget = colliderArray[i].gameObject;
                }
                else
                {
                    playerState.attackTarget = colliderArray[i].gameObject;
                }

                colliderArray[i].gameObject.GetComponent<Enemy>().Hit();
            }
        }
    }

    void AutoMove()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, playerState.walkSpeed * Time.fixedDeltaTime);
    }

    void AttackBack()
    {
        playerState.attackDelay = 0;
        playerState.attackState = 0;
        playerState.autoMoveing = 0;
        ani.aniState = 0;
        ani.attackState = 0;
    }

    void BlinkCheck()
    {
        if (playerState.playerFSM != PlayerState.PlayerFSM.Reveral && playerState.blinkStack > 0)
        {
            if (horizontalValue != 0 || VerticalValue != 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Blink();
                }
            }


            if (Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D))
            {
                if (blinkRotation == 0 && blinkInputCount == 1)
                {
                    blinkInputCount = 0;
                    Blink();
                }
                else
                {
                    blinkInputCount = 1;
                    blinkInputDelay = 0.3f;
                }
                blinkRotation = 0;
            }
            if (Input.GetKeyDown(KeyCode.D) && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.W))
            {
                if (blinkRotation == 1 && blinkInputCount == 1)
                {
                    blinkInputCount = 0;
                    Blink();
                }
                else
                {
                    blinkInputCount = 1;
                    blinkInputDelay = 0.3f;
                }
                blinkRotation = 1;
            }
            if (Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.D))
            {
                if (blinkRotation == 2 && blinkInputCount == 1)
                {
                    blinkInputCount = 0;
                    Blink();
                }
                else
                {
                    blinkInputCount = 1;
                    blinkInputDelay = 0.3f;
                }
                blinkRotation = 2;
            }
            if (Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D))
            {
                if (blinkRotation == 3 && blinkInputCount == 1)
                {
                    blinkInputCount = 0;
                    Blink();
                }
                else
                {
                    blinkInputCount = 1;
                    blinkInputDelay = 0.3f;
                }
                blinkRotation = 3;
            }

            if (blinkInputDelay > 0)
            {
                blinkInputDelay -= Time.fixedDeltaTime;
            }
            else
            {
                blinkInputCount = 0;
            }
        }
    }



    void Blink()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundPlay(2, 3);
        GameObject effect = Instantiate(blinkEffect1);
        effect.transform.position = gameObject.transform.position;
        effect.transform.rotation = gameObject.transform.rotation;
        ani.aniState = 2;
        playerState.blinkStack--;
        RaycastHit rayHit;

        int mask = 1 << 9 | 1 << 2;
        mask = ~mask;
        rayObj.transform.LookAt(rotatePos.transform.position);
        Vector3 rayObjRot = rayObj.transform.eulerAngles;
        rayObjRot.x = 0;
        rayObjRot.z = 0;
        rayObj.transform.eulerAngles = rayObjRot;
        if (Physics.Raycast(gameObject.transform.position + new Vector3(0, 1f, 0), rayObj.transform.forward, out rayHit, playerState.blinkDistance, mask))
        {
            gameObject.transform.position = rayHit.point;
            gameObject.transform.position += new Vector3(0, 1f, 0);
        }
        else
        {
            rotatePos.transform.localPosition = new Vector3(horizontalValue * playerState.blinkDistance, 0, VerticalValue * playerState.blinkDistance);


            gameObject.transform.position = rotatePos.transform.position;
            gameObject.transform.position += new Vector3(0, 1f, 0);
        }
        Destroy(effect, 2);
        StartCoroutine("BlinkRollBackDelay");
    }

    IEnumerator BlinkRollBackDelay()
    {
        yield return new WaitForSeconds(0.01f);
        GameObject effect = Instantiate(blinkEffect2);
        effect.transform.position = gameObject.transform.position;
        effect.transform.rotation = gameObject.transform.rotation;
        bool rollBackCheck = false;
        Vector3 bPos = gameObject.transform.position;
        for (int i = 0; i < 100; i++)
        {
            if (Vector3.Distance(gameObject.transform.position, bPos) > 1f)
            {
                if (rollBackCheck == false)
                {
                    rollBackCheck = true;
                    ani.aniState = 0;
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
        if (ani.aniState == 2 && rollBackCheck == false)
        {
            ani.aniState = 0;
            rollBackCheck = true;
        }
        Destroy(effect, 2);
    }


    void Move()
    {
        if (ani.attackState == 0)
        {



            if (Input.GetKey(KeyCode.LeftShift))
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, playerState.fastRunSpeed * Time.fixedDeltaTime);
                ani.movement = 3;

            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, playerState.walkSpeed * Time.fixedDeltaTime);
                ani.movement = 1;

            }
            else
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, playerState.slowRunSpeed * Time.fixedDeltaTime);
                ani.movement = 2;

            }
        }

        RaycastHit rayHit;

        int mask = 1 << 9 | 1 << 2;
        mask = ~mask;
        if (Physics.Raycast(gameObject.transform.position + new Vector3(0, 1, 0), Vector3.down, out rayHit, 50, mask))
        {
            Vector3 hitPoint = rayHit.point + new Vector3(0, 0.01f, 0);
            gameObject.transform.position = hitPoint;
        }
    }

    void TurnPointCheck()
    {
        rotatePos.transform.localPosition = new Vector3(horizontalValue * 1000, 0, VerticalValue * 1000);
        targetPos = rotatePos.transform.position;
    }

    void Turn(GameObject obj, Vector3 target)
    {
        float dz = target.z - obj.transform.position.z;
        float dx = target.x - obj.transform.position.x;

        float rotateDegree = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;

        obj.transform.rotation = Quaternion.RotateTowards(obj.transform.rotation, Quaternion.Euler(0, rotateDegree, 0), playerState.rotateSpeed * Time.fixedDeltaTime);

    }

    public void Hit()
    {
      GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundPlay(2, 0);
        if (playerState.playerFSM == PlayerState.PlayerFSM.Reveral)
        {
            return;
        }

        if (playerState.hitTime > 0)
        {
            return;
        }
        glitch.zero = true;
        StartCoroutine("GlitchRollback");
        playerState.hitTime = 1.0f;
        playerState.HP.x--;
        ani.aniState = 4;
    }

    IEnumerator GlitchRollback()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        glitch.zero = false;
        ani.aniState = 0;
    }
}
