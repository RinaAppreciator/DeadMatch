using System.Collections;
using Unity.Cinemachine;
using UnityEngine;




public class cameraManager : MonoBehaviour
{

    private CinemachineCamera virtualCam;
    private CinemachineBasicMultiChannelPerlin perlinNoise;

    private void Awake()
    {
        virtualCam = GetComponent<CinemachineCamera>();
        perlinNoise = virtualCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        ResetAmplitude();
    }

    public void Shake(float intensity, float shakeTime)
    {
        perlinNoise.AmplitudeGain = intensity;
        StartCoroutine(WaitTime(shakeTime));

    }

    public void ResetAmplitude()
    {
        perlinNoise.AmplitudeGain = 0;

    }

    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        //Reset amplitude
        ResetAmplitude();
    }

}
