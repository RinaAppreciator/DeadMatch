using UnityEngine;

public class Move : MonoBehaviour
{
   
    public float moveSpeed = 5f; 
    public float jumpForce = 7f; 
    public Rigidbody rb;
    private float moveInput;
    public bool isGrounded;
    public atkmanager state;
    public bool Player2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Player2){
        if(Input.GetKey(KeyCode.A)&& !state.cooldown && !state.stunned)
        {
            rb.linearVelocity = new Vector3(-moveSpeed, rb.linearVelocity.y, 0);
        }
        if(Input.GetKey(KeyCode.D)&& !state.cooldown && !state.stunned)
        {
            rb.linearVelocity = new Vector3(moveSpeed, rb.linearVelocity.y, 0);
        }
        

        if (Input.GetKeyDown(KeyCode.W) && isGrounded && !state.cooldown && !state.stunned)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isGrounded = false;
        }
        }
        if(Player2){
        if(Input.GetKey(KeyCode.LeftArrow)&& !state.cooldown && !state.stunned)
        {
            rb.linearVelocity = new Vector3(-moveSpeed, rb.linearVelocity.y, 0);
        }
        if(Input.GetKey(KeyCode.RightArrow)&& !state.cooldown && !state.stunned)
        {
            rb.linearVelocity = new Vector3(moveSpeed, rb.linearVelocity.y, 0);
        }
        

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded && !state.cooldown && !state.stunned)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isGrounded = false;
        }
        }
    }
    


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

    }

}
