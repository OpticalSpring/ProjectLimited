using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector2 HP;
    public float movementSpeed;
    public float attackDistance;
    public float playerDistance;
    public GameObject player;

    public virtual void Start()
    {
        player = GameObject.Find("Player");
    }

    public virtual void Hit()
    {

    }

    public virtual void DistanceCheck()
    {
        playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
    }

    public virtual void Chase()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, movementSpeed * Time.deltaTime);
    }
}
