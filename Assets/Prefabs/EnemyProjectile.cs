using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public SpriteRenderer sprRend;
    public Rigidbody2D rigbod;
    public Vector2 dir;
    public Sprite iceSprite;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name.Contains("Clone"))
        {
            rigbod.velocity = dir * 4;

            if (tag == "iceTrap")
            {
                sprRend.sprite = iceSprite;
            }

            if (dir == Vector2.up)
            {
                transform.localEulerAngles = new Vector3(0, 0, 90);
            }
            else if (dir == Vector2.down)
            {
                transform.localEulerAngles = new Vector3(0, 0, -90);
            }
            else if (dir == Vector2.left)
            {
                transform.localEulerAngles = new Vector3(0, 0, -180);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision);
        if (gameObject.name.Contains("Clone") && !collision.name.Contains("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
