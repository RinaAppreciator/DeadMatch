using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static hitbox;


public class fight : MonoBehaviour
{

    public float meter = 100;
    public float maxMeter = 100;

    public GameObject hitboxes;
    
    public Move movement;
    public atkmanager state;

    public bool isGrabbing;
    public bool shaking;

    // Animations
    public Animator moves; // animator
    public int grabLayerIndex;
    public int baseLayerIndex;

    public int characterID;


    // Attacks damages

    private bool atk;  // true during damage frames

    // Health
    public float hp;  // hitpoints
    public string FighterName;
    public float maxHP = 150;
    private float damage;  // the damage applied
    public float hitvar;
    public bool gotHit;
    public bool gotHitInAir;
    public int score;




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
    public AudioClip blockClip;

    //inputs
    private bool lightInputBuffered = false;
    private bool HeavyInputBuffered = false;
    private bool special1InputBuffered = false;
    private bool special2InputBuffered = false;
    private float inputBufferTime = 0.3f; 
    private float bufferTimer = 0f;

    //prefabs and projectiles
    public GameObject crossPrefab;
    public GameObject crossSpawnPoint;
    public GameObject VFXred;
    public GameObject VFXexplosion;
    public GameObject greenGlow;
    public GameObject block;
    public GameObject VFXspawnPoint;

    public bool canBounce;
 


    public void Start()
    {
      
        meter = 0;
        hp = maxHP;
        gotHit = false;
        gotHitInAir = false;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
       
        chain = 0;
        moves.SetBool("Alive", true);
    }

    public void Update()
    {
        //PlayerManager.Instance.RegisterPlayer(transform);

        Debug.Log("can block" + movement.canBlock);

        if (lightInputBuffered || HeavyInputBuffered || special1InputBuffered || special2InputBuffered)
        {
            bufferTimer -= Time.deltaTime;
            if (bufferTimer <= 0f)
            {
                lightInputBuffered = false;
                HeavyInputBuffered = false;
                special1InputBuffered = false;
                special2InputBuffered = false;
            }
        }


        if (transform.position.y < -10.0f)
        {
            hp = 0;
        }

        if (hp <= 0)
        {
            moves.SetBool("Alive", false);
       
        }

        //CheckRecover();

        if (state.ableBodied == false)
        {
            hitboxes.SetActive(false);
          
        }

        if (state.ableBodied == true)
        {
            hitboxes.SetActive(true);
         
        }


        if (chain > 0)
        {
            //StartCoroutine(chainResetTimer());
        }

     

        //rekkas
        if (lightInputBuffered || HeavyInputBuffered && state.atk == false && state.ableBodied && state.airAtk == false)
        {
            if (movement.isGrounded)
            {
                switch(chain)
                {
                    case 0:
                        {
                            if (lightInputBuffered && state.atk == false)
                            {
                            
                                moves.Play("Light");
                                chain = 1;
                                lightInputBuffered = false;
                                
                            }
                             if (HeavyInputBuffered && state.atk == false)
                            {
                                moves.Play("Heavy");
                                chain = 1;
                                HeavyInputBuffered = false;

                            }

                            break;
                        }

                    case 1:
                        {
                            if (state.followup)
                            {
                             
                                moves.Play("Combo 2");
                                chain = 2;
                                HeavyInputBuffered = false;
                                lightInputBuffered = false;
                                break;
                            }
                            
                            break;
                           
                        }

                    case 2:
                        {

                            if (state.followup)
                            {
                               
                                moves.Play("Combo 3");
                                chain = 0;
                                HeavyInputBuffered = false;
                                lightInputBuffered = false;
                            }
                      
                            break;
                           
                        }
                }
            }

            else if ( !movement.isGrounded)
            {
                switch(chain)
                {
         
                        case 0:
                            {
                               if (state.atk == false)
                            {
                                moves.Play("AirAttack1");
                                chain = 1;
                                HeavyInputBuffered = false;
                                lightInputBuffered = false;
                                break;
                            }
                            break;

                          
                          
                            }

                        case 1:
                            {
                            if(state.followup)
                            {
                                moves.Play("AirAttack2");
                                chain = 2;
                                HeavyInputBuffered = false;
                                lightInputBuffered = false;
                            }
                            
                            break;
                            }

                        case 2:
                            {
                            if (lightInputBuffered && state.followup)
                            {
                                
                                moves.Play("Air Attack 3_1");
                                chain = 0;
                               
                                lightInputBuffered = false;

                            }
                            if (HeavyInputBuffered && state.followup)
                            {
                                
                                moves.Play("Air Attack 3_2");
                                HeavyInputBuffered = false;
                                chain = 0;

                            }
                            break;
                        }
                    }
            }
        }

        //specials
        if (special1InputBuffered && state.atk == false && state.ableBodied && state.airAtk == false && movement.isGrounded)
        {
           
            moves.Play("Special 1");

        }

        if (special2InputBuffered && state.atk == false && state.ableBodied && state.airAtk == false && movement.isGrounded)
        {
           
            moves.Play("Special 2");

        }


        AnimatorStateInfo stateInfo = moves.GetCurrentAnimatorStateInfo(0);

        // Get the full path hash of the current state
        int fullPathHash = stateInfo.fullPathHash;

        // You can then use this hash to compare with known states
        // For example, if you have a state named "MyState"
        int forwardKnockback = Animator.StringToHash("Base Layer.ForwardKnockback");
        int spinningKnockback = Animator.StringToHash("Base Layer.RotationKnockback");

        if (fullPathHash == spinningKnockback || fullPathHash == forwardKnockback)
        {
            Debug.Log("Currently in state: MyState");
            canBounce = true;
        }

        else
        {
            canBounce = false;
        }
    }


    

