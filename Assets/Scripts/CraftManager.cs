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
                BecomeCyclist();
                
            }
        }
    }

    public void BecomeCyclist()
    {
        animator.runtimeAnimatorController = animCycle;
        spriteR.flipX = true;
        player.maxSpeed = 15;
    }



}
