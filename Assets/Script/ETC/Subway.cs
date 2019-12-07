using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subway : MonoBehaviour
{
    public int num;
    public GameObject checkPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, checkPoint.transform.GetChild(num).position) < 1)
        {
            num++;
            if(num >= checkPoint.transform.childCount)
            {
                num = 0;
                gameObject.transform.position = checkPoint.transform.GetChild(0).position;
            }
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, checkPoint.transform.GetChild(num).position, Time.deltaTime * 50);
            gameObject.transform.LookAt(checkPoint.transform.GetChild(num).position);
        }
    }
}
