﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType3 : Enemy
{
    public GameObject attackParent;
    public GameObject[] attackPoint;
    public int eventState;
    public GameObject effect;
    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < 12; i++)
        {
            attackPoint[i].transform.Translate(Vector3.forward * 2);
        }
        StartCoroutine("FSM");
    }

    void OnEffect()
    {
        GameObject eff = Instantiate(effect);
        eff.transform.position = gameObject.transform.position;
        eff.transform.rotation = gameObject.transform.rotation;
        eff.transform.parent = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        DeadCheck();
        switch (eventState)
        {
            case 0:
                DistanceCheck();
                if (playerDistance > attackDistance)
                {
                    Turn(gameObject, player.transform.position);
                    Chase();
                }
                attackParent.transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case 1:
                Turn(gameObject, player.transform.position);
                break;
            case 2:
                attackParent.transform.Rotate(Vector3.up * Time.deltaTime * -50);
                break;
            case 3:
                attackParent.transform.Rotate(Vector3.up * Time.deltaTime * 1000);
                break;
        }
    }


    IEnumerator FSM()
    {
        while (true)
        {
            StartCoroutine("Pattern_1");
            yield return new WaitForSeconds(20f);
            StartCoroutine("Pattern_2");
            yield return new WaitForSeconds(20f);
            StartCoroutine("Pattern_3");
            yield return new WaitForSeconds(20f);
        }
    }

    IEnumerator Pattern_1()
    {
        eventState = 1;
        OnEffect();
        yield return new WaitForSeconds(3f);
        for (int j = 0; j < 6; j++)
        {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundPlay(4, 3);
            for (int i = 0; i < 10; i++)
            {
                attackParent.transform.localEulerAngles = new Vector3(0, 0, 0);
                attackParent.transform.Rotate(Vector3.up * -30);
                Attack(0);
                attackParent.transform.Rotate(Vector3.up * 20);
                Attack(0);
                attackParent.transform.Rotate(Vector3.up * 5);
                Attack(0);
                attackParent.transform.Rotate(Vector3.up * 5);
                Attack(0);
                attackParent.transform.Rotate(Vector3.up * 5);
                Attack(0);
                attackParent.transform.Rotate(Vector3.up * 5);
                Attack(0);
                attackParent.transform.Rotate(Vector3.up * 20);
                Attack(0);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0.5f);
        }
        eventState = 0;
    }

    IEnumerator Pattern_2()
    {
        eventState = 2;
        OnEffect();
        yield return new WaitForSeconds(3f);
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundPlay(4, 3);
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                Attack(j);
            }
            yield return new WaitForSeconds(0.05f);
        }
        eventState = 0;
    }

    IEnumerator Pattern_3()
    {
        eventState = 3;
        OnEffect();
        yield return new WaitForSeconds(3f);
        for (int k = 0; k < 3; k++)
        {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundPlay(4, 3);
            eventState = 3;
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    Attack(j);
                }
                yield return new WaitForSeconds(0.01f);
            }
            eventState = 0;
            yield return new WaitForSeconds(1f);
        }
        eventState = 0;
    }

    void Attack(int i)
    {
        GameObject temp = GameObject.Find("ObjectPool").GetComponent<ObjectPoolManager>().ActiveClone();
        temp.transform.position = attackPoint[i].transform.position;
        temp.transform.rotation = attackPoint[i].transform.rotation;
        temp.GetComponent<EnemyBall>().moveOn = true;
    }
}
