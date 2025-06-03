using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static hitbox;


public class fight : MonoBehaviour
{
  
    
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
    public string FighterName;
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
    public AudioClip FinishingBlow;


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

        //if (hp <= 0)
        //{
        //    moves.SetBool("Alive", false);
        //    PlayerManager.Instance.UnregisterPlayer(transform);
        //    StartCoroutine(WaitForDeath());
        //}

        //CheckRecover();
        
        if (state.atk == true)
        {
           //StartCoroutine(hitRecover());
        }
       

        if (chain > 0)
        {
            StartCoroutine(chainResetTimer());
        }

        //if(state.canWalk == false)
        //{
        //    StartCoroutine(hitRecover());
        //}


        
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
        moves.SetFloat("VerticalVelocity", y);       // Informa o animator sobre a velocidade do jogador
        moves.SetFloat("ForwardVelocity", z);

    }

    public void GrabCheck()                 //não é utilizado 
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

    //verifica se o jogador está no chão ou no ar para
    //tocar o move correto

    public void LightAttackInput(InputAction.CallbackContext context)                                   //verifica se o jogador está no chão ou no ar para
                                                                                                        //tocar o move correto
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


    #region atualmente nao utilizado
    private void OnCollisionEnter(Collision collision)          // não é mais utilizado
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

    public void Recover()                                   //antes era utilizado para levantar o jogador caso ele ficasse preso no chão
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

        //antes era usado para usar chain attacks, mas a versão atual do jogo não utiliza chain attacks
        // por causa da personagem usada não possuir attacks em chain.
        // a implementação tradicional de utilizar chains por meio de eventos tem um erro:
        // o chain tem que ser frame-perfect, ou seja, o jogador tem que apertar o botão nos frames exatos da animação
        //a solução ideal seria criar um input buffer , que significaria um sistema que "guarda" inputs por um tempo determinado
        //antes de jogar eles fora.


        yield return new WaitForSeconds(0.5f);
        chain = 0;
        //state.canWalk = true;
    }

    #endregion

    #region Hit stun quando o jogador acerta alguem

    public void Slowdown()
    {
        // ativada quando o jogador bate em alguem. Ela desativa a gravidade e desacelera a animação do jogador
        DisableAnimation();
        DisableGravity();
        StartCoroutine(ResetAttacker());

    }


    public void DisableAnimation()
    {
        //Ela desacelera a animação do jogador
        moves.speed = 0;
        
    }

    public void DisableGravity()
    {

        //desativa uma variavel que afeta uma função da gravidade no script "Move"
        movement.frozenState = true;
     
    }

    IEnumerator ResetAttacker()
    {
        // jogador atacante volta ao normal após 0.2 segundos
        yield return new WaitForSeconds(0.2f);
        RestoreSpeed();
        movement.frozenState = false;

    }



    #endregion 

    #region Hit stun quando o jogador leva dano

    public void GetSlowdown(hitbox collision, AudioClip hitSound, float damage, KnockbackType knockback , HitboxType hitboxType)
    {

        //esta função ativa quando um jogador é atingido. Funciona de maneira um pouco diferente de Slowdown()

        if (damage >= hp)
        {
            //ativa o slowmotion chamando a instance do Roundmanager.
           Debug.Log("killing blow");
            PlaySound(FinishingBlow);
           RoundManager.Instance.SlowdownGame(1);
           moves.SetTrigger("Hurt");
        }

        if (damage < hp && !movement.isGrounded)
        {
            //a ideia aqui é transformar todo move normal do atacante em um move que dá knockback vertical automaticamente se o oponente estiver no ar.
            //porém, há problemas com essa solução que ainda não sei resolver, não está muito consistente.
           
            if (knockback == KnockbackType.Ground)
            {
                Debug.Log("hititng a normal on  the air");
                moves.SetTrigger("UpwardHurt");
                collision.VerticalKnockback = 7;
            }

        }


        if (damage < hp )
        {
            //Se o dano não for um finishing blow, ativa um trigger de animação de acordo com o tipo de animação da hitbox.

            if (knockback == KnockbackType.Upward)
            {
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

        }

       
        gotHit = true;
        PlaySound(hitSound);
        hp -= damage;
        
   
        StartCoroutine(ShakeRoutine(2, collision));
     

    }


    private IEnumerator ShakeRoutine(int shakeCount, hitbox collision)
    {
        //cria o hit stun : o inimigo vibra no mesmo lugar para dar uma sensação de impacto na pessoa que levou o dano

        DisableAnimation();
        DisableGravity();
        shaking = true;
        Vector3 originalPosition = rb.position;
        float shakeSpeed = 0.03f; 

        for (int i = 0; i < shakeCount; i++)
        {
            // Vai para a direita
            rb.MovePosition(originalPosition + Vector3.right * 0.03f);
            yield return new WaitForSeconds(shakeSpeed);

            // Vai para a esquerda
            rb.MovePosition(originalPosition + Vector3.left * 0.03f);
            yield return new WaitForSeconds(shakeSpeed);
            shakeSpeed -= 0.02f;                                        // diminui a velocidade do shake a cada ciclo completo
        }

        
        yield return new WaitForSeconds(0.2f);              //duração do hitstop


        rb.position = originalPosition;
        shaking = false;

        //somente após o personagem terminar de vibrar , a gravidade e a velocidade das animações voltam ao normal
        RestoreSpeed();
        RestoreGravity(collision);

       
    }

    public void RestoreSpeed()
    {
        //volta a velocidade normal da animação
        moves.speed = 1f;
    }


    public void RestoreGravity(hitbox collision)
    {
        //Restora a gravidade, e apenas após isso, GetHit é chamado para ativar o knockback.
        movement.frozenState = false;
        GetHit(collision);
    }


    IEnumerator ResetRecover()
    {
       
        // Acho que não está sendo usado?
        Debug.Log("recovery reset");
        yield return new WaitForSeconds(30f);
        moves.ResetTrigger("Hurt");
        moves.ResetTrigger("UpwardHurt");
        moves.ResetTrigger("DiagonalHurt");
        moves.ResetTrigger("ForwardHurt");
        moves.SetBool("Recovering", false);
        recovered = false;

    }

   

    IEnumerator hitRecover()
    {
        // Resetting player movement
        yield return new WaitForSeconds(0.5f);
        state.atk = false;
        state.ableBodied = true;
    }


    public void GetHit(hitbox collision)
    {

        GameObject enemyObject = collision.transform.root.gameObject;                                   //vai procurar a raiz do objeto que tem a hitbox

        fight player = enemyObject.GetComponent<fight>();                                               //pega o script Fight do objeto raiz 
        hitbox hitBoxObject = collision.GetComponent<hitbox>();                                         //pega o script hitbox 



        
        if (gotHit == true)
        {
            //desativa a ação do outro jogador
            state.ableBodied = false;
            state.atk = false;

            //Recover();

            //Vector3 directionAwayFromAttacker = (transform.position - enemyObject.transform.position).normalized;

            //Vector3 localKnockback = transform.InverseTransformDirection(directionAwayFromAttacker);

            //Debug.Log($"Applied Knockback: {localKnockback}"); // Debugging

            //Vector3 knockback =
            // (transform.right * localKnockback.x * hitBoxObject.HorizontalKnockback) +       
            // (Vector3.up * hitBoxObject.VerticalKnockback);

            //rb.linearVelocity = Vector3.zero;

            //rb.linearVelocity = knockback;


            Vector3 directionAwayFromAttacker = (transform.position - enemyObject.transform.position).normalized;

            
            float horizontalDir = Mathf.Sign(directionAwayFromAttacker.x);

            // cria a direção do knockback em um vetor
            Vector3 knockback = new Vector3(horizontalDir * hitBoxObject.HorizontalKnockback, hitBoxObject.VerticalKnockback, 0f);

            // aplica o vetor
            rb.linearVelocity = Vector3.zero;
            rb.linearVelocity = knockback;

        }
    }


    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

}

#endregion

