using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsMovement : MonoBehaviour
{
    public CharacterController CharacterController;
    CharacterInput characterinput;

    public float gravity = -30f;
    public float jumpHeight = 2.5f;
    

    private float speed;
    public float walkspeed;
    public float runspeed;
    public float crouchspeed;
    public Animator anim;

    public Transform groundCheck;
    CapsuleCollider theCollider;

    public float groundDistance = 0.9f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isCrouched;
    bool isGrounded;

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

        //if (Input.GetKeyDown(KeyCode.W) || (KeyCode.A) || (KeyCode.S) || (KeyCode.D) || )
        //{
        //    anim.SetFloat("Speed", walkspeed);
        //}

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

        if (Input.GetKeyDown(characterinput.sprintkey) && !isCrouched)
        {
            speed = runspeed;

        }


        if (Input.GetKeyUp(characterinput.sprintkey))
        {
            speed = walkspeed;

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
