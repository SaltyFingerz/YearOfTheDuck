using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;
    bool toggle = false;
    public GameObject Player;
    public GameObject sprite;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.E))
        {
            do
            {
                toggle = true;
                Drag();
            } while (toggle == true);
        }
    }

    public void Drag()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, (float)0.1);
    }
}
