using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : Enemy
{
    public float attackTime;
    float realAttackTime;
    Vector3 nPos;
    float attackTimer;
    public GameObject effect;
    public GameObject effectPosition;
    bool at;
    void OnEffect()
    {
        GameObject eff = Instantiate(effect);
        eff.transform.position = effectPosition.transform.position;
        eff.transform.rotation = effectPosition.transform.rotation;
        eff.transform.parent = effectPosition.transform;
    }
    


    // Update is called once per frame
    void Update()
    {
        DeadCheck();
        DelayOut();
        if(hitTime > 0)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.GetComponent<PlayerControl>().targetPos, player.GetComponent<PlayerState>().walkSpeed * Time.fixedDeltaTime);
            return;
        }
        DistanceCheck();
        if (realAttackTime > attackTime-2)
        {
            Turn(gameObject, nPos);
            if(at == false)
            {
                at = true;
                OnEffect();
                GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundPlay3D(4, 0, gameObject.transform.position);
            }
            realAttackTime -= Time.deltaTime;

        }
        else if(realAttackTime > 0)
        {
            at = false;
            Attack();
            realAttackTime -= Time.deltaTime;
        }
        else if (playerDistance > attackDistance)
        {
            Turn(gameObject, player.transform.position);
            Chase();
        }
        else if (realAttackTime <= 0)
        {
            realAttackTime = attackTime;
            nPos = player.transform.position;
            nPos.x = nPos.x + Random.Range(-1f, 1f);
            nPos.z = nPos.z + Random.Range(-1f, 1f);
        }
    }

    protected override void Attack()
    {
        ani.aniState = 2;
        gameObject.transform.Translate(Vector3.forward * movementSpeed*3 * Time.deltaTime);

        if (attackTimer < 0)
        {
            attackTimer = 0.1f;
            Collider[] colliderArray = Physics.OverlapBox(gameObject.transform.position, new Vector3(1.0f, 1.0f, 1.0f), gameObject.transform.rotation);
            for (int i = 0; i < colliderArray.Length; i++)
            {
                if (colliderArray[i].tag == "Player")
                {
                    colliderArray[i].gameObject.GetComponent<PlayerControl>().Hit();
                }
            }
        }
        else
        {
            attackTimer -= Time.fixedDeltaTime;
        }
    }


}
