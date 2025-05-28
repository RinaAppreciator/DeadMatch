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


    


   
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
