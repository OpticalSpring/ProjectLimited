using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject mob;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;                    
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine("SpawnMob");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadSceneAsync(1);
        }
    }

    IEnumerator SpawnMob()
    {
        while (true)
        {
            GameObject temp = Instantiate(mob);
            temp.transform.position = spawnPoint.transform.position;
           yield return new WaitForSeconds(3f);
        }
    }
}
