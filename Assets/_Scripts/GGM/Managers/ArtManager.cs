using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.Singleton;

public class ArtManager : Singleton<ArtManager>
{
    public enum ArtType
    {
        ArtType_01,
        ArtType_02,
        ArtType_03
    }

    public List<ArtSetup> artSetups;  

    public ArtSetup GetArtSetupByType(ArtType artType)
    {
        return artSetups.Find(i => i.artType == artType);
    }  
}

[System.Serializable]
public class ArtSetup
{
    public ArtManager.ArtType artType;
    public GameObject artPrefab;
    
}