using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace GGM.Cloth
{
    public class ClothChanger : MonoBehaviour
    {
        public SkinnedMeshRenderer[] mesh;
        public Texture2D texture;
        public string shaderIdName = "_EmissionMap";

        private Texture2D _defaultTexture;

        private void Awake()
        {
            _defaultTexture = mesh[0].sharedMaterials[0].GetTexture(shaderIdName) as Texture2D;
        }

        [NaughtyAttributes.Button("Change Texture")]
        private void ChangeTexture()
        {
            foreach (var mesh in mesh)
            {
                mesh.materials[0].SetTexture(shaderIdName, texture);
                //mesh.material.mainTexture = texture;
            }
        }
        public void ChangeTexture(ClothSetup setup)
        {
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
