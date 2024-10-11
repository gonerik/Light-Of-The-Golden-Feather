using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

using UnityEngine;
 
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpPower = 5;
    [SerializeField] private float lightDownSpeed = 0.003f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private GameObject settingsMenu;
    private Rigidbody2D body;
    private DoorRespawn respawn;
    private float auraScale = 1;
    // private Animator anim;
    private BoxCollider2D boxCollider;
    private float jumpCooldown;
    private float horizontalInput;
    private PlayersLight playerLight;
    public bool midFeatherTaken = false;
    private bool lockPlayer = false;
    private bool flying = false;
    bool canJump;
    float jumpDelay;
    
    private void Awake()
    {
        playerLight = GetComponentInChildren<PlayersLight>();
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        // anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        if(instance == null)
            instance = this;
        else
        {
            Debug.Log("Instance already exists!");
        }
    }

    private void Start()
    {
        InvokeRepeating("lightDown", 0f, 0.03f);
    }

    private void lightDown()
    {
        auraScale -= lightDownSpeed;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            lockPlayer = !lockPlayer;
            settingsMenu.SetActive(lockPlayer);
            
        }
        if (lockPlayer)
        {
            return;
        }
        if (auraScale <= 0)
        {
            lockPlayer = true;
            StartCoroutine(GameOver());
        }
        playerLight.gameObject.transform.localScale = new Vector3(auraScale*6, auraScale*4, 0);
        horizontalInput = Input.GetAxis("Horizontal");
        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //Set animator parameters
        // anim.SetBool("run", horizontalInput != 0);
        // anim.SetBool("grounded", isGrounded());

        //Wall jump logic
        if (jumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            // if (onWall() && !isGrounded())
            // {
            //     body.gravityScale = 0;
            //     body.velocity = Vector2.zero;
            // }
            // else
                body.gravityScale = 7;

            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
            jumpCooldown += Time.deltaTime;
        
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        instance.gameObject.transform.position = respawn.transform.position;
        auraScale = respawn.startAura;
        lockPlayer = false;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            // anim.SetTrigger("jump");
        }

        if (midFeatherTaken)
        {
            midFeatherTaken = false;
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            
        }

        
        // else if (onWall() && !isGrounded())
        // {
        //     if (horizontalInput == 0)
        //     {
        //         body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
        //         transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        //     }
        //     else
        //         body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
        //
        //     wallJumpCooldown = 0;
        // }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            ResetJump();
    }
    void ResetJump()
    {
        canJump = true;
        jumpDelay = 0;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        
        return raycastHit.collider != null;
    }

  

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        // return raycastHit.collider != null;
        return false;
    }

    public void collectFeather(float lenght)
    {
        if(auraScale<lenght) 
            auraScale = lenght;
    }

    public void setRespawn(DoorRespawn respawn)
    {
        this.respawn = respawn;
    }
}