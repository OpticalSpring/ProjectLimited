using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float followSpeed;
    public float rotationSpeed;
    public float maxDistance;
    float camDistance;
    GameObject Player;
    GameObject cam;
    GameObject realCamera;
    GameObject rotatePivot;
    Vector3 rotateValue;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        cam = gameObject.transform.GetChild(0).gameObject;
        realCamera = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        rotatePivot = gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
        DistanceCheck();
        DistanceSet();
        RotatePosSet();
        RotateCam();
    }
    
    void RotatePosSet()
    {
        rotatePivot.transform.localEulerAngles = new Vector3(0, cam.transform.localEulerAngles.y, 0);
    }

    void Follow()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, Player.transform.position, followSpeed * Time.fixedDeltaTime);
    }

    void DistanceCheck()
    {
        RaycastHit rayHit;
        
        Debug.DrawRay(cam.transform.position, -cam.transform.forward * maxDistance, Color.red);
        int mask = 1 << 9;
        mask = ~mask;
        if (Physics.SphereCast(cam.transform.position, 0.3f, -cam.transform.forward, out rayHit, maxDistance, mask))
        {
            Vector3 hitPoint = rayHit.point;
            camDistance = Vector3.Distance(hitPoint, cam.transform.position) - 1f;
            
        }
        else
        {
            camDistance = maxDistance;
        }
    }
    
    void DistanceSet()
    {
        realCamera.transform.localPosition = new Vector3(1, 0, -camDistance);
    }

    void RotateCam()
    {
        Quaternion targetRotation;

        rotateValue.y += Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
        rotateValue.x -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime;
        rotateValue.x = Mathf.Clamp(rotateValue.x, -30f, 70f);
        targetRotation = Quaternion.Euler(rotateValue);

        Quaternion q = targetRotation;
        cam.transform.rotation = targetRotation;
    }
}
