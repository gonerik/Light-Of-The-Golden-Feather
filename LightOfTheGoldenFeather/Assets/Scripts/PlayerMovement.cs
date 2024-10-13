using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpPower = 5;
    [SerializeField] private float lightDownSpeed = 0.003f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask slopeLayer;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Animator playerAnimator;

    [SerializeField] private AudioSource jmpUp;
    [SerializeField] private AudioSource jmpDwn;
    
    private Rigidbody2D body;
    private DoorRespawn respawn;
    public float auraScale = 1;
    private BoxCollider2D boxCollider;
    private float jumpCooldown;
    private float horizontalInput;
    public bool midFeatherTaken;
    private bool lockPlayer;
    private bool canJump;
    [SerializeField] private float jumpDelay = 0.2f;
    private bool bigFeatherTaken;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("Instance already exists!");
        }
    }

    private void Start()
    {
        body.gravityScale = 7;
        InvokeRepeating("lightDown", 0f, 0.03f);
    }

    private void lightDown()
    {
        if (auraScale > 0)
            auraScale -= lightDownSpeed;
        else
        {
            StartCoroutine(GameOver());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            lockPlayer = !lockPlayer;
            settingsMenu.SetActive(lockPlayer);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void FixedUpdate()
    {
        if (lockPlayer || checkSlope())
        {
            playerAnimator.SetTrigger("Slide");
            return;
        }
        playerAnimator.ResetTrigger("Slide");
        horizontalInput = Input.GetAxis("Horizontal");
        if (Math.Abs(horizontalInput) > 0.01f)
        {
            playerAnimator.ResetTrigger("Idle");
            playerAnimator.SetTrigger("Run");
        }
        else
        {
            playerAnimator.ResetTrigger("Run");
            playerAnimator.SetTrigger("Idle");
        }
        // Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y, 1);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x)*-1, transform.localScale.y, 1);
        
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y); // Frame rate independent movement

    }

    public IEnumerator GameOver()
    {
        if (lockPlayer) yield break;
        lockPlayer = true;
        playerAnimator.SetBool("Respawn",true);
        yield return new WaitForSeconds(.1f);
        dieInstantly();
    }

    public void setLockPlayer(bool value)
    {
        lockPlayer = value;
    }

    public void dieInstantly()
    {
        body.velocity = new Vector2(0, 0);
        instance.gameObject.transform.position = respawn.transform.position;
        auraScale = respawn.startAura;
        bigFeatherTaken = false;
        FindObjectOfType<cameraManagerScr>().ResetToFirst();
        FeatherManager.restart();
        lockPlayer = false;
        playerAnimator.SetBool("Respawn",false);
    }

    private void Jump()
    {
        if (isGrounded() && canJump)
        {
            canJump = false;
            body.velocity = new Vector2(body.velocity.x, jumpPower); // Jump logic remains the same
            playerAnimator.SetTrigger("Jump");
            jmpUp.Play();
            // anim.SetTrigger("jump");
        }
        else if (midFeatherTaken)
        {
            midFeatherTaken = false;
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            playerAnimator.SetTrigger("Jump");
            jmpUp.Play();
        }
        if (bigFeatherTaken)
        {
            auraScale -= FeatherManager.instance.bigFeatherCost;
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Delay();
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

    void Delay()
    {
        if (canJump == false)
        {
            jmpDwn.Play();

        }
        canJump = true;
    }

    public void collectFeather(float length)
    {
        if (auraScale < length)
            auraScale = length;
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
