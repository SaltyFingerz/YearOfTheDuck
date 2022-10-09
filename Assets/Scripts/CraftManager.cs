using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public GameObject Player;
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
            }
        }
    }




}
