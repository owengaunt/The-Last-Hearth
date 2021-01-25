using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsMovement : MonoBehaviour
{
    public CharacterController CharacterController;
    CharacterInput characterinput;
    PlayerINV playerINV;

    public float gravity = -30f;
    public float jumpHeight = 2.5f;


    public float speed;
    public float walkspeed;
    public float runspeed;
    public float crouchspeed;
    public Animator anim;

    public Transform groundCheck;
    CapsuleCollider theCollider;

    public float groundDistance = 0.9f;
    public LayerMask groundMask;

    public Vector3 velocity;

    public bool Swinging = false;


    bool canJump = true;
    bool inventoryEnabled = false;
    bool isGrounded;
    bool isWalking = false;
    bool isBWDWalking = false;
    bool isLeftWalking = false;
    bool isRightWalking = false;
    bool isSprinting = false;


    //Start of thing
    void Start()
	{
        characterinput = GetComponent<CharacterInput>();
        CharacterController = GetComponent<CharacterController>();
        playerINV = GetComponent<PlayerINV>();

        theCollider = GetComponent<CapsuleCollider>();
        speed = walkspeed;
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryEnabled = !inventoryEnabled;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(Swinging == false)
        {
            Vector3 move = transform.right * x + transform.forward * z;
            CharacterController.Move(move * speed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;

        CharacterController.Move(velocity * Time.deltaTime);


        if (Input.GetButtonDown("Jump") && canJump == true)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            canJump = false;

            StartCoroutine(JumpReset());
        }
        
        if (Input.GetKey(characterinput.backwalkkey))
        {
            speed = walkspeed;
        }
        else
        {
            if (Input.GetKeyDown(characterinput.sprintkey))
            {
                speed = runspeed;

            }
        }

        if (!isGrounded)
        {
            StartCoroutine(AreYouFalling());
        }
        else
        {
            canJump = true;
        }
        

        if (Input.GetKeyUp(characterinput.sprintkey))
        {
            speed = walkspeed;

        }

        //walking animations
        //forward
        if (Input.GetKey(characterinput.walkkey) && !isRightWalking && !isLeftWalking && !isSprinting && !Swinging)
        {
            anim.SetBool("isWalking", true);
            isWalking = true;
        }
        else
        {
            anim.SetBool("isWalking", false);
            isWalking = false;
        }

        //backwards
        if (Input.GetKey(characterinput.backwalkkey) && !isRightWalking && !isLeftWalking && !isSprinting && !Swinging)
        {
            anim.SetBool("isBWalking", true);
            isBWDWalking = true;

        }
        else
        {
            anim.SetBool("isBWalking", false);
            isBWDWalking = false;
        }

        //left
        if (Input.GetKey(characterinput.leftwalkkey) && !isWalking && !isBWDWalking && !isSprinting && !Swinging)
        {
            anim.SetBool("isLWalking", true);
            isLeftWalking = true;
        }
        else
        {
            anim.SetBool("isLWalking", false);
            isLeftWalking = false;
        }

        //right
        if (Input.GetKey(characterinput.rightwalkkey) && !isWalking && !isBWDWalking && !isSprinting && !Swinging)
        {
            anim.SetBool("isRWalking", true);
            isRightWalking = true;
        }
        else
        {
            anim.SetBool("isRWalking", false);
            isRightWalking = false;
        }

        //diagonal front left
        if (Input.GetKey(characterinput.leftwalkkey) && Input.GetKey(characterinput.walkkey) && !isSprinting && !Swinging)
        {
            anim.SetBool("isDFLWalking", true);
            anim.SetBool("isLWalking", false);
            anim.SetBool("isWalking", false);

        }
        else
        {
            anim.SetBool("isDFLWalking", false);
        }


        //diagonal front right
        if (Input.GetKey(characterinput.rightwalkkey) && Input.GetKey(characterinput.walkkey) && !isSprinting && !Swinging)
        {
            anim.SetBool("isDFRWalking", true);
            anim.SetBool("isRWalking", false);
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isDFRWalking", false);
        }

        //diagonal back left
        if (Input.GetKey(characterinput.leftwalkkey) && Input.GetKey(characterinput.backwalkkey) && !isSprinting && !Swinging)
        {
            anim.SetBool("isDBLWalking", true);
            anim.SetBool("isLWalking", false);
            anim.SetBool("isBWalking", false);
        }
        else
        {
            anim.SetBool("isDBLWalking", false);
        }

        //diagonal back right
        if (Input.GetKey(characterinput.rightwalkkey) && Input.GetKey(characterinput.backwalkkey) && !isSprinting && !Swinging)
        {
            anim.SetBool("isDBRWalking", true);
            anim.SetBool("isRWalking", false);
            anim.SetBool("isBWalking", false);
        }
        else
        {
            anim.SetBool("isDBRWalking", false);
        }

        //extra movement animations
        //jumping
        if ((Input.GetKey(characterinput.jumpkey) && !canJump))
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }

        //sprinting forward
        if (Input.GetKey(characterinput.sprintkey) && Input.GetKey(characterinput.walkkey) && !Swinging)
        {
            anim.SetBool("isSprinting", true);
            anim.SetBool("isWalking", false);
            isSprinting = true;
        }
        else
        {
            anim.SetBool("isSprinting", false);
            isSprinting = false;
        }

        //swing animations
        if ((playerINV.istoolEquipped == true) && (Input.GetMouseButton(0)) && inventoryEnabled == false)
        {
            anim.SetBool("isSwinging", true);
            StartCoroutine(ResetSwing());
            Swinging = true;
        }
        
        //standingswing

    }

        public IEnumerator AreYouFalling()
    {
        if (isGrounded)
        {
            canJump = true;
        }
        else
        {
            yield return new WaitForSeconds(1f);

            if (!isGrounded)
            {
                canJump = false;
            }
        }
     
    }

        public IEnumerator JumpReset()
    {
        yield return new WaitForSeconds(1.5f);

        canJump = true;
    }

        public IEnumerator ResetSwing()
    {
        yield return new WaitForSeconds(0.9f);

            anim.SetBool("isSwinging", false);
            Swinging = false;
    } 

}
