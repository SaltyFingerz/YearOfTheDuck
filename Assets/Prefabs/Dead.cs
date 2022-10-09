using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
    public bool triggerActive = false;
    public bool toggle;
    public bool carrying = false;
    public GameObject Player;
    public BoxCollider2D boxC2D;
    public Rigidbody2D rb;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        boxC2D = gameObject.GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
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
            }
            if (toggle)
            {
                Drag();
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("Default");
                //boxC2D.enabled = true;
            }
        }
        else
        {
            toggle = false;
        }
        
    }

    public void Drag()
    {

        
        Vector3 temp = Vector3.MoveTowards(transform.position, Player.transform.position + Vector3.up * 1 - Mathf.Sign(Player.transform.localScale.x)*Vector3.right + new Vector3(0.31f,0.31f,0), 0.1f);
        rb.MovePosition(new Vector2(temp.x, temp.y));
        gameObject.layer = LayerMask.NameToLayer("Carried Body");
        //boxC2D.enabled = false;
    }
}
