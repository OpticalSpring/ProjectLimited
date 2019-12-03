using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMaterial : MonoBehaviour
{
    Material mat;
    public bool on;
    public float val;
    // Start is called before the first frame update
    void Start()
    {
       if(GetComponent<MeshRenderer>())
        mat = gameObject.GetComponent<MeshRenderer>().material;
        if (GetComponent<SkinnedMeshRenderer>())
            mat = gameObject.GetComponent<SkinnedMeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(on == true && val < 0.5f)
        {
            val += Time.deltaTime;
            mat.SetFloat("_Hide", val);
        }
        else if(on == false && val > -0.5f)
        {
            val -= Time.deltaTime;
            mat.SetFloat("_Hide", val);
        }
    }


}
