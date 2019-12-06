using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAni : MonoBehaviour
{
    Animator ani;
    public int aniState;
    // Start is called before the first frame update
    void Start()
    {
        ani = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAniState();
    }

    void SetAniState()
    {
        ani.SetInteger("AniState", aniState);
    }
    
}
