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
    public VFXType vfxType;
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

    public enum VFXType
    {
      
        Explosion,
        bigRedExplosion,
        greenExplosion,
        megaHit

    }

    public void Start()
    {
        //nota : para a hitbox funcionar, ela precisa ter um rigidbody

    }

    public void OnTriggerEnter(Collider collision)
    {
        if (layerMask == (layerMask | (1 << collision.transform.gameObject.layer)))  //somente afeta a layer mask Water
        {

            Debug.Log("found something");

            hitbox h = this;


            Hurt hurt = collision.GetComponent<Hurt>();                         //pega o script Hurt da hurtbox

            //ballHurtbox ballhurt = collision.GetComponent<ballHurtbox>();       

            if (hurt != null)
            {
                Debug.Log("hitting");
                OnHit(hurt, h);
            }

            if (hurt == null)
            {
                Debug.Log("not hitting");
            }


        }
    }

  

    protected virtual void OnHit(Hurt hurt, hitbox h)
    {
        //hurt.enemy.GetHit(h);
        if (hurt.player != null && hurt != selfHurtBox)
        {
            //acessa a variavel player do script Hurt se ela não for nula, e ativa uma função do script do jogador
            //utilizando todas as variaveis da hitbox e passando elas como parametros
            hurt.player.GetSlowdown(h, impactHit, damage, knockback, hitboxType, vfxType);

         
        }

        if (playerScript != null)
        {
            //faz o hit stun para o player que atingiu o inimigo
            playerScript.Slowdown();
            
        }

       

    }






}