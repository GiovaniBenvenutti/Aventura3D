using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.Singleton;

public class SfxPool : Singleton<SfxPool>
{
    private List<AudioSource> _audioSourceList;

    public int poolSize = 10;

    private int _index = 0;

    void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        _audioSourceList = new List<AudioSource>();

        for(int i = 0; i < poolSize; i++)
        {
            CreateAudioSourceItem();        
        }

    }

    private void CreateAudioSourceItem()
    {
        GameObject go = new GameObject("SFX_Pool");
        go.transform.SetParent(gameObject.transform);
        _audioSourceList.Add(go.AddComponent<AudioSource>());        
    }

    public void Play(SfxType sfxType)
    {
        if(sfxType == SfxType.NONE) return;
        var sfx = SoundManager.Instance.GetSfxByType(sfxType);
        _audioSourceList[_index].clip = sfx.audioClip;
        _audioSourceList[_index].Play();

        _index++;
        if(_index >=_audioSourceList.Count) _index = 0;
    }



    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
