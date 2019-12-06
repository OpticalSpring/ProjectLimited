using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType2 : Enemy
{
    public float attackTime;
    float realAttackTime;
    Vector3 nPos;
    float attackTimer;
    public GameObject attackPoint;
    bool attacked;
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
        if (hitTime > 0)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.GetComponent<PlayerControl>().targetPos, player.GetComponent<PlayerState>().walkSpeed * Time.fixedDeltaTime);
            return;
        }
        DistanceCheck();
        if (realAttackTime > attackTime - 2)
        {
            if (at == false)
            {
                at = true;
                OnEffect();
            }
            Turn(gameObject, nPos);
            realAttackTime -= Time.deltaTime;

        }
        else if (realAttackTime > 0)
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
            attacked = false;
            realAttackTime = attackTime;
            nPos = player.transform.position;
            nPos.x = nPos.x + Random.Range(-1f, 1f);
            nPos.z = nPos.z + Random.Range(-1f, 1f);
        }
    }

    protected override void Attack()
    {
        if (attacked == false) {
            StartCoroutine("DelayShot");
            attacked = true;
            ani.aniState = 3;
        }
    }

    IEnumerator DelayShot()
    {
        GameObject temp = GameObject.Find("ObjectPool").GetComponent<ObjectPoolManager>().ActiveClone();
        temp.GetComponent<EnemyBall>().moveOn = false;
        temp.transform.position = attackPoint.transform.position;
        temp.transform.rotation = attackPoint.transform.rotation;
        yield return new WaitForSeconds(0.5f);
        if(temp != null)
        temp.GetComponent<EnemyBall>().moveOn = true;
    }
}

