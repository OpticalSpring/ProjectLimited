using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleCheck : MonoBehaviour
{
    public int num;
    public int count;
    public bool go;
    // Start is called before the first frame update
    void Start()
    {
        num = 1;
        count = gameObject.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        PointCheck();
        Turn(gameObject.transform.GetChild(0).gameObject, gameObject.transform.GetChild(num).position);
        Move(gameObject.transform.GetChild(0).gameObject);
    }

    void PointCheck()
    {
        if (Vector3.Distance(gameObject.transform.GetChild(num).position, gameObject.transform.GetChild(0).position) < 0.5f)
        {
            if (go == true)
            {
                if (num == count - 2)
                {
                    go = false;
                }
                num++;
            }
            else
            {
                if (num < 2)
                {
                    go = true;
                }
                num--;
            }
        }
    }

    void Move(GameObject obj)
    {
        obj.transform.Translate(Vector3.forward * Time.deltaTime * 5);
    }

    void Turn(GameObject obj, Vector3 target)
    {
        float dz = target.z - obj.transform.position.z;
        float dx = target.x - obj.transform.position.x;

        float rotateDegree = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;

        obj.transform.rotation = Quaternion.RotateTowards(obj.transform.rotation, Quaternion.Euler(0, rotateDegree, 0), 250 * Time.deltaTime);
    }
}
