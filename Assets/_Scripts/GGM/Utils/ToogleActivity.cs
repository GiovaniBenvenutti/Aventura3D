using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToogleActivity : MonoBehaviour
{
    public void ToggleGameObjectActivity(GameObject target)
    {
        if (target != null) target.SetActive(!target.activeSelf);
    }
}
