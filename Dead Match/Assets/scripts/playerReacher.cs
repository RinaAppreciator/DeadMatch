using UnityEngine;

public class playerReacher : MonoBehaviour
{

    public CapsuleCollider playerReached;
    public bool hasReachedPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            Debug.Log("found player");
            hasReachedPlayer = true;
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            Debug.Log("lost player");
            hasReachedPlayer = false;
        }
    }


}
