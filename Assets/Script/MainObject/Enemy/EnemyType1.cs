using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : Enemy
{
    

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
        if(playerDistance > attackDistance)
        {
            Chase();
        }
    }


}
