using UnityEngine;
using Unity.Collections;

public class hitbox : MonoBehaviour
{
    public GameObject suicide;
    public bool hiti;

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hiti = true;
            suicide.SetActive(false);

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
