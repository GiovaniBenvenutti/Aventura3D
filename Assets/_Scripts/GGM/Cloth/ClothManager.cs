using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.Singleton;

namespace GGM.Cloth
{
    public enum ClothType
    {
        ORIGINAL,
        SPEED,
        STRONG
    }
    public class ClothManager : Singleton<ClothManager>
    {
        public List<ClothSetup> clothSetups;

        public ClothSetup GetClothByType(ClothType clothType)
        {
            return clothSetups.Find(i => i.clothType == clothType);
        }
    }

    [System.Serializable]
    public class ClothSetup
    {
        public ClothType clothType;
        //public SkinnedMeshRenderer[] mesh;
        public Texture2D text;
        //public string shaderIdName = "_EmissionMap";
    }
}

