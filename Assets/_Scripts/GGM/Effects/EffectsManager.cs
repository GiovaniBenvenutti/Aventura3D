using System.Collections;
using System.Collections.Generic;
using GGM.Singleton;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class EffectsManager : Singleton<EffectsManager>
{
    public PostProcessVolume processVolume;
    [SerializeField] private Vignette _vignette;

    public float duration = 1f;
    public float intensity = 0.35f;
    public float originalIntensity = 0.35f;


    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        StartCoroutine(FlashColorVignette());
    }

    IEnumerator FlashColorVignette()
    {
        Vignette tmp;

        if (processVolume.profile.TryGetSettings<Vignette>(out tmp))
        {
            _vignette = tmp;
        }

        ColorParameter c = new ColorParameter();

        float time = 0;
        while(time < duration)
        {
            c.value = Color.Lerp(Color.black, Color.red, time / duration);
            _vignette.color.Override(c);
            _vignette.intensity.Override(intensity);

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        time = 0;
        while(time < duration)
        {
            c.value = Color.Lerp(Color.red, Color.black, time / duration);
            _vignette.color.Override(c);
            _vignette.intensity.Override(originalIntensity);

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _vignette.intensity.Override(originalIntensity);

        
    }
}
