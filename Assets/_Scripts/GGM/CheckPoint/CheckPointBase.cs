using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Material lightMaterial;

    public int materialNumber = 0;
    public SphereCollider sphereCollider;

    public int key = 01;

    private string checkPointKey = "CheckPoint";

    private bool checkPointActive = false;

    void Start()
    {
        Material[] materials = meshRenderer.materials;
        lightMaterial = materials[materialNumber];


        checkPointKey = "CheckPoint_" + key.ToString();

        if(lightMaterial != null)
        {
            lightMaterial.SetColor("_Color", Color.red);
            lightMaterial.SetColor("_EmissionColor", Color.red);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!checkPointActive && other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint ativado");
            CheckCheckPint();
        }
    }

    private void CheckCheckPint()
    {
        SaveCheckPoint();
        TurnItOn();
    }

    [NaughtyAttributes.Button]
    private void TurnItOn()
    {
        checkPointActive = true;
        
        if(lightMaterial != null)
        {
            lightMaterial.SetColor("_Color", Color.green);
            lightMaterial.SetColor("_EmissionColor", Color.green);
        }
    }

    [NaughtyAttributes.Button]
    private void TurnItOff()
    {
        checkPointActive = false;
        
        if(lightMaterial != null)
        {
            lightMaterial.SetColor("_Color", Color.red);
            lightMaterial.SetColor("_EmissionColor", Color.red);
        }
    }

    private void SaveCheckPoint()
    {
        //if (PlayerPrefs.GetInt(checkPointKey, 0) >= key) return; // Já foi salvo um checkpoint igual ou superior a este    
        //PlayerPrefs.SetFloat(checkPointKey, key);

        CheckPointManager.Instance.saveCheckPoint(key);
    }
}
