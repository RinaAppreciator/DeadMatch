using UnityEngine;

public class grabHitbox : MonoBehaviour
{
    public GameObject player;
    public fight playerObject;
    GameObject enemyObject;
    Enemy enemy;
    public Animator anim;
    public bool hiti;
    public LayerMask layerMask;

    public void Start()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (layerMask == (layerMask | (1 << collision.transform.gameObject.layer)))
        {
            Debug.Log("found hitbox");
            Debug.Log(collision.name);
            grabHitbox h = this;
            enemyObject = collision.transform.root.gameObject;
            enemy = enemyObject.GetComponent<Enemy>();


            Hurt hurt = collision.GetComponent<Hurt>();
            ballHurtbox ballhurt = collision.GetComponent<ballHurtbox>();

            if (hurt != null)
            {
                Debug.Log("grabbing");
                OnHit(hurt, h);
            }
            if (ballhurt != null)
            {
                Debug.Log("grabbing ball");
                OnBallHit(ballhurt, h);
            }

        }
    }

    void RestoreSpeed()
    {
        anim.speed = 1f;
    }

    protected virtual void OnHit(Hurt hurt, grabHitbox h)
    {
        hurt.enemy.GetGrabbed(h);
        anim.speed = 0.6f; // Reduce animation speed (0.2x slower)
        Invoke("RestoreSpeed", 1f); // Restore normal speed after 2 seconds
        playerObject.isGrabbing = true;
        playerObject.grabbedEnemy = enemy;
    }

    protected virtual void OnBallHit(ballHurtbox ballhurt, grabHitbox h)
    {
        //ballhurt.ball.GetGrabbed(h);
        anim.speed = 0.6f; // Reduce animation speed (0.2x slower)
        Invoke("RestoreSpeed", 1f); // Restore normal speed after 2 seconds
    }



}

