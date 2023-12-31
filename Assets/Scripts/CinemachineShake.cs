using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{

public static CinemachineShake Instance { get; private set;}        
private CinemachineVirtualCamera cinemachineVirtualCamera;
private float shakeTimer;
private float shakeTimerTotal;
private float startingIntensity;

private void Awake()
{
    Instance = this;
    cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

}

public void ShakeCamera(float intensity, float time)
{
    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
    shakeTimer = time;
    shakeTimerTotal = time;    
    startingIntensity = intensity;
  

}

private void Update()
{
    shakeTimer -= Time.deltaTime;
    if(shakeTimer <= 0f)
    {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;//Mathf.Lerp(startingIntensity, 0f, shakeTimer / shakeTimerTotal);
    }
}
}
