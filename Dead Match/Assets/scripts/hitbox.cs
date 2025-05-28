using UnityEngine;
using Unity.Collections;
using System.Collections;

public class hitbox : MonoBehaviour
{

    public GameObject player;
    public fight playerScript;
    public Move movement;
    public Hurt selfHurtBox;
    public Animator anim;
    public bool hiti;
    public float VerticalKnockback;
    public float HorizontalKnockback;
    public float ForwardKnockback;
    public float damage;
    public KnockbackType knockback;
    public HitboxType hitboxType;
    public LayerMask layerMask;

    public AudioClip impactHit;
    public AudioClip soundHit;

    public enum KnockbackType
    {
        Upward,
        Downward,
        Forward,
        Spinning,
        Ground
    }

    public enum HitboxType
    {
        GroundLevel,
        AirLevel
    }

    public void Start()
    {

    }

    public void OnTriggerEnter(Collider collision)
    {
        if (layerMask == (layerMask | (1 << collision.transform.gameObject.layer)))
        {
            Debug.Log("found hitbox");
            Debug.Log(collision.name);
            hitbox h = this;


            Hurt hurt = collision.GetComponent<Hurt>();
            ballHurtbox ballhurt = collision.GetComponent<ballHurtbox>();

            if (hurt != null)
            {
                Debug.Log("hitting");
                OnHit(hurt, h);
            }
            if (ballhurt != null)
            {
                Debug.Log("hit ball");
                OnBallHit(ballhurt, h);
            }

        }
    }

    void RestoreSpeed()
    {
        // anim.speed = 1f;
    }

    protected virtual void OnHit(Hurt hurt, hitbox h)
    {
        //hurt.enemy.GetHit(h);
        if (hurt.player != null && hurt != selfHurtBox)
        {
            Debug.Log("hitting player");

            hurt.player.GetSlowdown(h, impactHit, damage, knockback, hitboxType);

         
        }
        if (hurt.enemy != null)
        {
            Debug.Log("found enemy hitbox");
            hurt.enemy.Slowdown(h, impactHit, damage);
            
        }

        if (playerScript != null)
        {
            playerScript.Slowdown();
            
        }

        //playerScript.Slowdown();
        //anim.speed = 0.6f; // Reduce animation speed (0.2x slower)
        //Invoke("RestoreSpeed", 1f); // Restore normal speed after 2 seconds
    }

    protected virtual void OnBallHit(ballHurtbox ballhurt, hitbox h)
    {
        ballhurt.ball.GetHit(h);
        anim.speed = 0.6f; // Reduce animation speed (0.2x slower)
        Invoke("RestoreSpeed", 1f); // Restore normal speed after 2 seconds
    }





}