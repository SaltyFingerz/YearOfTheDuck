using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class player : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;
    public Camera mainCamera;
    public GameObject prefab;

    public bool isAlive = true;
    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    Vector3 cameraPos;
    Vector3 originPos;
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform sprite;
    Animator animator;
    Transform t;
    SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        sprite = transform.GetChild(0);
        animator = sprite.GetComponent<Animator>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
        spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        originPos = gameObject.transform.position;

        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Movement controls
        moveDirection = Input.GetAxis("Horizontal");

        // Change facing direction
        if (moveDirection > 0)
        {
            facingRight = true;
            t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
        }
        if (moveDirection < 0)
        {
            facingRight = false;
            t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && isGrounded && isAlive)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }

        // Camera follow
        if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(t.position.x, cameraPos.y, cameraPos.z);
        }
    }

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius, LayerMask.GetMask("Default"));
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider && !colliders[i].isTrigger)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        // Apply movement velocity
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

        //Animator
        animator.SetBool("On Ground", isGrounded);
        animator.SetFloat("Horizontal Velocity", Mathf.Abs(r2d.velocity.x));
        animator.SetFloat("Vertical Velocity", r2d.velocity.y);

        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
    }

    //check for trap collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "trap" && isAlive)
        {
            StartCoroutine(Dead());
        }
    }

    //trigger version of trap collision (for projectiles)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "trap" && isAlive)
        {
            StartCoroutine(Dead());
        }
    }

    public IEnumerator Dead()
    {
        isAlive = false;
        spriteRenderer.enabled = false;
        mainCollider.enabled = false;
        Instantiate(prefab, gameObject.transform.position + new Vector3(0.31f,0.31f,0.0f), Quaternion.identity); ; // create dead body where the player is
        yield return new WaitForSeconds(1);
        animator.Rebind();
        animator.Update(0f);
        gameObject.transform.position = originPos; // return the player to original position
        spriteRenderer.enabled = true;
        isAlive = true;
        mainCollider.enabled = true;
    }

    public void SetSpawnPoint(Vector2 point) {
        originPos = point;
    }
}