using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.Singleton;

public class MainManager : Singleton<MainManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
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
