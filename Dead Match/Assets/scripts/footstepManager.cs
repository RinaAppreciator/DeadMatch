using System.Collections.Generic;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{

    public AudioSource audioSource;

    public AudioClip concrete;

    RaycastHit hit;
    public Transform RayStart;
    public float range;
    public LayerMask layerMask;







    public void PlayStep(AudioClip audioClip)
    {
        audioSource.pitch = Random.Range(0.8f, 1.0f);
        audioSource.PlayOneShot(audioClip);
    }


    public void Footstep()
    {
        if (Physics.Raycast(RayStart.position, RayStart.transform.up * -1, out hit, range, layerMask ))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                PlayStep(concrete);
            }
        }
    }
    //private void SelectStepList()
    //{
    //    switch (surface)
    //    {
    //        case Surface.grass:
    //            currentList = grassSteps;
    //            break;
    //        case Surface.water:
    //            currentList = waterSteps;
    //            break;
    //        case Surface.cave:
    //            currentList = caveSteps;
    //            break;
    //    }
    //}

    
    

}
