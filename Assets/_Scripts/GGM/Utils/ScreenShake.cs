using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    [Header("Lista de Câmeras")]
    public List<CinemachineVirtualCamera> virtualCameras = new List<CinemachineVirtualCamera>();

    [Header("Shake Values")]
    public float amplitude = 0.3f;
    public float frequency = 10f;
    public float shakeTime = 0.2f;

    public void ShakeTeste()
    {
        Shake(amplitude, frequency, shakeTime);
    }

    // método para aplicar shake na câmera ativa
    public void Shake(float amplitude, float frequency, float time)
    {
        var activeCam = GetActiveCamera();
        if (activeCam == null)
        {
            Debug.LogWarning("Nenhuma câmera ativa encontrada pelo Brain!");
            return;
        }

        var perlin = activeCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (perlin == null)
        {
            Debug.LogWarning("Câmera ativa não possui Perlin configurado!");
            return;
        }

        perlin.m_AmplitudeGain = amplitude;
        perlin.m_FrequencyGain = frequency;

        StartCoroutine(ShakeCoroutine(perlin, time));
    }

    private IEnumerator ShakeCoroutine(CinemachineBasicMultiChannelPerlin perlin, float time)
    {
        yield return new WaitForSeconds(time);
        perlin.m_AmplitudeGain = 0f;
        perlin.m_FrequencyGain = 0f;
    }

    private CinemachineVirtualCamera GetActiveCamera()
    {
        var brain = Camera.main.GetComponent<CinemachineBrain>();
        var activeCam = brain.ActiveVirtualCamera;
        if (activeCam is CinemachineVirtualCamera vcam)
        {
            return vcam;
        }
        else if (activeCam is CinemachineStateDrivenCamera sdcam)
        {
            return sdcam.LiveChild as CinemachineVirtualCamera;
        }
        return null;

    }
}
