using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.Singleton;

public class ColorManager : Singleton<ColorManager>
{
    public List<Material> materials;
    public List<ColorSetup> collorSetups;

    public void ChangeColorByType(ArtManager.ArtType artType)
    {
        ColorSetup colorSetup = collorSetups.Find(i => i.artType == artType);
        if(colorSetup != null)
        {
            for(int i = 0; i < materials.Count; i++)
            {
            //    materials[i].SetColor("_Color", colorSetup.colors[i]);  // DEVE EXISTIR A MESMA QUANTIDADE DE CORES E MATERIAIS, CASO CONTRÁRIO VAI DAR ERRO
                materials[i].SetColor("_Color", colorSetup.colors[Random.Range(0, colorSetup.colors.Count)]);  // SE QUISER CORES ALEATÓRIAS, DESCOMENTE ESSA LINHA E COMENTE A LINHA ACIMA
            }
        }
        
    }
}

[System.Serializable]
public class ColorSetup
{
    public ArtManager.ArtType artType;
    public List<Color> colors;
    //public Material material;
}
