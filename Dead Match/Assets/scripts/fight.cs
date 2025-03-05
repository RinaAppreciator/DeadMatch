using UnityEngine;

public class fight : MonoBehaviour
{
    public GameObject suicide;
    public fight otherplayer;
    public hitbox hit;

    public bool range;

    private float damage;
    //attacks damages 
   public float LAD;//ligh 
   public float HAD;//heavy
   public float HAHD;//heavy hold
   public float CAD;//chain attacks
   public float SAD;//special
   public float UAD;//launcher
   public float UAHD;//laucher hold

   //heath
   public float hp;

   //knockbacks
   public Rigidbody rb;
   public float lightK;
   public float heavyK;
   public float upK;



   public GameObject Lhit;
   public GameObject Hhit;
   public GameObject Shit;
   public GameObject Uhit;



    public void Start()
    {

        hit = Lhit.GetComponent<hitbox>();
    
    }
    public void Gethit()
    {
        hp -= otherplayer.damage;
        
    }

    public void Update()
    {
        

        if(hp<=0)
        {
            suicide.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.J) && hit.hiti)
        {
            damage = LAD;
            otherplayer.rb.linearVelocity = new Vector3(lightK, otherplayer.rb.linearVelocity.y, otherplayer.rb.linearVelocity.z);
            otherplayer.Gethit();
        }

         if (Input.GetKeyDown(KeyCode.K) && hit.hiti)
        {
            damage = HAD;
            otherplayer.rb.linearVelocity = new Vector3(lightK, otherplayer.rb.linearVelocity.y, otherplayer.rb.linearVelocity.z);
            otherplayer.Gethit();
        }

         if (Input.GetKeyDown(KeyCode.L) && hit.hiti)
        {
            damage = SAD;
            otherplayer.rb.linearVelocity = new Vector3(lightK, otherplayer.rb.linearVelocity.y, otherplayer.rb.linearVelocity.z);
            otherplayer.Gethit();
        }

         if (Input.GetKeyDown(KeyCode.U) && hit.hiti)
        {
            damage = UAD;
            otherplayer.rb.linearVelocity = new Vector3(lightK/2, upK, otherplayer.rb.linearVelocity.z);
            otherplayer.Gethit();
        }


    }

  
    
    


}
