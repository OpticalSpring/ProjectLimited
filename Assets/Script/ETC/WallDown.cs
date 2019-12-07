using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDown : MonoBehaviour
{
    bool set;
    public GameObject wall;
    public GameObject eventManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && set == false)
        {
            eventManager.GetComponent<EventManager>().eventNumber++;
            set = true;
            for (int i = 0; i < wall.transform.childCount; i++)
            {
                wall.SetActive(true);
                wall.transform.GetChild(i).gameObject.GetComponent<BarricadeControl>().on = true;
            }
        }
    }
}
