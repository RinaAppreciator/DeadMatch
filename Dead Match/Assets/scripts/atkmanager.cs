using UnityEngine;
using Unity.Collections;

public class atkmanager : MonoBehaviour
{
   public bool atk;
   public bool stunned;
   private Animator moveset;
   public Move movement;
   public fight player;
   public bool followup;
   public bool cooldown;
   public Flip flipped;


   public GameObject Lhitbox;//Light attack hitbox
   public GameObject Hhitbox;//Heavy attack hitbox
   public GameObject Chitbox;//Chain attack hitbox
   public GameObject Shitbox;//Special attack hitbox
   public GameObject Uhitbox;//Launcher hitbox

    public void Start()
    {
        if(player.Player2)
        {
            player.K = -player.K;
        }
    }
   public void atacking()
   {
    atk = true;
    followup = true;
   }

   public void attackReset()
   {
    atk = false;
    Lhitbox.SetActive(false);
    Hhitbox.SetActive(false);
    Chitbox.SetActive(false);
    Shitbox.SetActive(false);
    Uhitbox.SetActive(false);

   }
   
   void Latk()
   {
    Lhitbox.SetActive(true);

    if(movement.isGrounded)
            {
            movement.rb.linearVelocity = new Vector3(player.K , movement.rb.linearVelocity.y, movement.rb.linearVelocity.z);
            }
   }

   void Hatk()
   {
     Hhitbox.SetActive(true);
     if(movement.isGrounded)
            {
            movement.rb.linearVelocity = new Vector3(player.K , movement.rb.linearVelocity.y, movement.rb.linearVelocity.z);
            }
   }

   void Catk()
   {
    Chitbox.SetActive(true);
    if(movement.isGrounded)
            {
            movement.rb.linearVelocity = new Vector3(player.K , movement.rb.linearVelocity.y, movement.rb.linearVelocity.z);
            }
   }

   void Satk()
   {
    Shitbox.SetActive(true);
   }

   void Uatk()
   {
     Uhitbox.SetActive(true);
   }

    void attaked()
    {
        stunned = true;
        followup = false;
    }

    void stunend()
    {
        stunned = false;
    }

    void ChainEnd()
    {
        followup =false;
    }

    void CooldownStart()
    {
        cooldown= true;
    }

    void CooldownEnd()
    {
        cooldown=false;
    }

}
