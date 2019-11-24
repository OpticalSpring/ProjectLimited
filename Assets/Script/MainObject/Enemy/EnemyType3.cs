using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType3 : Enemy
{
    public GameObject attackPoint;
    public GameObject enemyBall;
    protected override void Start()
    {
        base.Start();
        StartCoroutine("FSM");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator FSM()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator Pattern_1()
    {
        yield return new WaitForSeconds(0.1f);
    }
}
