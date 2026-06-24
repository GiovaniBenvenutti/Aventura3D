using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartCheck : MonoBehaviour
{
    public string TagToCheck = "Player";

    public GameObject bossCamera;

    public Color gizmosColor = Color.yellow;

    private void Awake() 
    {
        TurnBossCameraOff();    
    }

    private void OnTriggerEnter (Collider other)
    {
        if(other.transform.tag == TagToCheck)
        {
            TurnBossCameraOn();
        }
    }

    private void TurnBossCameraOn()
    {
        bossCamera.SetActive(true);
    }

    public void TurnBossCameraOff()
    {
        bossCamera.SetActive(false);
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawSphere(transform.position, transform.localScale.x);    
    }
}
