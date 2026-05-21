using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAbilityBase : MonoBehaviour
{
    protected Player player;

    protected Inputs inputs;

    private void OnValidate()
    {
        if (player == null) player = GetComponent<Player>();
    }

    private void Start()
    {
        inputs = new Inputs();
        inputs.Enable();

        Init();
        OnValidate();
        RegisterListeners();        
    }

    private void OnEnable()
    {
        if (inputs != null) inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    protected virtual void Init()
    {
        // aqui é pra inicializar variáveis, referências, etc
    }

    protected virtual void RegisterListeners()
    {
        // aqui é pra registrar os listeners, como por exemplo:
        // player.fsmPlayer.OnStateChanged += FsmPlayer_OnStateChanged;
    }

    protected virtual void RemoveListeners()
    {
        // aqui é pra desregistrar os listeners, como por exemplo:
        // player.fsmPlayer.OnStateChanged -= FsmPlayer_OnStateChanged;
    }

}
