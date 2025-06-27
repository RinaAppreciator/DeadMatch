using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{

    public float impactThreshold;
    public float liftForce;        // Upward force applied to objects
    public float shakeThreshold = 15f;   // Speed threshold for a heavy shake
    public float shakeMagnitude = 0.5f;
    public AudioSource audioSource;

    public AudioClip groundExplosion;

    [SerializeField] private cameraManager cameraManager;


    private HashSet<Rigidbody> touchingObjects = new HashSet<Rigidbody>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;

        fight playerScript = collision.gameObject.GetComponent<fight>();

        if (playerScript != null)
        {
            if (playerScript.canBounce)
            {
                playSound(groundExplosion);
                playerScript.bounceWall();
                Debug.Log("heavy");
                shakeGround(3, 0.5f, 3);
            }
        }

        else
        {
            Debug.Log("Player wasnt found");
        }


        //if (rb != null && collision.contacts.Length > 0)
        //{           
        //    CheckImpact(rb, playerScript);
        //}
    }

    void CheckImpact(Rigidbody rb, fight playerScript)
    {
     

        if (playerScript != null)
        {
            if (playerScript.canBounce)
            {
                playSound(groundExplosion);
                playerScript.bounceWall();
                Debug.Log("heavy");
                shakeGround(3, 0.5f, 3);
            }
        }

        else
        {
            Debug.Log("Player wasnt found");
        }

        

    }


    public void screenShake(float amount, float time, float frequency)
    {
        cameraManager.Shake(amount, time, frequency);
    }

    public void shakeGround(float amount, float time, float frequency)
    {
        //playSound(groundExplosion);
        screenShake(amount, time, frequency);
        //LiftObjects(force);
    }

    public void playSound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }

}
