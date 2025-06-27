using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Flip : MonoBehaviour
{
    public GameObject player;
    public bool playerGrounded;
    public bool gotHitInAir;
    public float side;
    private Move moveScript;
    private fight fightScript;
    public bool mirrorReset = false;
    public bool mirrorActivate = false;
    public bool playerFlipped = false;
    private Vector3 originalScale;
   




    public void Awake()
    {
        if (player != null)
        {
            moveScript = player.GetComponent<Move>();
            fightScript = player.GetComponent<fight>();
            originalScale = player.transform.localScale;
        }
    }

    public void FixedUpdate()
    {



        if (moveScript.isGrounded == true)
        {
            playerGrounded = true;

       
        }
        else
        {
            playerGrounded = false;
            
        }

        if (fightScript.gotHitInAir == true)
        {
            gotHitInAir = true;
        }

        else
        {
            gotHitInAir = false;
        }

       
    }


    public void OnTriggerStay(Collider collision)
    {
        if (!playerGrounded)
        {
            Debug.Log("i am not on the ground so i cant flip");
        }

        if (collision.CompareTag("Respawn") && playerGrounded || gotHitInAir)
        {
            Debug.Log("Hitting a player");

            Vector3 scale = player.transform.localScale;
            float rotationY = player.transform.eulerAngles.y;
            rotationY = Mathf.Round(rotationY);

            switch (moveScript.characterID)
            {
                case 0:
                    {
                        if (Mathf.Approximately(rotationY, 180f) && scale.z > 0)
                        {
                            // Flip left-side player (180°, scale.z = 1) → 0°, scale.z = -1
                            Debug.Log("flipped to the right ");
                            scale.z *= -1;
                            player.transform.eulerAngles = new Vector3(0, 0, 0);
                        }
                        else if (Mathf.Approximately(rotationY, 0f) && scale.z < 0)
                        {
                            Debug.Log("flipped to the left ");
                            // Flip right-side player (0°, scale.z = -1) → 180°, scale.z = 1
                            scale.z *= -1;
                            player.transform.eulerAngles = new Vector3(0, 180, 0);
                        }
                        else
                        {
                            Debug.Log("i am not eligible for flipping, this is my rotation" + rotationY);
                        }

                        player.transform.localScale = scale;

                        break;
                    }

                case 1:
                    {
                        if (Mathf.Approximately(rotationY, 270f) && scale.z < 0) // if !flipped
                        {
                            // Flip left-side player (180°, scale.z = 1) → 0°, scale.z = -1
                            Debug.Log("flipped to the left ");
                            scale.z *= -1;
                            player.transform.eulerAngles = new Vector3(0, -90, 0);
                        }
                        else if (Mathf.Approximately(rotationY, 270f) && scale.z > 0) //if flipped
                        {
                            Debug.Log("flipped to the right ");
                            // Flip right-side player (0°, scale.z = -1) → 180°, scale.z = 1
                            scale.z *= -1;
                            player.transform.eulerAngles = new Vector3(0, -90, 0);
                        }
                        else
                        {
                            Debug.Log("i am not eligible for flipping, this is my rotation" + rotationY);
                        }







                        Debug.Log("i am tetsu hello");
                        player.transform.localScale = scale;
                        break;

                    }



                    player.transform.localScale = scale;
            }
        }
    }

    //public void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.CompareTag("Respawn") && playerGrounded)
    //    {
    //        Debug.Log("Hitting a player");

    //        Vector3 scale = player.transform.localScale;
    //        float rotationY = player.transform.eulerAngles.y;

    //        if (Mathf.Approximately(rotationY, 180f) && scale.z > 0)
    //        {
    //            // Flip left-side player (180°, scale.z = 1) → 0°, scale.z = -1
    //            Debug.Log("flipped to the right ");
    //            scale.z *= -1;
    //            player.transform.eulerAngles = new Vector3(0, 0, 0);
    //        }
    //        else if (Mathf.Approximately(rotationY, 0f) && scale.z < 0)
    //        {
    //            Debug.Log("flipped to the left ");
    //            // Flip right-side player (0°, scale.z = -1) → 180°, scale.z = 1
    //            scale.z *= -1;
    //            player.transform.eulerAngles = new Vector3(0, 180, 0);
    //        }

    //        player.transform.localScale = scale;
    //    }
    //}


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
