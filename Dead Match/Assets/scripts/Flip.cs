using UnityEditor;
using UnityEngine;

public class Flip : MonoBehaviour
{
    public GameObject player;
    public bool playerGrounded;
    public float side;
    private Move moveScript;
    public bool mirrorReset = false;
    public bool mirrorActivate = false;
    public bool playerFlipped = false;
    private Vector3 originalScale;
   




    public void Awake()
    {
        if (player != null)
        {
            moveScript = player.GetComponent<Move>();
            originalScale = player.transform.localScale;
        }
    }

    public void FixedUpdate()
    {



        if (moveScript.isGrounded == true)
        {
            playerGrounded = true;

            //if (mirrorReset == true && playerFlipped == true)
            //{
            //    FlipPlayer(false);
            //    mirrorReset = false;
                
            //}
            ////Debug.Log("on the ground");

            //if (mirrorActivate == true && playerFlipped == false)
            //{
            //    FlipPlayer(true);
            //    mirrorActivate = false;
               

            //}

        }
        else
        {
            playerGrounded = false;
            //Debug.Log("on the air");
        }

       
    }


    public void OnTriggerStay(Collider collision)
    {
        
        if (collision.CompareTag("Respawn") && playerGrounded)
        {
            Debug.Log("hitting a player");


            Vector3 scale = player.transform.localScale;
            scale.z *= -1;
            player.transform.localScale = scale;

            //if (playerGrounded == true)
            //{
            //    playerFlipped = true;
            //    FlipPlayer(true);
            //}

            //else
            //{

            //    mirrorActivate = true;

            //}


        }
    }


    //public void OnTriggerExit(Collider collision)
    //{
    //    // não funcionará, pois se o jogador sair e não estiver grounded, ele não voltará ao normal
    //    if (collision.CompareTag("Player") )
    //    {
    //        Debug.Log("not hitting a player anymore");
            

    //        if (playerGrounded == true)
    //        {
    //            FlipPlayer(false);
    //        }

    //        else
    //        {
    //            mirrorReset = true;
    //        }
         



    //    }

    //}




    private void FlipPlayer(bool flip)
    {
      

        if (flip && !playerFlipped)
        {
            Debug.Log("flipping the player");
            player.transform.localScale = new Vector3(
                originalScale.x,
                originalScale.y,
                -originalScale.z
            );
            playerFlipped = true;
        }
        if (!flip && playerFlipped)
        {
            Debug.Log("Resetting the flip");
            player.transform.localScale = originalScale;
            playerFlipped = false;
        }
    }
}
