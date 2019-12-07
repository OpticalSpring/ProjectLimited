using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAniEvent : MonoBehaviour
{
    public void FootStepSoundPlay()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().RandomPlayNew(0, 0, 5);
    }
}
