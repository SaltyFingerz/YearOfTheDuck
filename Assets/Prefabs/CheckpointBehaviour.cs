using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehaviour : MonoBehaviour
{
    public GameObject altSpawnObj;
    public List<Enemy> enemiesToEnableAfterTouched;
    bool hasBeenActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Vector2 spawnPoint;

            if (altSpawnObj != null)
            {
                //if alternative spawn object is used
                spawnPoint = altSpawnObj.transform.position;
            }
            else
            {
                //standard checkpoint behaviour
                spawnPoint = transform.position;
            }

            collision.gameObject.GetComponent<player>().SetSpawnPoint(spawnPoint);

            if (!hasBeenActivated)
            {
                hasBeenActivated = true;

                for (int i = 0; i < enemiesToEnableAfterTouched.Count; i++)
                {
                    enemiesToEnableAfterTouched[i].disable = false;
                }
            }
        }
    }
}
