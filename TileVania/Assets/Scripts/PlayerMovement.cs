using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D playerRigid;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float climbSpeed = 2f;
    Animator anim;
    [SerializeField]float jumpSpeed=10f;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    bool canJump=false;
    float gravityAtStart;
    bool isAlive = true;
    [SerializeField] Vector2 deathPos = new Vector2(10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    void Start()
    {
        canJump = false;
        playerRigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        gravityAtStart = playerRigid.gravityScale;
        feetCollider = GetComponent<BoxCollider2D>();
    }
     
   
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        flipSprite();
        Climb();
        Jump();
       Die();
        onFire();
    }
 
    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
        
    }
    void onFire()
    {
        if (!isAlive) { return; }
        if(Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(bullet, gun.transform.position, transform.rotation);
            anim.SetBool("isShooting", true);
        }
        else
        {
            anim.SetBool("isShooting", false);

        }

    }


    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, playerRigid.velocity.y);
        playerRigid.velocity = playerVelocity;
        bool PlayerhashorizontalSpeed = Mathf.Abs(playerRigid.velocity.x) > Mathf.Epsilon;
        anim.SetBool("isRunning", PlayerhashorizontalSpeed);
       // anim.SetBool("isJumping", false);

    }
    void flipSprite()
    {
        bool PlayerhashorizontalSpeed = Mathf.Abs(playerRigid.velocity.x) > Mathf.Epsilon;
        if(PlayerhashorizontalSpeed)
        {

        transform.localScale = new Vector2(Mathf.Sign(playerRigid.velocity.x), 1f);
        }
    }
    private bool checkIfCanJump()
    {
        if(feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
         {
            canJump = true; 
        }
        return canJump;
    }    
    void Jump()
    {
       if (!isAlive) { return; }


        if ( checkIfCanJump())
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {  anim.SetBool("isRunning", false);
            Debug.Log(canJump);
            playerRigid.velocity = new Vector2(playerRigid.velocity.x, jumpSpeed);
            canJump = false;
               /* if (playerRigid.velocity.y<0)
                {  anim.SetBool("isJumping", true);}*/    
               
            }    
        }
        else
        {
            //anim.SetBool("isJumping", false);
            playerRigid.velocity = new Vector2(playerRigid.velocity.x, playerRigid.velocity.y);
        }

    }
    private void Climb()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        { playerRigid.gravityScale = gravityAtStart;
            anim.SetBool("isCliming", false);
        return;
        }
        Vector2 climbVelocity = new Vector2( playerRigid.velocity.x,moveInput.y*climbSpeed);
        playerRigid.velocity = climbVelocity;
        bool PlayerhasVerticalSpeed = Mathf.Abs(playerRigid.velocity.y) > Mathf.Epsilon;
        anim.SetBool("isCliming", PlayerhasVerticalSpeed);
        playerRigid.gravityScale = 0;
    }
    private void Die()
    {
        if(bodyCollider.IsTouchingLayers(LayerMask.GetMask("enemy", "Harzards")))
        {
            anim.SetTrigger("Death");
            playerRigid.velocity = deathPos;
            isAlive = false;
            FindObjectOfType<GameSession>().playerDeathProcess();
        }    
    }    
}
