using UnityEngine;

public class ball : hitbox
{

    bool gotHit;
    public Rigidbody body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void GetHit(hitbox collision)
    {
        GameObject playerObject = collision.transform.root.gameObject;

        fight player = playerObject.GetComponent<fight>();
        hitbox hitBoxObject = collision.GetComponent<hitbox>();

        gotHit = true;

        if (gotHit == true)
        {

            Vector3 directionAwayFromAttacker = (transform.position - player.transform.position);

            // Apply knockback in that direction
            Vector3 knockbackDirection = (directionAwayFromAttacker * hitBoxObject.HorizontalKnockback) + (Vector3.up * hitBoxObject.VerticalKnockback);

            Debug.Log($"Applied Knockback: {knockbackDirection}"); // Debugging

            body.linearVelocity = Vector3.zero;

            body.linearVelocity = knockbackDirection;
        }

    }
}
