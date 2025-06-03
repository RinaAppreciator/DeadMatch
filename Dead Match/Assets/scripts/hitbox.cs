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

        //quais das animações irão tocar 
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
        //nota : para a hitbox funcionar, ela precisa ter um rigidbody

    }

    public void OnTriggerEnter(Collider collision)
    {
        if (layerMask == (layerMask | (1 << collision.transform.gameObject.layer)))  //somente afeta a layer mask Water
        {
           
            hitbox h = this;


            Hurt hurt = collision.GetComponent<Hurt>();                         //pega o script Hurt da hurtbox

            //ballHurtbox ballhurt = collision.GetComponent<ballHurtbox>();       

            if (hurt != null)
            {
                Debug.Log("hitting");
                OnHit(hurt, h);
            }

            //if (ballhurt != null)
            //{
            //    Debug.Log("hit ball");
            //    OnBallHit(ballhurt, h);
            //}

        }
    }

  

    protected virtual void OnHit(Hurt hurt, hitbox h)
    {
        //hurt.enemy.GetHit(h);
        if (hurt.player != null && hurt != selfHurtBox)
        {
            //acessa a variavel player do script Hurt se ela não for nula, e ativa uma função do script do jogador
            //utilizando todas as variaveis da hitbox e passando elas como parametros
            hurt.player.GetSlowdown(h, impactHit, damage, knockback, hitboxType);

         
        }

        if (playerScript != null)
        {
            //faz o hit stun para o player que atingiu o inimigo
            playerScript.Slowdown();
            
        }

        /// depreciado, mas não apague

        //playerScript.Slowdown();
        //anim.speed = 0.6f; // Reduce animation speed (0.2x slower)
        //Invoke("RestoreSpeed", 1f); // Restore normal speed after 2 seconds

        //if (hurt.enemy != null)
        //{
        //    Debug.Log("found enemy hitbox");
        //    hurt.enemy.Slowdown(h, impactHit, damage);

        //}

    }

    //protected virtual void OnBallHit(ballHurtbox ballhurt, hitbox h)
    //{
    //    ballhurt.ball.GetHit(h);
    //    anim.speed = 0.6f; // Reduce animation speed (0.2x slower)
    //    Invoke("RestoreSpeed", 1f); // Restore normal speed after 2 seconds
    //}





}