    public void FixedUpdate()
    {
        Vector3 velocity = rb.linearVelocity;

        // Deadzone to avoid jitter on each axis
        float x = Mathf.Abs(velocity.x) < 0.05f ? 0f : velocity.x; // Horizontal (strafe)
        float y = Mathf.Abs(velocity.y) < 0.05f ? 0f : velocity.y; // Vertical (jump/fall)
        float z = Mathf.Abs(velocity.z) < 0.05f ? 0f : velocity.z; // Forward/back

  

    }

    public void spawnProjectile()
    {
        GameObject cross = Instantiate(crossPrefab, crossSpawnPoint.transform.position, Quaternion.identity);
        cross.transform.Rotate(-90, 90, 0);
        Rigidbody crossRb = cross.GetComponent<Rigidbody>();
        if (movement.flipped)
        {
            crossRb.AddForce(new Vector3(-25, 0, 0), ForceMode.Acceleration);
        }
        else
        {
            crossRb.AddForce(new Vector3(25, 0, 0), ForceMode.Acceleration);
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
        if (context.started )
        {
            lightInputBuffered = true;
            bufferTimer = inputBufferTime;
        }

    }

    public void HeavyAttackInput(InputAction.CallbackContext context )
    {
        if (context.started)
        {
            HeavyInputBuffered = true;
            bufferTimer = inputBufferTime;
        }
    }

    public void LauncherAttackInput(InputAction.CallbackContext context)
    {
        if (context.started )
        {

            special1InputBuffered = true;
            bufferTimer = inputBufferTime;

        }
    }

    public void ShootAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            special2InputBuffered = true;
            bufferTimer = inputBufferTime;
        }
    
    }

    public void GrabAttackInput(InputAction.CallbackContext context)
    {
        if (context.started && state.atk == false && state.ableBodied && state.airAtk == false)
        {

            if (movement.isGrounded == true)
            {
                moves.Play("Charge");
            }

        }
    }

    public void SuperAttackInput(InputAction.CallbackContext context)
    {
        if (context.started && state.atk == false && state.ableBodied && state.airAtk == false && meter == 100)
        {

            if (movement.isGrounded == true)
            {
                //meter -= 100;
                moves.Play("Super");
            }

        }
    }

    public void pauseInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {

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


        yield return new WaitForSeconds(5f);
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

    public void GetSlowdown(hitbox collision, AudioClip hitSound, float damage, KnockbackType knockback , HitboxType hitboxType, VFXType vfxType)
    {
        
        //esta função ativa quando um jogador é atingido. Funciona de maneira um pouco diferente de Slowdown()

        if (damage >= hp && !movement.canBlock)
        {


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
                collision.HorizontalKnockback = 2;
                collision.VerticalKnockback = 1;
                moves.SetTrigger("ForwardHurt");
            }

            if (knockback == KnockbackType.Downward)
            {
                moves.SetTrigger("DownwardHurt");
            }
            //ativa o slowmotion chamando a instance do Roundmanager.

            PlaySound(FinishingBlow);
           RoundManager.Instance.SlowdownGame(1);
           
        }

        if (damage < hp && !movement.isGrounded)
        {
            gotHitInAir = true;
            //a ideia aqui é transformar todo move normal do atacante em um move que dá knockback vertical automaticamente se o oponente estiver no ar.
            //porém, há problemas com essa solução que ainda não sei resolver, não está muito consistente.
           
            if (knockback == KnockbackType.Ground)
            {
                Debug.Log("hititng a normal on  the air");
                moves.SetTrigger("UpwardHurt");
                collision.VerticalKnockback = 7;
            }

        }


        if (damage < hp && !movement.canBlock )
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

        if (movement.canBlock)
        {
            damage = 0;
            moves.Play("Block");
            GameObject blockSpawn = Instantiate(block, crossSpawnPoint.transform.position, Quaternion.identity);
            if (movement.flipped == false)
            {
                Vector3 scale = blockSpawn.transform.localScale;
                float rotationY = blockSpawn.transform.eulerAngles.y;
                rotationY = Mathf.Round(rotationY);

                scale.z *= -1;
                blockSpawn.transform.eulerAngles = new Vector3(0, 180, 0);
               
            }
            gotHit = true;
            PlaySound(blockClip);
            collision.VerticalKnockback = 0;
            collision.HorizontalKnockback = 2;
            StartCoroutine(ShakeRoutine(2, collision));
        }

        else
        {

            if (vfxType == VFXType.Explosion)
            {
                GameObject explosionSpawn = Instantiate(VFXexplosion, VFXspawnPoint.transform.position, Quaternion.identity);
            }

            if (vfxType == VFXType.greenExplosion)
            {
                GameObject greenSpawn = Instantiate(greenGlow, VFXspawnPoint.transform.position, Quaternion.identity);
            }

            if (vfxType == VFXType.bigRedExplosion)
            {
                GameObject redSpawn = Instantiate(VFXred, VFXspawnPoint.transform.position, Quaternion.identity);
            }

            if (vfxType == VFXType.megaHit)
            {
                GameObject explosionSpawn = Instantiate(VFXexplosion, VFXspawnPoint.transform.position, Quaternion.identity);
                GameObject greenSpawn = Instantiate(greenGlow, VFXspawnPoint.transform.position, Quaternion.identity);
                GameObject redSpawn = Instantiate(VFXred, VFXspawnPoint.transform.position, Quaternion.identity);
            }

            gotHit = true;
            PlaySound(hitSound);
            hp -= damage;
            //hitboxes.SetActive(false);

            StartCoroutine(ShakeRoutine(2, collision));
        }

    }

    public void bounceWall()
    {
        Debug.Log("zacktivated");
        moves.Play("WallHit");
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
            gotHitInAir = false;

     

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

