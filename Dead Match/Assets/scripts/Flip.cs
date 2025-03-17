using UnityEngine;

public class Flip : MonoBehaviour
{
    public GameObject player;
    public float side;

    public void OnTriggerEnter(Collider collision)
    {       
        if (collision.CompareTag("Player"))
        {
        player.transform.Rotate(0, 180, 0);
        side +=1;

        if(side==2)
        {
            side = 0;

        }



        }
    }
}
