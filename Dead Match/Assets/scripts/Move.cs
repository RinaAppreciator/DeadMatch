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

    public GameObject IshtarMesh;
    public Mesh MagicGirl;
    public Mesh Soldier;
    public SkinnedMeshRenderer bodyMesh;
    public Material[] MagicGirlMat;
    public Material[] SoldierMat;
    public Transform playerBody;
    public Transform shadow;
    public float playerID;

    private Vector2 movement;

    private Vector3 moveDirection;
    private Quaternion m_Rotation = Quaternion.identity;
    [SerializeField] atkmanager state;
    [SerializeField] Animator moves;

    public float gravityScale = 1f;
    


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.useGravity = false;

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
    

        //if (shadow.position.y != 0.25f)
        //{
        //Vector3 pos = shadow.position;
        // pos.y = 0.25f;
        //shadow.position = pos;
        //}

        if (isGrounded)
        {
            moves.SetBool("OnAir", false);
            state.airAtk = false;
        }


        if (!isGrounded)
        {
            moves.SetBool("OnAir", true);
        }

        if (running == true)
        {
            moves.SetBool("Running", true);
        }

        if (running == false)
        {
            moves.SetBool("Running", false);
        }


        Movement();      
 

    }

   

    public void Movement()
    {
        // isso está funcionando bem
        if (moveDirection != Vector3.zero && !state.atk && state.ableBodied)
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
        
              Debug.Log("player tried to move while he is attacking or disabled, what a fool");
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

            //moves.Play("Jump");
            running = false;
            
        }
    }

    void FixedUpdate()
    {

        //if (!isGrounded)
        //{
        //    rb.linearVelocity += Vector3.down * gravityScale * Time.fixedDeltaTime;
        //}


        if (state.atk )
        {
            rb.linearVelocity = Vector3.zero;
        }

        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        Vector3 currentVelocity = rb.linearVelocity;

        // Only apply input-based movement if canWalk is true
        if (!state.atk && state.ableBodied)
        {
            //Debug.Log("can walk in theory");
            Vector3 inputVelocity = new Vector3(moveDirection.x * moveSpeed, currentVelocity.y, 0);
            rb.linearVelocity = inputVelocity;
            //rb.MoveRotation(m_Rotation);
        }
        else
        {
            //Debug.Log("not able to walk");
            // Keep Y velocity and external forces like knockback
            rb.linearVelocity = new Vector3(currentVelocity.x, currentVelocity.y, 0);
        }

    }



}

