using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerBaseState currentState;
    public Player_Idle_State idleState; // = new Player_Idle_State();
    public Player_Run_State runState; // = new Player_Run_State();
    public Player_Jump_State jumpState; // = new Player_Jump_State();
    public Player_Die_State dieState; // = new Player_Die_State();

    void Awake()
    {
        // Inicializa os estados
        idleState = gameObject.AddComponent<Player_Idle_State>();
        runState = gameObject.AddComponent<Player_Run_State>();
        jumpState = gameObject.AddComponent<Player_Jump_State>();
        dieState = gameObject.AddComponent<Player_Die_State>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = idleState;
        currentState.OnStateEnter(this);
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     currentState.OnCollisionEnter(this, collision);
    // }


    // Update is called once per frame
    void Update()
    {
        currentState.OnStateStay(this);
    }

    public void SwitchState(PlayerBaseState newState)
    {
        currentState.OnStateExit(this);
        currentState = newState;
        currentState.OnStateEnter(this);
    }
}
