using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static hitbox;


public class fight : MonoBehaviour
{
    public GameObject suicide;
    
    public Move movement;
    public atkmanager state;

    public bool isGrabbing;
    public bool shaking;

    // Animations
    public Animator moves; // animator
    public int grabLayerIndex;
    public int baseLayerIndex;

    // Attacks damages

    private bool atk;  // true during damage frames

    // Health
    public float hp;  // hitpoints
    public float maxHP = 150;
    private float damage;  // the damage applied
    public float hitvar;
    public bool gotHit;
    public int score;

    // Knockbacks
    public Rigidbody rb;  // rigidbody that moves player

    public float chain;
    public bool maxchain;

    public bool OnTheGroundHurt;
    public bool recovered;
   
    // Attack Hitboxes
    public atkmanager enemy;
    public Enemy grabbedEnemy;

    //sound effects
    AudioSource audioSource;
    public AudioClip HitClip;
    public AudioClip ShootClip;
    public AudioClip UppercutClip;


    public void Start()
    {
        hp = maxHP;
        gotHit = false;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        //baseLayerIndex = moves.GetLayerIndex("Base");
        //grabLayerIndex = moves.GetLayerIndex("Grab");
        chain = 0;
        moves.SetBool("Alive", true);
    }

    public void Update()
    {
        //PlayerManager.Instance.RegisterPlayer(transform);

        if (transform.position.y < -10.0f)
        {
            hp = 0;
        }

        if (hp <= 0)
        {
            moves.SetBool("Alive", false);
            PlayerManager.Instance.UnregisterPlayer(transform);
            StartCoroutine(WaitForDeath());
        }

        CheckRecover();
        
        if (state.atk == true)
        {
           //StartCoroutine(hitRecover());
        }
       

        if (chain > 0)
        {
            StartCoroutine(chainResetTimer());
        }

        if(state.canWalk == false)
        {
            StartCoroutine(hitRecover());
        }


        
        GrabCheck();
    }

    public void FixedUpdate()
    {
        Vector3 velocity = rb.linearVelocity;

        // Deadzone to avoid jitter on each axis
        float x = Mathf.Abs(velocity.x) < 0.05f ? 0f : velocity.x; // Horizontal (strafe)
        float y = Mathf.Abs(velocity.y) < 0.05f ? 0f : velocity.y; // Vertical (jump/fall)
        float z = Mathf.Abs(velocity.z) < 0.05f ? 0f : velocity.z; // Forward/back

        moves.SetFloat("HorizontalVelocity", x);
        moves.SetFloat("VerticalVelocity", y);       // For jump/fall
        moves.SetFloat("ForwardVelocity", z);

    }

    public void GrabCheck()
    {
        if (isGrabbing == true)
        {
            moves.SetLayerWeight(baseLayerIndex, 0);
            moves.SetLayerWeight(grabLayerIndex, 1);
        }
        else
        {
            moves.SetLayerWeight(grabLayerIndex, 0);
            moves.SetLayerWeight(baseLayerIndex, 1);
        }
    }

    public void CheckRecover()
    {
        if (state.ableBodied)
        {
            recovered = true;
            //gotHit = false;
            OnTheGroundHurt = false;
            //moves.SetBool("Recovered", true);


            //StartCoroutine(ResetRecover());
        }
    }

    #region player input

    public void LightAttackInput(InputAction.CallbackContext context)
    {
        if (context.started && state.atk == false && chain == 0 && state.ableBodied && state.airAtk == false)
        {
            if (movement.isGrounded == false)
            {

                moves.Play("Air_Jab");
            }

            else
            {
                moves.Play("Slash");
            }
        }


        //if (context.started && state.atk == false && chain == 1 && state.followup && !isGrabbing)
        // {
        //Debug.Log("second attack");
        //moves.Play("Light3");
        //chain += 1;
        //}


        // if (context.started && state.atk == false && chain == 2 && state.followup && !isGrabbing )
        //  {
        // Debug.Log("third attack");
        //moves.Play("Light2");
        // chain = 0;

    }



