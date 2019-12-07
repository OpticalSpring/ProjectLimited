using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeControl : MonoBehaviour
{
    public Material[] mat = new Material[4];
    public bool on;
    public float val;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            mat[i] = gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (on == true && val < 1f)
        {
            val += Time.deltaTime;
            for (int i = 0; i < 4; i++)
            {
                mat[i].color = new Color(1, 1, 1, (float)val);
            }
        }
        else if (on == false && val > 0f)
        {
            val -= Time.deltaTime;
            for (int i = 0; i < 4; i++)
            {
                mat[i].color = new Color(1, 1, 1, (float)val);
            }
        }

    }
}
