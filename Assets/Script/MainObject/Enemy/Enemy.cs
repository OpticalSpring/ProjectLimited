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
    public float hitTime;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Hit()
    {
        HP.x--;
        hitTime = 0.5f;
        Vector3 nPos = gameObject.transform.position;
        nPos.x = nPos.x + Random.Range(-0.1f, 0.1f);
        nPos.z = nPos.z + Random.Range(-0.1f, 0.1f);
        nPos.y = nPos.y + Random.Range(0.0f, 0.3f);
        gameObject.transform.position = nPos;
    }
    protected virtual void Attack()
    {

    }
    protected void DistanceCheck()
    {
        playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
    }

    protected void Chase()
    {
        
        gameObject.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    protected void Turn(GameObject obj, Vector3 target)
    {
        float dz = target.z - obj.transform.position.z;
        float dx = target.x - obj.transform.position.x;

        float rotateDegree = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;

        obj.transform.rotation = Quaternion.RotateTowards(obj.transform.rotation, Quaternion.Euler(0, rotateDegree, 0), 300 * Time.deltaTime);
    }

    protected void DelayOut()
    {
        if (hitTime > 0)
        {
            hitTime -= Time.fixedDeltaTime;
        }
    }

    protected void DeadCheck()
    {
        if(HP.x <= 0)
        {
            Destroy(gameObject.transform.GetChild(0).gameObject, 2);
            gameObject.transform.GetChild(0).gameObject.GetComponent<DestroyMaterial>().enabled = true;
            gameObject.transform.GetChild(0).parent = null;
            Destroy(gameObject);
        }
    }
}
