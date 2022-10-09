using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isVertical;
    public Sprite verticalSprite;
    public GameObject projectile;

    public SpriteRenderer sprRend;

    GameObject playerObj;

    float nextTime = 2;
    Vector3 proDir;

    Vector3 proPosOffset;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("Player");

        if (isVertical)
        {
            sprRend.sprite = verticalSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.unscaledTime >= nextTime && projectile != null)
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
            proObj.GetComponent<EnemyProjectile>().dir = proDir;

            nextTime = Time.unscaledTime + 2;
        }
    }
}
