using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{


    float horizontalValue;
    float VerticalValue;
    int blinkRotation;
    int blinkInputCount;
    float blinkInputDelay;
    GameObject rotatePos;
    GameObject playerModel;
    GameObject rayObj;
    Vector3 targetPos;

    PlayerState playerState;
    // Start is called before the first frame update
    void Start()
    {
        rotatePos = GameObject.Find("RotatePos");
        playerModel = gameObject.transform.GetChild(0).gameObject;
        rayObj = gameObject.transform.GetChild(1).gameObject;
        playerState = GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        InputCheck();
    }

    void InputCheck()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
        VerticalValue = Input.GetAxisRaw("Vertical");

        
        BlinkCheck();

        if (horizontalValue != 0 || VerticalValue != 0)
        {
            TurnPointCheck();
            Turn(playerModel, targetPos);
            Move();
        }



    }

    void BlinkCheck()
    {
        if (Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D))
        {
            if(blinkRotation == 0 && blinkInputCount == 1)
            {
                blinkInputCount = 0;
                Blink();
            }
            else
            {
                blinkInputCount = 1;
                blinkInputDelay = 0.3f;
            }
            blinkRotation = 0;
        }
        if (Input.GetKeyDown(KeyCode.D) && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.W))
        {
            if (blinkRotation == 1 && blinkInputCount == 1)
            {
                blinkInputCount = 0;
                Blink();
            }
            else
            {
                blinkInputCount = 1;
                blinkInputDelay = 0.3f;
            }
            blinkRotation = 1;
        }
        if (Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.D))
        {
            if (blinkRotation == 2 && blinkInputCount == 1)
            {
                blinkInputCount = 0;
                Blink();
            }
            else
            {
                blinkInputCount = 1;
                blinkInputDelay = 0.3f;
            }
            blinkRotation = 2;
        }
        if (Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D))
        {
            if (blinkRotation == 3 && blinkInputCount == 1)
            {
                blinkInputCount = 0;
                Blink();
            }
            else
            {
                blinkInputCount = 1;
                blinkInputDelay = 0.3f;
            }
            blinkRotation = 3;
        }

        if (blinkInputDelay > 0)
        {
            blinkInputDelay -= Time.fixedDeltaTime;
        }
        else
        {
            blinkInputCount = 0;
        }
    }

    void Blink()
    {
        Debug.Log(blinkRotation);
        RaycastHit rayHit;

        int mask = 1 << 9;
        mask = ~mask;
        rayObj.transform.LookAt(rotatePos.transform.position);
        Vector3 rayObjRot = rayObj.transform.eulerAngles;
        rayObjRot.x = 0;
        rayObjRot.z = 0;
        rayObj.transform.eulerAngles = rayObjRot;
        if (Physics.Raycast(gameObject.transform.position + new Vector3(0, 1f, 0), rayObj.transform.forward , out rayHit, playerState.blinkDistance, mask))
        {


            gameObject.transform.position = rayHit.point;
            gameObject.transform.position += new Vector3(0,1f, 0);
        }
        else
        {
            rotatePos.transform.localPosition = new Vector3(horizontalValue * playerState.blinkDistance, 0, VerticalValue * playerState.blinkDistance);
            

            gameObject.transform.position = rotatePos.transform.position;
            gameObject.transform.position += new Vector3(0,1f, 0);
        }
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, playerState.walkSpeed * Time.fixedDeltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, playerState.fastRunSpeed * Time.fixedDeltaTime);
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, playerState.slowRunSpeed * Time.fixedDeltaTime);
        }
        
        RaycastHit rayHit;
        
        int mask = 1 << 9;
        mask = ~mask;
        if (Physics.Raycast(gameObject.transform.position + new Vector3(0,1,0), Vector3.down, out rayHit, 50, mask))
        {
            Vector3 hitPoint = rayHit.point;
            gameObject.transform.position = hitPoint;
        }
    }

    void TurnPointCheck()
    {
        rotatePos.transform.localPosition = new Vector3(horizontalValue * 1000, 0, VerticalValue * 1000);
        targetPos = rotatePos.transform.position;
    }

    void Turn(GameObject obj, Vector3 target)
    {
        float dz = target.z - obj.transform.position.z;
        float dx = target.x - obj.transform.position.x;

        float rotateDegree = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;

        obj.transform.rotation = Quaternion.RotateTowards(obj.transform.rotation, Quaternion.Euler(0, rotateDegree, 0), playerState.rotateSpeed * Time.deltaTime);

    }
}
