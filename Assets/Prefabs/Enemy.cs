using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool disable;
    public bool isVertical;
    public bool isIce;
    public float fireRate;
    public List<Sprite> sprites;
    public GameObject projectile;

    public SpriteRenderer sprRend;

    GameObject playerObj;

    float nextTime = 0;
    Vector3 proDir;

    Vector3 proPosOffset;

    // Start is called before the first frame update
    void Start()
    {
        int spriteId = 0;

        playerObj = GameObject.Find("Player");

        if (isIce)
        {
            spriteId += 2;
        }

        if (isVertical)
        {
            spriteId++;
        }

        sprRend.sprite = sprites[spriteId];
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTime && projectile != null)
        {
            if (!disable)
            {
                if (isVertical)
                {
                    if (playerObj.transform.position.y > transform.position.y)
                    {
                        proDir = new Vector3(0, 1, 0);
                    }
                    else
                    {
                        proDir = new Vector3(0, -1, 0);
                    }
                }
                else
                {
                    if (playerObj.transform.position.x > transform.position.x)
                    {
                        proDir = new Vector3(1, 0, 0);
                    }
                    else
                    {
                        proDir = new Vector3(-1, 0, 0);
                    }
                }

                GameObject proObj = Instantiate(projectile, transform.position + proDir * 0.5f, Quaternion.identity);
                EnemyProjectile proScr = proObj.GetComponent<EnemyProjectile>();

                proScr.dir = proDir;

                if (isIce)
                {
                    proScr.tag = "iceTrap";
                }
            }

            nextTime = Time.time + fireRate;
        }
    }
}