    public void HeavyAttackInput(InputAction.CallbackContext context )
    {
        if (context.started && state.atk == false && chain == 0 && state.ableBodied && state.airAtk == false )
        {
            if (movement.isGrounded == false )
            {

                moves.Play("Air_Heavy");
            }

            else
            {
                moves.Play("Overhead");
            }

       
        }

    }

    public void LauncherAttackInput(InputAction.CallbackContext context)
    {
        if (context.started && state.atk == false && state.ableBodied && state.airAtk == false)
        {


            if (movement.isGrounded == false)
            {

                moves.Play("Air_Slash");
            }

            else
            {
                moves.Play("Poke");
            }

 

        }
    }

    public void ShootAttackInput(InputAction.CallbackContext context)
    {
        if (context.started && state.atk == false && state.ableBodied && state.airAtk == false)
        {

            if (movement.isGrounded == false)
            {

                moves.Play("Air_Kick");
            }

            else
            {
                moves.Play("Kick");
            }


        }
    
    }

    public void GrabAttackInput(InputAction.CallbackContext context)
    {
        if (context.started && state.atk == false && state.ableBodied && state.airAtk == false)
        {

            if (movement.isGrounded == false)
            {

                moves.Play("Air_Dust");
            }

            else
            {
                moves.Play("Uppercut");
            }




            // if (isGrabbing)
            // {
            // Debug.Log("Throwing");
            //  moves.Play("Throw");
            //  }
            //  else
            //      {
            //    Debug.Log("Grabbing");
            //    moves.Play("Grab");
            //  }


        }
    }

