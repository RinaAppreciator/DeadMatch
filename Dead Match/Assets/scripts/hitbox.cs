using UnityEngine;

public class hitbox : MonoBehaviour
{
    
    public bool hiti;

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hiti = true;
        }

    }
     public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hiti = false;
        }

    }
}
