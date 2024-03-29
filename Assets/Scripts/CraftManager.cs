using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerSprite;
    public Sprite CycleSprite;
    public Animator animator;
    public  SpriteRenderer spriteR;
    public RuntimeAnimatorController animCycle;
    public bool canCraft = false;
    [SerializeField]
    public static bool Crafting = false;
    private bool useSkel = false;
    public bool dontDestroyBodies;
    Rigidbody2D r2d;
    public string craftObject;
    public static bool isCopter;
    private void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name.Contains("Dead"))
        {
            canCraft = true;

            
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.name.Contains("Dead"))
        {
            canCraft = false;

            if (useSkel && !dontDestroyBodies)
            {
                other.gameObject.SetActive(false);
            }
        }
       
    }

    private void Update()
    {
        if (canCraft)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                print("craft");
                Crafting = true;
                if (craftObject == "bike")
                    BecomeCyclist();
                else if (craftObject == "helicopter")
                    BecomeHelicopter();


            }
        }
    }

    public void BecomeCyclist()
    {
        animator.runtimeAnimatorController = animCycle;
        spriteR.flipX = true;
        player.maxSpeed = 15;
        useSkel = true;
        Player.GetComponent<player>().hasCrafted = true;
    }


    public void BecomeHelicopter()
    {
        animator.runtimeAnimatorController = animCycle;
        spriteR.flipX = false;
        player.maxSpeed = 15;
        useSkel = true;
        Player.GetComponent<player>().hasCrafted = true;
        r2d.gravityScale = 0;
        isCopter = true;

    }

}
