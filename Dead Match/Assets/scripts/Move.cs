using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using static Move;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Rigidbody rb;
    public bool isGrounded;
    public bool wasGrounded;
    public bool running = false;
    public fight player;
    public float groundCheckDistance = 1f; // Slightly more than your desired "early detect" buffer
    public LayerMask groundLayer;
    public bool flipped;

    public bool canBlock;

    public GameObject IshtarMesh;
    
    public Transform playerBody;
    public Transform shadow;
    public float playerID;

    private Vector2 movement;
    public Vector3 scale;

    private Vector3 moveDirection;
    private Quaternion m_Rotation = Quaternion.identity;
    [SerializeField] atkmanager state;
    [SerializeField] Animator moves;

    public CapsuleCollider collision;

    public bool frozenState;

    public bool onTheAirHurt;


    public float gravityScale ;

    public int characterID;
    


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        //Code to switch characters

        //playerID = PlayerManager.Instance.players.IndexOf(transform);

        //switch (playerID)
        //{
        //case 0:
        //bodyMesh.sharedMesh = MagicGirl;
        //bodyMesh.materials = MagicGirlMat;
        //break;
        //case 1:
        //bodyMesh.sharedMesh = Soldier;
        //bodyMesh.materials = SoldierMat;
        //break;
        //}
    }

    void Update()
    {


        checkFlipped();

        if (characterID == 0)
        {

        }
        if (playerBody.transform.localScale.z > 0)
        {
            flipped = false;
            
        }

        if (playerBody.transform.localScale.z < 0)
        {
            
            flipped = true;
        }


        if (isGrounded)
        {
            
            moves.SetBool("IsGrounded", true);
            state.airAtk = false;
        }


        if (isGrounded == false)
        {

            
            moves.SetBool("IsGrounded", false);
        }

        if (running == true)
        {
            if ( !flipped)
            {
                if (moveDirection.x > 0 )
                {
                    moves.SetBool("WalkingForward", true);
                }
                else if (moveDirection.x < 0 )
                {
                    moves.SetBool("WalkingBackwards", true);
                    canBlock = true;
                }

            }

            else
            {
                if (moveDirection.x < 0 )
                {
                    moves.SetBool("WalkingForward", true);
                }
                else if (moveDirection.x > 0 )
                {
                    moves.SetBool("WalkingBackwards", true);
                    canBlock = true;
                }

            }


        }

        if (running == false)
        {
            //moves.SetBool("Running", false);
            moves.SetBool("WalkingBackwards", false);
            moves.SetBool("WalkingForward", false);
            canBlock = false;
        }


        Movement();      
 

    }

   

    public void Movement()
    {
        // isso está funcionando bem
        if (moveDirection != Vector3.zero && !state.atk && state.ableBodied && isGrounded == true)
        {
            //playerBody.rotation = Quaternion.Slerp(playerBody.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * 10f);
            running = true;
        }
        else
        {
            running = false;
        }

    }

    public void MovementInput(InputAction.CallbackContext context)
    {


        if (context.canceled)
        {
            moveDirection = Vector3.zero;
            return;
        }


        if (state.atk || state.ableBodied == false)
        {

            Debug.Log("attacking :" + state.atk + " able bodied " + state.ableBodied);
              Debug.Log("can't move");
               return;
            
            
        }

        else
        {
            moveDirection = new Vector2(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y).normalized;
        }
          
      
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (state.atk == true || !state.ableBodied)
        {
            return;
        }

        if (context.started && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, 0);

            moves.SetTrigger("Jump");
            running = false;
            
        }
    }

    public void RunInput(InputAction.CallbackContext context)
    {
        if (state.atk == true || !state.ableBodied)
        {
            return;
        }

        if (context.started && isGrounded && running == false )
        {

            if (characterID == 0)
            {
                if (!flipped)
                {
                    
                    moves.SetTrigger("Dash");
                    //rb.linearVelocity = new Vector3(25, rb.linearVelocity.y, 0);
                   
                }

                else
                {
                   
                    moves.SetTrigger("Dash");
                    
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (!isGrounded)
        {
            //collision.enabled = false;
        }

        if (isGrounded)
        {
            //collision.enabled = true;
        }


        if (!isGrounded && !frozenState )
        {
            //ativa a gravidade custom
           
            //se o jogador estiver no ar, sem estar congelado ou machucado, aplicar a gravidade de maneira normal
            rb.linearVelocity += Vector3.down * gravityScale * Time.fixedDeltaTime;
        }


        

        // Uma maneira interessante de implementar isso seria outros scripts diretamente afetar a gravity scale para aumentar ou diminuir a gravidade.


        if (state.atk || frozenState)
        {
            rb.linearVelocity = Vector3.zero;
        }

        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        Vector3 currentVelocity = rb.linearVelocity;

        // Only apply input-based movement if canWalk is true
        if (!state.atk && state.ableBodied && isGrounded && !frozenState)
        {
            //Debug.Log("can walk in theory");
            Vector3 inputVelocity = new Vector3(moveDirection.x * moveSpeed, currentVelocity.y, 0);
            rb.linearVelocity = inputVelocity;
            //rb.MoveRotation(m_Rotation);
        }
        else
        {
            //Se o jogador não poder se mover, deixe ele se mover na direção natural do knockback
            rb.linearVelocity = new Vector3(currentVelocity.x, currentVelocity.y, 0);
        }

    }

    public void checkFlipped()
    {
        switch(characterID)
        {
            case 0:
                {
                    if (playerBody.transform.localScale.z > 0)
                    {
                        flipped = false;

                    }

                    if (playerBody.transform.localScale.z < 0)
                    {

                        flipped = true;
                    }
                    break;
                }

            case 1:
                {
                    if (playerBody.transform.localScale.z > 0)
                    {
                        flipped = true;

                    }

                    if (playerBody.transform.localScale.z < 0)
                    {

                        flipped = false;
                    }
                    break;
                }

        }
    }


}

