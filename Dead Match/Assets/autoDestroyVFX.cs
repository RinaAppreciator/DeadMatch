using UnityEngine;

public class autoDestroyVFX : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        float duration = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, duration);
    }
}
