using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{

    NavMeshAgent agent;
    public Animator anim;
    public Transform player;
    public Enemy enemy;
    State currentState;
    public playerReacher capsuleObject;
    public bool hasReachedPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        currentState = new State.Idle(this.gameObject, agent, anim, player, enemy, this) ;

    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
        hasReachedPlayer = capsuleObject.hasReachedPlayer;
        currentState.isAbleToAttackPlayer = hasReachedPlayer;
        if (enemy.gotHit)
            currentState.agent.enabled = false;
        
    }



    public void callWaitTimeAttack()
    {
        StartCoroutine(WaitAttackTimer());
        Debug.Log("WAITING for next attack");
    }

    IEnumerator WaitAttackTimer()
    {

        yield return new WaitForSeconds(1f);
        currentState.hasAttacked = false;
        Debug.Log("attacking again");
        currentState = new State.Attack(this.gameObject, agent, anim, player, enemy, this);

    }

    public void callPursueTimer()
    {
        StartCoroutine(WaitToChaseAgain());

    }

    IEnumerator WaitToChaseAgain()
    {
        yield return new WaitForSeconds(1f);
        currentState = new State.Pursue(this.gameObject, agent, anim, player, enemy, this);
    }

}
