using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public Material _material;
    public Vector2 _texOffset;
    void Start() {
        _material = gameObject.GetComponent<MeshRenderer>().material;
        _texOffset = _material.GetTextureOffset("_MainTex"); } 
    void Update() {  
        _texOffset.x += Time.deltaTime * 0.1f;
        _material.SetTextureOffset("_MainTex", _texOffset);
        _material.SetTextureOffset("_EmissionMap", _texOffset);
    }
        
}
