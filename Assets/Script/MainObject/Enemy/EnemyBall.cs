using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    public bool moveOn;
    public float movementSpeed;
    float delayTime;
    float deadTime;

    private void OnEnable()
    {
        gameObject.transform.GetChild(0).GetChild(0).localScale = new Vector3(0, 0, 0);
        deadTime = 10;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.GetChild(0).GetChild(0).localScale = Vector3.Lerp(gameObject.transform.GetChild(0).GetChild(0).localScale, new Vector3(1, 1, 1), Time.deltaTime);
        Move();
        if (delayTime < 0)
        {
            delayTime = 0.1f;
            CollisionCheck();
        }
        else
        {
            delayTime -= Time.fixedDeltaTime;
        }

        if (deadTime < 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            deadTime -= Time.deltaTime;
        }
    }
    
    void Move()
    {
        if(moveOn == true)
        {
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
    }




    void CollisionCheck()
    {
        Collider[] colliderArray = Physics.OverlapSphere(gameObject.transform.position, 0.1f);
        for (int i = 0; i < colliderArray.Length; i++)
        {
            if (colliderArray[i].tag == "Player")
            {
                colliderArray[i].gameObject.GetComponent<PlayerControl>().Hit();
                gameObject.SetActive(false);
            }
            else if (colliderArray[i].tag == "Enemy")
            {
                
            }
            else if (colliderArray[i].tag == "Trigger")
            {

            }
            else 
            {
                gameObject.SetActive(false);
            }
        }
    }
}
