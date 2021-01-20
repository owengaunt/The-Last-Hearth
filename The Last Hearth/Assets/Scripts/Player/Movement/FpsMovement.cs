using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsMovement : MonoBehaviour
{
    public CharacterController CharacterController;
    CharacterInput characterinput;

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

    bool isCrouched;
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
        theCollider = GetComponent<CapsuleCollider>();
        speed = walkspeed;
    }


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;


        CharacterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        CharacterController.Move(velocity * Time.deltaTime);


        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouched)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetKeyUp(characterinput.crouchkey))
        {
            DoCrouch();
        }

        
        if (Input.GetKey(characterinput.backwalkkey) && !isCrouched)
        {
            speed = walkspeed;
        }
        else
        {
            if (Input.GetKeyDown(characterinput.sprintkey) && !isCrouched)
            {
                speed = runspeed;

            }
        }
       


        if (Input.GetKeyUp(characterinput.sprintkey))
        {
            speed = walkspeed;

        }

        //walking animations
        //forward
        if (Input.GetKey(characterinput.walkkey) && !isRightWalking && !isLeftWalking && !isCrouched && !isSprinting && isGrounded)
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
        if (Input.GetKey(characterinput.backwalkkey) && !isRightWalking && !isLeftWalking && !isCrouched && !isSprinting && isGrounded)
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
        if (Input.GetKey(characterinput.leftwalkkey) && !isWalking && !isBWDWalking && !isCrouched && !isSprinting && isGrounded)
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
        if (Input.GetKey(characterinput.rightwalkkey) && !isWalking && !isBWDWalking && !isCrouched && !isSprinting && isGrounded)
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
        if (Input.GetKey(characterinput.leftwalkkey) && Input.GetKey(characterinput.walkkey) && !isCrouched && !isSprinting && isGrounded)
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
        if (Input.GetKey(characterinput.rightwalkkey) && Input.GetKey(characterinput.walkkey) && !isCrouched && !isSprinting && isGrounded)
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
        if (Input.GetKey(characterinput.leftwalkkey) && Input.GetKey(characterinput.backwalkkey) && !isCrouched && !isSprinting && isGrounded)
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
        if (Input.GetKey(characterinput.rightwalkkey) && Input.GetKey(characterinput.backwalkkey) && !isCrouched && !isSprinting && isGrounded)
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
        if (isGrounded == false)
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }

        //sprinting forward
        if (Input.GetKey(characterinput.sprintkey) && Input.GetKey(characterinput.walkkey) && !isCrouched && isGrounded)
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

        //crouchidle
        if( isCrouched == true)
        {
            anim.SetBool("isCrouching", true);
        }
        else
        {
            anim.SetBool("isCrouching", false);
        }
        
        //crouch forward
        if ((isCrouched == true) && Input.GetKey(characterinput.walkkey) && !isRightWalking && !isLeftWalking && !isSprinting)
        {
            anim.SetBool("isCrouchFWD", true);
            anim.SetBool("isCrouching", false);
            anim.SetBool("isDFLWalking", false);
            anim.SetBool("isLWalking", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isDBRWalking", false);
            anim.SetBool("isRWalking", false);
            anim.SetBool("isBWalking", false);
        }
        else
        {
            anim.SetBool("isCrouchFWD", false);
        }
    
        //crouch backward
        if ((isCrouched == true) && Input.GetKey(characterinput.backwalkkey) && !isRightWalking && !isLeftWalking && !isSprinting)
        {
            anim.SetBool("isCrouchBWD", true);
            anim.SetBool("isCrouching", false);
            anim.SetBool("isDFLWalking", false);
            anim.SetBool("isLWalking", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isDBRWalking", false);
            anim.SetBool("isRWalking", false);
            anim.SetBool("isBWalking", false);
        }
        else
        {
            anim.SetBool("isCrouchBWD", false);
        }

        //crouch left
        if ((isCrouched == true) && Input.GetKey(characterinput.leftwalkkey) && !isWalking && !isBWDWalking && !isSprinting)
        {
            anim.SetBool("isCrouchL", true);
            anim.SetBool("isCrouching", false);
            anim.SetBool("isDFLWalking", false);
            anim.SetBool("isLWalking", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isDBRWalking", false);
            anim.SetBool("isRWalking", false);
            anim.SetBool("isBWalking", false);
        }
        else
        {
            anim.SetBool("isCrouchL", false);
        }


        //crouch right
        if ((isCrouched == true) && Input.GetKey(characterinput.rightwalkkey) && !isWalking && !isBWDWalking && !isSprinting)
        {
            anim.SetBool("isCrouchR", true);
            anim.SetBool("isCrouching", false);
            anim.SetBool("isDFLWalking", false);
            anim.SetBool("isLWalking", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isDBRWalking", false);
            anim.SetBool("isRWalking", false);
            anim.SetBool("isBWalking", false);
        }
        else
        {
            anim.SetBool("isCrouchR", false);
        }


    }
        void DoCrouch()
    {
        
            if (isCrouched)
            {
                theCollider.height += 1f;
                CharacterController.height += 1f;
                speed = walkspeed;
            }
            else
            {
                theCollider.height -= 1f;
                CharacterController.height -= 1f;
                speed = crouchspeed;
            }
                isCrouched = !isCrouched;
            
            }


}
