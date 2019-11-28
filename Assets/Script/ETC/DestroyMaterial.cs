using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMaterial : MonoBehaviour
{
    Material mat;
    public float val;
    // Start is called before the first frame update
    void Start()
    {
        val = 0.5f;
        mat = gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        val -= Time.deltaTime;
        mat.SetFloat("_Hide", val);
    }
}
