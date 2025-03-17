using UnityEngine;

public class Dummy : MonoBehaviour
{

    private Animator moves;
    public Rigidbody rb;
    public GameObject Dad;
    private float hit;
    public atkmanager state;
    public fight player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
     public void Start()
    {

        moves = GetComponent<Animator>();
        moves.SetBool("Dummy", true);
    
    }


     public void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Attack"))
        {
            Gethit();
        }
    }


     public void Gethit()
    {
        hit = Random.Range(-1,1);
        rb.linearVelocity = new Vector3(player.K, player.Kup, rb.linearVelocity.z);

        if(hit>=0)
        {
            moves.Play("Hitted");
        }

        if(hit<0)
        {
            moves.Play("Hitted2");
        }
        
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
