using UnityEngine;
using UnityEngine.InputSystem;

public class fight : MonoBehaviour
{
    

    public GameObject suicide;
    public fight otherplayer;
    public Move movement;
    public atkmanager state;
    public bool Player2;
    public Flip flipped;

    //Animations
    public Animator moves; //animator

    
    //attacks damages 
   public float LAD;//ligh attack damage
   public float HAD;//heavy attack damage
   public float HAHD;//heavy attack damage after hold
   public float CAD;//chain attacks damage
   public float SAD;//special attack damage
   public float UAD;//launcher attack damage
   public float UAHD;//laucher damage after hold
   private bool atk;//true during damage frames

   
   //heath
   public float hp; //hitpoints
   private float damage;//the damage applied
   public float hitvar;
   public bool gotHit;

   //knockbacks
   public Rigidbody rb;//rigidbody that moves player
   public float lightK;//normal attack knockback
   public float heavyK;//big knockback
   public float upK;//upwards knockbak for launcher
   public float K;
   public float Kup;
   public float chain;
   public bool maxchain;


    //Attack Hitboxes
   public GameObject Lhit;//Light attack hitbox
   public GameObject Hhit;//Heavy attack hitbox
   public GameObject Shit;//Special attack hitbox
   public GameObject Uhit;//Launcher hitbox


   public atkmanager enemy;



    public void Start()
    {
        gotHit=false;

        rb = GetComponent<Rigidbody>();
       
    
    }
    public void Gethit()
    {
        hitvar = Random.Range(-1,1);
       
        
         
            rb.linearVelocity = new Vector3(otherplayer.K, otherplayer.Kup, rb.linearVelocity.z);
        
        gotHit = true;
        if(hitvar>=0)
        {
            moves.Play("Hitted");
        }

        if(hitvar<0)
        {
            moves.Play("Hitted2");
        }
        
        otherplayer.chain += 1;
       
        
    }
        
    

    public void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Attack"))
        {
            Gethit();
        }
    }

    public void Update()
    {
        
        
        if(hp<=0)
        {
            suicide.SetActive(false);
        }

        if(!Player2){
        if (Input.GetKeyDown(KeyCode.F))
        {   
            damage = LAD;
            Kup = 0;

            if(flipped.side==0)
            {
                K= lightK;
            }
            
            if(flipped.side==1)
            {
                K= -lightK;
            }
           
            
            if(!movement.isGrounded)
            {
                Kup = lightK;
            }

           
            


        if (Input.GetKeyDown(KeyCode.F) && chain==0 && !state.stunned && !state.cooldown)
        {
            moves.Play("Light Attack");
        }
        if (Input.GetKeyDown(KeyCode.F) && chain==1 && state.followup && !state.cooldown)
        {
            moves.Play("Chain1");
        }
         if (Input.GetKeyDown(KeyCode.F) && chain==2 && state.followup && !state.cooldown)
        {
            moves.Play("Chain2");
            if(flipped.side==0)
            {
                K= lightK*2;
            }
            
            if(flipped.side==1)
            {
                K= lightK*-2;
            }
        }

        
        }
        
        if(!state.followup)
        {
            chain=0;
        }
        }
        if(Player2){
        if (Input.GetKeyDown(KeyCode.RightShift))
        {   
            damage = LAD;
            K = lightK;
            Kup = 0;

            if(flipped.side==1)
            {
                K= -lightK;
            }
           
            
            if(!movement.isGrounded)
            {
                Kup = -lightK;
            }

           
            


        if (Input.GetKeyDown(KeyCode.RightShift) && chain==0 && !state.stunned && !state.cooldown)
        {
            moves.Play("Light Attack");
        }
        if (Input.GetKeyDown(KeyCode.RightShift) && chain==1 && state.followup && !state.cooldown)
        {
            moves.Play("Chain1");
        }
         if (Input.GetKeyDown(KeyCode.RightShift) && chain==2 && state.followup && !state.cooldown)
        {
            moves.Play("Chain2");
            K = lightK*2;
        }

        
        }
        }
        
        if(!state.followup)
        {
            chain=0;
        }

         if (Input.GetKeyDown(KeyCode.K))
        {
            damage = HAD;
            K = lightK;
            Kup = 0;
            
        }

         if (Input.GetKeyDown(KeyCode.L))
        {
            damage = SAD;
            K = lightK;
            Kup = 0;
            
        }

         if (Input.GetKeyDown(KeyCode.U))
        {
            damage = UAD;
            K = lightK/2;
            Kup = upK;
            
        
            
        }


        
        


    }



  
    
    


}
