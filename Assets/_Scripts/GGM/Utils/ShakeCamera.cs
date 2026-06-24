using System.Collections;
using System.Collections.Generic;
using GGM.Singleton;
using UnityEngine;
using Cinemachine;
using System;

public class ShakeCamera : Singleton<ShakeCamera>
{
    public CinemachineVirtualCamera virtualCamera;

    public CinemachineBasicMultiChannelPerlin perlin;

    [Header("Shake Values")]
    public float amplitude = 0.3f;
    public float frequecy = 10f;
    public float shakeTime = 0.2f;

    private void OnValidate() 
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        perlin =  virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Start()
    {
        if (perlin == null) Debug.Log("Camera não possui Perlin configurado");
    }

    #region Debug

        [NaughtyAttributes.Button]
        public void ShakeTeste()
        {
            Shake(amplitude, frequecy, shakeTime);
        }

    #endregion

    public void Shake(float amplitude, float frequency, float time)
    {
        perlin.m_AmplitudeGain = amplitude;
        perlin.m_FrequencyGain = frequency;

        shakeTime = time;
        StartCoroutine(ShakeCorroutine());

    }

    IEnumerator ShakeCorroutine()
    {
        yield return new WaitForSeconds(shakeTime);

        perlin.m_AmplitudeGain = 0f;
        perlin.m_FrequencyGain = 0f;
    }


}