    #endregion


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (gotHit == true)
            {
                //checking if the player has hit the ground after being thrown in the air. Deprecated for now, will re-enable again later


                //moves.SetBool("Hurt", false);
                //OnTheGroundHurt = true;
                //Recover();
            }

        }

    }

    public void Recover()
    {
        Debug.Log("recovering now");
        //player is supposedly playing the recover animation at this moment
        moves.SetBool("Recovering", true);
        StartCoroutine(RecoverTimer());
    
    }   


    IEnumerator RecoverTimer()
    {
        //For when the player is on the ground, hurt
        yield return new WaitForSeconds(0.8f);
        gotHit = false;
        OnTheGroundHurt = false;
        //moves.SetBool("Recovered", true);
        StartCoroutine(ResetRecover());
    }


    IEnumerator chainResetTimer()
    {
        yield return new WaitForSeconds(0.5f);
        chain = 0;
        //state.canWalk = true;
    }

    #region Getting hit


    public void Slowdown()
    {
        DisableAnimation();
        DisableGravity();
    }

    public void DisableAnimation()
    {
        moves.speed = 0;
        StartCoroutine(RestoreSpeedCoroutine());
    }

    public void DisableGravity()
    {
        //rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.useGravity = false;
        StartCoroutine(RestoreGravity());
    }

    IEnumerator RestoreGravity()
    {
        yield return new WaitForSeconds(0.4f);
        rb.useGravity = true;
        //rb.constraints = RigidbodyConstraints.None;
        //rb.constraints = RigidbodyConstraints.FreezePositionZ;
        //rb.constraints = RigidbodyConstraints.FreezeRotation;

    }
    IEnumerator RestoreSpeedCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        moves.speed = 1f;
        

    }


    public void GetSlowdown(hitbox collision, AudioClip hitSound, float damage, KnockbackType knockback , HitboxType hitboxType)
    {

        //if (damage >= hp)
        //{
        //    Debug.Log("killing blow");
        //    moves.SetTrigger("Hurt");
        //}



        //for when the player is hit
        if (knockback == KnockbackType.Upward)
        {
            Debug.Log("why");
            moves.SetTrigger("UpwardHurt");          
        }

        if (knockback == KnockbackType.Forward)
        {
            moves.SetTrigger("ForwardHurt");        
        }
        if (knockback == KnockbackType.Spinning)
        {
            moves.SetTrigger("DiagonalHurt");
        }
        if (knockback == KnockbackType.Ground)
        {
            moves.SetTrigger("NormalHurt");
        }

        DisableAnimation();
        DisableGravity();

        gotHit = true;
        PlaySound(hitSound);
        hp -= damage;
        
   
        StartCoroutine(ShakeRoutine(2, collision));
     

    }





    

    IEnumerator RestoreSpeedCoroutine(hitbox collision)
    {
        // for when the player hits something 
        yield return new WaitForSeconds(0.2f);
        moves.speed = 1f;
        rb.useGravity = true;
        GetHit(collision);
    }

    IEnumerator ResetRecover()
    {
        //this is being called every turn, fix it



        // After the player has recovered and is back on their feet
        Debug.Log("recovery reset");
        yield return new WaitForSeconds(30f);
        moves.ResetTrigger("Hurt");
        moves.ResetTrigger("UpwardHurt");
        moves.ResetTrigger("DiagonalHurt");
        moves.ResetTrigger("ForwardHurt");
        moves.SetBool("Recovering", false);
        recovered = false;

    }

    IEnumerator WaitForDeath()
    {
        // What happens when the player dies
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    IEnumerator hitRecover()
    {
        // Resetting player movement
        yield return new WaitForSeconds(0.5f);
        state.atk = false;
        state.ableBodied = true;
    }


    private IEnumerator ShakeRoutine(int shakeCount, hitbox collision)
    {
        shaking = true;
        Vector3 originalPosition = rb.position;
        float shakeSpeed = 0.03f; // Adjust for how fast the shake happens

        for (int i = 0; i < shakeCount; i++)
        {
            // Move slightly right
            rb.MovePosition(originalPosition + Vector3.right * 0.03f);
            yield return new WaitForSeconds(shakeSpeed);

            // Move slightly left
            rb.MovePosition(originalPosition + Vector3.left * 0.03f);
            yield return new WaitForSeconds(shakeSpeed);
            shakeSpeed -= 0.02f;
        }

        // Return to the original position
        //rigidbody2D.MovePosition(originalPosition);
        rb.isKinematic = false;
        shaking = false;
        StartCoroutine(RestoreSpeedCoroutine(collision));
    }


    public void GetHit(hitbox collision)
    {

        GameObject enemyObject = collision.transform.root.gameObject;

        hitvar = Random.Range(-1, 1);

        fight player = enemyObject.GetComponent<fight>();
        hitbox hitBoxObject = collision.GetComponent<hitbox>();



        if (hitBoxObject != null)
        {
            Debug.Log("found reference to hitbox data");
        }

        Debug.Log(hitBoxObject.HorizontalKnockback);

        


        if (gotHit == true)
        {
            state.ableBodied = false;
            state.atk = false;

            Debug.Log("got hit, recovering ");

            Recover();

            //moves.Play("Hitted");
            //gotHit = false;

            Vector3 directionAwayFromAttacker = (transform.position - enemyObject.transform.position).normalized;

            // Break it down into local directions relative to THIS character
            Vector3 localKnockback = transform.InverseTransformDirection(directionAwayFromAttacker);

            Debug.Log($"Applied Knockback: {localKnockback}"); // Debugging

            Vector3 knockback =
             (transform.right * localKnockback.x * hitBoxObject.HorizontalKnockback) +       // Sideways
             (transform.forward * localKnockback.z * hitBoxObject.ForwardKnockback) +        // Forward/back
             (Vector3.up * hitBoxObject.VerticalKnockback);

            rb.linearVelocity = Vector3.zero;

            rb.linearVelocity = knockback;


            if (hitBoxObject.HorizontalKnockback < 2 && hitBoxObject.VerticalKnockback < 2)
            {
                {

                    //moves.Play("Hurt");
                    //gotHit = false;
                }
            }







        }




    }


    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

}

#endregion 