using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : Enemy
{
    

    // Update is called once per frame
    void Update()
    {
        DistanceCheck();
        if(playerDistance > attackDistance)
        {
            Chase();
        }
    }
}
