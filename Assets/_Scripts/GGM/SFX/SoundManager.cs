//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.Singleton;

public class SoundManager : Singleton<SoundManager>
{
    public List<MusicSetup> musicSetups;
    public List<SfxSetup> sfxSetups;
    public AudioSource musicSource;

    public void PlayMusicByType(MusicType musicType)
    {
        var music = GetMusicByType(musicType);
        musicSource.clip = music.audioClip;
        musicSource.Play();
    }

    public MusicSetup GetMusicByType(MusicType musicType)
    {
        return musicSetups.Find(i => i.musicType == musicType);    
    }

    public SfxSetup GetSfxByType(SfxType sfxType)
    {
        return sfxSetups.Find(i => i.sfxType == sfxType);    
    }
}

public enum MusicType
{
    NONE,
    TYPE01,
    TYPE02, 
    TYPE03
}

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;
    
}

public enum SfxType
{
    NONE,
    TYPE01,
    TYPE02, 
    TYPE03
}

[System.Serializable]
public class SfxSetup
{
    public SfxType sfxType;
    public AudioClip audioClip;

}
