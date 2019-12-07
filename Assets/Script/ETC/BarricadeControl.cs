using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeControl : MonoBehaviour
{
    public Material material1, material2;
    public bool on;
    public float val;
    // Start is called before the first frame update
    void Start()
    {
        material1 = gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
        material2 = gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (on == true && val < 1f)
        {
            val += Time.deltaTime;
            material1.color = new Color(1, 1, 1, (float)val);
            material2.color = new Color(1, 1, 1, (float)val);
        }
        else if (on == false && val > 0f)
        {
            val -= Time.deltaTime;
            material1.color = new Color(1, 1, 1, (float)val);
            material2.color = new Color(1, 1, 1, (float)val);
        }

    }
}
