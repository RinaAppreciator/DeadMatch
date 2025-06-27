using System.Collections;
using Unity.Cinemachine;
using UnityEngine;




public class cameraManager : MonoBehaviour
{

    private CinemachineCamera virtualCam;
    private CinemachineBasicMultiChannelPerlin perlinNoise;
    public CinemachineTargetGroup targetGroup;
    public GameObject player1;
    public GameObject player2;

    private void Awake()
    {
        virtualCam = GetComponent<CinemachineCamera>();
        perlinNoise = virtualCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
       
       
    }

    public void Start()
    {

        player1 = GameObject.Find("Player #1");
        player2 = GameObject.Find("Player #2");
        AddTargets();
    }

    public void Shake(float intensity, float shakeTime, float frequency)
    {
        perlinNoise.AmplitudeGain = intensity;
        perlinNoise.FrequencyGain = frequency;
        StartCoroutine(WaitTime(shakeTime));

    }

    public void ResetAmplitude()
    {
        perlinNoise.AmplitudeGain = 0;
        perlinNoise.FrequencyGain = 0;
    }

    public void AddTargets()
    {
        targetGroup.AddMember(player1.gameObject.transform, 1, 1);
        targetGroup.AddMember(player2.gameObject.transform, 1, 1);
    }

    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        //Reset amplitude
        ResetAmplitude();
    }

}
