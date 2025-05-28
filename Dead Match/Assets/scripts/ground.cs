using System.Collections.Generic;
using UnityEngine;

public class ground : MonoBehaviour
{

    public float impactThreshold = 10f;  // Minimum velocity to trigger lift
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
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(touchingObjects.Count);
    }

    public void screenShake(float amount, float time)
    {
        cameraManager.Shake(amount, time);
    }

    public void shakeGround(float amount, float time,  float force)
    {
        playSound(groundExplosion);
        screenShake(amount, time);
        LiftObjects(force);
    }

    public void LiftObjects(float liftForce)
    {

        // Apply upward force to all objects in contact with the ground
        foreach (Rigidbody rb in touchingObjects)
        {
            if (rb.transform.root.gameObject.name == "Esther")
            {
                Debug.Log("player is here");
            }

            else
            {
                rb.AddForce(Vector3.up * liftForce, ForceMode.Impulse);
            }

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        if (rb != null && collision.contacts.Length > 0)
        {
            touchingObjects.Add(rb);
            //CheckImpact(rb);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        if (rb != null )
        {
            touchingObjects.Remove(rb);
        }

    }

    public void playSound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }

    void CheckImpact(Rigidbody rb)
    {
        float verticalSpeed = Mathf.Abs(rb.linearVelocity.y);

        //Debug.Log(verticalSpeed);

    
        if (verticalSpeed >= impactThreshold)
          {
            Debug.Log("heavy");
            shakeGround(2,0.1f,2);
          }
       
    }
}




