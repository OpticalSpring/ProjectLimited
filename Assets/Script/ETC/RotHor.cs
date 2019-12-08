using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotHor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 n = gameObject.transform.eulerAngles;
        n.x = 0;
        n.z = 0;
        gameObject.transform.eulerAngles = n;
    }
}
