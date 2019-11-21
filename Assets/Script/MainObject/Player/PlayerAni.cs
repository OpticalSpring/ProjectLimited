using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAni : MonoBehaviour
{
    Animator ani;
    public int aniState;
    public int attackState;
    public float movement;
    public float realMovement;
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
        realMovement = Mathf.Lerp(realMovement, movement, Time.fixedDeltaTime * 5);
        ani.SetFloat("Movement", realMovement);
        ani.SetInteger("AniState", aniState);
        ani.SetInteger("AttackState", attackState);
    }
}
