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
    public GameObject enemyBall;
    bool attacked;

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

            Turn(gameObject, nPos);
            realAttackTime -= Time.deltaTime;

        }
        else if (realAttackTime > 0)
        {
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
        }
    }

    IEnumerator DelayShot()
    {
        GameObject temp = Instantiate(enemyBall);
        temp.transform.position = attackPoint.transform.position;
        temp.transform.rotation = attackPoint.transform.rotation;
        yield return new WaitForSeconds(1f);
        if(temp != null)
        temp.GetComponent<EnemyBall>().moveOn = true;
    }
}

