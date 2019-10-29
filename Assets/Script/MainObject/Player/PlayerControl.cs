using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public float horizontalValue;
    public float VerticalValue;
    GameObject rotatePos;
    GameObject playerModel;
    Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        rotatePos = GameObject.Find("RotatePos");
        playerModel = gameObject.transform.GetChild(0).gameObject;
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



        if (horizontalValue != 0 || VerticalValue != 0)
        {
            TurnPointCheck();
            Turn(playerModel, targetPos);
            Move();
        }
    }

    void Move()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, moveSpeed * Time.deltaTime);
        RaycastHit rayHit;
        
        int mask = 1 << 9;
        mask = ~mask;
        if (Physics.Raycast(gameObject.transform.position + new Vector3(0,0.5f,0), Vector3.down, out rayHit, 1, mask))
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

        obj.transform.rotation = Quaternion.RotateTowards(obj.transform.rotation, Quaternion.Euler(0, rotateDegree, 0), rotateSpeed * Time.deltaTime);

    }
}
