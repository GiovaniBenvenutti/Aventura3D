using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using NaughtyAttributes;

namespace GGM.Cloth
{
    public class ClothChanger : MonoBehaviour
    {
        public SkinnedMeshRenderer[] mesh;
        public Texture2D texture;
        public string shaderIdName = "_EmissionMap";

        private Texture2D _defaultTexture;
        

        private void Start()
        {
            _defaultTexture = ClothManager.Instance.GetClothByType(ClothType.ORIGINAL).text;
        }

     //   [NaughtyAttributes.Button("Change Texture")]
        private void ChangeTexture()
        {
            foreach (var mesh in mesh)
            {
                mesh.sharedMaterials[0].SetTexture(shaderIdName, texture);
                //mesh.material.mainTexture = texture;
            }
        }
        public void ChangeTexture(ClothSetup setup)
        {
            SaveManager.Instance.SavePlayerCloth(setup.clothType);

            foreach (var mesh in mesh)
            {
                mesh.sharedMaterials[0].SetTexture(shaderIdName, setup.text);
            }
        }

        public void ResetTexture()
        {
            foreach (var mesh in mesh)
            {
                mesh.sharedMaterials[0].SetTexture(shaderIdName, _defaultTexture);
            }
        }

    }

}
