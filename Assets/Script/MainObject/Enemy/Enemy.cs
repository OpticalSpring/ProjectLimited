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
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void Hit()
    {
        HP.x--;
    }

    public virtual void DistanceCheck()
    {
        playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
    }

    public virtual void Chase()
    {
        Turn(gameObject, player.transform.position);
        gameObject.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    public virtual void Turn(GameObject obj, Vector3 target)
    {
        float dz = target.z - obj.transform.position.z;
        float dx = target.x - obj.transform.position.x;

        float rotateDegree = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;

        obj.transform.rotation = Quaternion.RotateTowards(obj.transform.rotation, Quaternion.Euler(0, rotateDegree, 0), 300 * Time.deltaTime);
    }
}
