using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;
    bool toggle;
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
            toggle = !toggle;
        }
        if(toggle == true)
        {
            Drag();
        }
    }

    public void Drag()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position * -1, Time.time);
    }
}
