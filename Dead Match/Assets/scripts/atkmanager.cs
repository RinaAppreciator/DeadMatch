using UnityEngine;
using Unity.Collections;

public class atkmanager : MonoBehaviour
{
    public bool atk;
    public bool atkTransition;
    public bool airAtk;
    public bool stunned;
    private Animator moveset;
    public Rigidbody rb;
    public Move movement;
    public fight player;
    public GameObject playerObject;
    public bool followup;
    public bool cooldown;
    public Flip flipped;
    public bool isBeingHit;
    public bool canWalk = true;
    public int chain;
    public bool ableBodied;


  


    public void Start()
    {
        moveset = GetComponent<Animator>();
        canWalk = true;
        ableBodied = true;
    
    }

    private void FixedUpdate()
    {
        if(atkTransition == false)
        {
            moveset.SetBool("Attacking", false);
        }

        if(atkTransition == true)
        { 
            moveset.SetBool("Attacking", true);
        }
    }

    public void Dash()
    {
        if (movement.flipped)
        {
            rb.AddForce(new Vector3(-50, 0, 0), ForceMode.Impulse);

        }

        else
        {
            rb.AddForce(new Vector3(50, 0, 0), ForceMode.Impulse);
        }
            
    }

    public void DashAttack()
    {
        if (movement.flipped)
        {
            rb.AddForce(new Vector3(-100, 0, 0), ForceMode.Impulse);
        }

        else
        {
            rb.AddForce(new Vector3(100, 0, 0), ForceMode.Impulse);
        }
    }

    public void spawnProjectile()
    {
    
        
            player.spawnProjectile();
        

    }


    public void finishAtk() 
    {
        atkTransition = true;
    }

    public void canWalkAgain()
    {
        canWalk = true;
    }

    public void atacking()
    {
        atk = true;
        player.moves.SetBool("Attacking", true);
        followup = false;
    }

    public void airAttacking()
    {
        airAtk = true;
    }

    public void ResetAirAtk()
    {
        airAtk = false;
    }

    public void throwing()
    {
        player.grabbedEnemy.GetThrown(player, playerObject, 6f, 2f);
        player.isGrabbing = false;
        atk = false; 
    }

    // Code for dropping items 


    public void Disable()
    {
        Debug.Log("got disabled");
        ableBodied = false;
    }

    public void Enable()
    {
        Debug.Log("got enabled");
        ableBodied = true;
    }


    public void dropping()
    {
        //player.grabbedEnemy.GetThrown(player, playerObject, 6f, 2f);
        player.isGrabbing = false;
        atk = false;
   
    }
   

    public void attackReset()
   {
    atk = false;
        player.moves.SetBool("Attacking", false);

    }

    public void Super()
    {
        atk = true;

    }

    public void SuperEnd()
    {

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

    void ChainStart()
    {
        
       followup = true;
        
    }

    void ChainEnd()
    {
        followup =false;
        player.chain = 0;
    }

    void CooldownStart()
    {
        Debug.Log("no cooldown");
        //cooldown= true;
    }

    void CooldownEnd()
    {
        //cooldown=false;
    }

    void slowdown()
    {
        //enemy.Slowdown();
        Debug.Log("slowdown");

    }

    //void RestoreSpeed()
    //{
       // isBeingHit = false;
       // moveset.speed = 1f;
       // rb.useGravity = true;
    //}

}
