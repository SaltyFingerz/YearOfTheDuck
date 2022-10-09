using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;
    bool toggle;
    bool carrying = true;
    public GameObject Player;
    BoxCollider2D boxC2D;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        boxC2D = gameObject.GetComponent<BoxCollider2D>();
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
        if (Player.GetComponent<player>().isAlive)
        {
            if (triggerActive && Input.GetKeyDown(KeyCode.E))
            {
                toggle = !toggle;
                carrying = !carrying;
            }
            if (toggle == true && carrying == false)
            {
                Drag();
            }
            else
            {
                boxC2D.enabled = true;
            }
        }
        else
        {
            toggle = false;
        }
        
    }

    public void Drag()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position + Vector3.up * 2 - Mathf.Sign(Player.transform.localScale.x)*Vector3.right + new Vector3(0.31f,0.31f,0), Time.time);
        boxC2D.enabled = false;
    }
}
