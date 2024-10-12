using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine;
 
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpPower = 5;
    [SerializeField] private float lightDownSpeed = 0.003f;
    [SerializeField] private LayerMask groundLayer ;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask slopeLayer;
    [SerializeField] private GameObject settingsMenu;
    private Rigidbody2D body;
    private DoorRespawn respawn;
    public float auraScale = 1;
    // private Animator anim;
    private BoxCollider2D boxCollider;
    private float jumpCooldown;
    private float horizontalInput;
    private PlayersLight playerLight;
    public bool midFeatherTaken;
    private bool lockPlayer;
    private bool flying = false;
    private bool canJump;
    [SerializeField] private float jumpDelay = 0.2f;
    private bool bigFeatherTaken;
    bool dead = false;
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
        if(auraScale>0)
            auraScale -= lightDownSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            lockPlayer = !lockPlayer;
            settingsMenu.SetActive(lockPlayer);
        }
        if (lockPlayer || checkSlope())
        {
            return;
        }
        if (auraScale <= 0 && !lockPlayer)
        {
            StartCoroutine(GameOver());
        }
        else
        {
            //playerLight.gameObject.transform.localScale = new Vector3(auraScale*6, auraScale*4, 0);
        }
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
        body.gravityScale = 7;

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
    }

    public IEnumerator GameOver()
    {
        lockPlayer = true;
        yield return new WaitForSeconds(2f);
        dieInstantly();
    }

    public void dieInstantly()
    {
        instance.gameObject.transform.position = respawn.transform.position;
        auraScale = respawn.startAura;
        bigFeatherTaken = false;
        FindObjectOfType<cameraManagerScr>().ResetToFirst();
        FeatherManager.restart();
        lockPlayer = false;
    }

    private void Jump()
    {
        if (isGrounded() && canJump)
        {
            canJump = false;
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            // anim.SetTrigger("jump");
        }

        else if (midFeatherTaken)
        {
            midFeatherTaken = false;
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            
        }
        if (bigFeatherTaken)
        {
            auraScale -= FeatherManager.instance.bigFeatherCost;
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer==7)
        {
            StartCoroutine(Delay());
            midFeatherTaken = false;
        }

        
    }

    private bool checkSlope()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, slopeLayer);
        
        return raycastHit.collider != null;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        
        return raycastHit.collider != null;
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(jumpDelay);
        canJump = true;
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

    public void setBigFeatherTaken(bool value)
    {
        bigFeatherTaken = value;
    }
}