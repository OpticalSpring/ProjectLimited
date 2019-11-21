using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    public bool moveOn;
    public float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CheckDelay");
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, new Vector3(1, 1, 1), Time.deltaTime);
        Move();
    }
    
    void Move()
    {
        if(moveOn == true)
        {
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
    }

    IEnumerator CheckDelay()
    {
        while (true)
        {
            CollisionCheck();
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    void CollisionCheck()
    {
        Collider[] colliderArray = Physics.OverlapSphere(gameObject.transform.position, 0.25f);
        for (int i = 0; i < colliderArray.Length; i++)
        {
            if (colliderArray[i].tag == "Player")
            {
                colliderArray[i].gameObject.GetComponent<PlayerControl>().Hit();
                Destroy(gameObject);
            }
            else if (colliderArray[i].tag == "Enemy")
            {
                
            }
            else 
            {
                Destroy(gameObject);
            }
        }
    }
}
