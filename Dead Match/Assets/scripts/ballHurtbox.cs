using UnityEngine;

public class ballHurtbox : MonoBehaviour
{

    public ball ball;
    private bool isColliding;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ball = transform.parent.gameObject.GetComponent<ball>();
    }

   
}
