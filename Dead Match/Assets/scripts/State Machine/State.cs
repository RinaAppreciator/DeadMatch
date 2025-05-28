using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class State 
{

    public enum STATE
    {
        IDLE, PURSUE, ATTACK, SLEEP, PATROL
    };

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    public Transform player;
    protected State nextState;
    public NavMeshAgent agent;
    public Enemy enemy;
    public AI enemyController;

    public bool hasAttacked = false;



    public bool isAbleToAttackPlayer;

    public State(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, Enemy _enemy, AI _enemyController)
    {
        this.npc = _npc;
        this.agent = _agent;
        this.anim = _anim;
        this.player = _player;
        this.enemy = _enemy;
        this.enemyController = _enemyController;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
         
        }
        return this;
    }

   // public bool canSeePlayer()
    //{
       // Vector3 direction = player.position - npc.transform.position;

       // float angle = Vector3.Angle(direction, npc.transform.position);

       // if (direction.magnitude < visDist && angle < visAngle)
       // {
        //    return true;
       // }
       // return false;
    //}


    //public bool canAttackPlayer()
    //{
       // Vector3 direction = player.position - npc.transform.position;

       // if (direction.magnitude < attackDist)
       // {
        //    return true;
       // }

       // return false;
    //}

    public class Idle : State
    {
        public Idle(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, Enemy _enemy, AI _enemyController) : base(_npc, _agent, _anim, _player, _enemy, _enemyController)
        {
            name = STATE.IDLE;
        }

        public override void Enter()
        {
            //anim.SetTrigger("isIdle");
            //Debug.Log("entered idle");
            base.Enter();
        }


        public override void Update()
        {
            //Debug.Log("Idling");
            if (player != null)
            {
                Debug.Log("starting to chase the player ");
                nextState = new Pursue(npc, agent, anim, player,enemy, enemyController);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            anim.ResetTrigger("isIdle");
            //Debug.Log("exiting idle");
            base.Exit();

        }
    }


    public class Pursue : State
    {
        public Pursue(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, Enemy _enemy, AI _enemyController) : base(_npc, _agent, _anim, _player, _enemy, _enemyController)
        {
            name = STATE.PURSUE;
            agent.speed = 5;
            agent.isStopped = false;
        }

        public override void Enter()
        {

            if (!enemy.gotHit)
            {
                anim.SetBool("isRunning", true);
                Debug.Log("entered pursue state");
                agent.ResetPath();
                base.Enter();
            }
        }


        public override void Update()
        {
            if (!enemy.gotHit)
            {
                agent.SetDestination(player.position);

            }

            Debug.Log($"is able to attack player: {isAbleToAttackPlayer}"); // Debugging
            Debug.Log($"is the agent stopped: {agent.isStopped}"); // Debugging
            Debug.Log($"agent speed: {agent.speed}"); // Debugging

            if (agent.hasPath)
            {
                Debug.Log("chasing the player");
                if (agent.remainingDistance < 1)

                    Debug.Log("player reached according to nav agent");
                    

                if (isAbleToAttackPlayer == true && !enemy.gotHit)
                {
                    Debug.Log("reached player");
                    nextState = new Attack(npc, agent, anim, player, enemy, enemyController);
                    stage = EVENT.EXIT;
                }

                if (isAbleToAttackPlayer == false && !enemy.gotHit)
                {
                    Debug.Log("player got out of reach");
                    nextState = new Pursue(npc, agent, anim, player, enemy, enemyController);
                    stage = EVENT.EXIT;
                }

           

            }

        }

        public override void Exit()
        {
            anim.SetBool("isRunning", false);
            Debug.Log("stopped running");
            base.Exit();

        }

    }

    public class Attack : State
    {
        float rotationSpeed = 1.0f;
        public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, Enemy _enemy, AI _enemyController) : base(_npc, _agent, _anim, _player, _enemy, _enemyController)
        {
            name = STATE.ATTACK;
        }

        public override void Enter()
        {
            if (!hasAttacked && !enemy.gotHit)
            {
                anim.SetTrigger("EnemyAttack");
                agent.isStopped = true;
                hasAttacked = true;
                Debug.Log("entered attack state");
                base.Enter();
            }

            if (hasAttacked)
                return;

        }


        public override void Update()
        {
            if (!enemy.gotHit)
            {
                Vector3 direction = player.position - npc.transform.position;

                float angle = Vector3.Angle(direction, npc.transform.position);

                direction.y = 0;

                npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

            }

            if (isAbleToAttackPlayer)
            {
                Debug.Log("Wait a little bit, then attack again");
                WaitForNextAttack();
                //stage = EVENT.EXIT;
     
            }

            if (!isAbleToAttackPlayer)
            {
                Debug.Log("not able to attack player. Chasing");
                anim.ResetTrigger("EnemyAttack");
                WaitForNextPursue();
                stage = EVENT.EXIT;
                //WaitForNextAttack();
            }

        }

        public override void Exit()
        {
            //anim.SetBool("isRunning", false);
            anim.ResetTrigger("EnemyAttack");
            Debug.Log("not attacking anymore");
            agent.isStopped = false;
            base.Exit();

        }




        }

    public void WaitForNextAttack()
    {
        Debug.Log("trying to wait");
        enemyController.callWaitTimeAttack();
        
    }

    public void WaitForNextPursue()
    {
        Debug.Log("trying to wait");
        enemyController.callPursueTimer();
        
    }



}
