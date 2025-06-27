using UnityEngine;
using UnityEngine.XR;

public class boundaryManager : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;

    public GameObject wallCube1;
    public GameObject wallCube2;

    public Transform wallPoint1;
    public Transform wallPoint2;

    public Rigidbody wallRb1;
    public Rigidbody wallRb2;

    public BoxCollider wallRbCollider;
    public BoxCollider wallRbCollider2;

    public PlayerSpawner spawner;

    private bool hardened;



    public Vector3 distanceBetweenPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hardened = false;
        player1 = GameObject.Find("Player #1");
        player2 = GameObject.Find("Player #2");
        wallPoint1 = player1.transform.Find("WallPoint");
        wallPoint2 = player2.transform.Find("WallPoint");
    }

    // Update is called once per frame
    void Update()
    {
        

     
        distanceBetweenPoints = player1.transform.position - player2.transform.position;

        if (hardened == false)
        {
            wallCube1.transform.position = wallPoint1.transform.position;
            wallCube2.transform.position = wallPoint2.transform.position;
        }

        if ( distanceBetweenPoints.x <= -4.5  && !hardened)
        {
            harden();
        }

        if (distanceBetweenPoints.x >= 4.5 && !hardened)
        {
            
            harden();
        }

        else if (hardened && distanceBetweenPoints.x >= -4.5  )
        {
            soften();
        }
    }

    public void harden()
    {
        Debug.Log("harden");
        hardened = true;
        
        wallRb1.constraints = RigidbodyConstraints.FreezeAll;
        wallRb2.constraints = RigidbodyConstraints.FreezeAll;
        wallRbCollider.enabled = true;
        wallRbCollider2.enabled = true;
    }

    public void soften()
    {
        Debug.Log("soften");
        hardened = false;
   
        wallRb1.constraints = RigidbodyConstraints.None;
        wallRb2.constraints = RigidbodyConstraints.None;
        wallRbCollider.enabled = false;
        wallRbCollider2.enabled = false;
   
   
    }
}
