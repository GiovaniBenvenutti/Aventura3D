using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.Singleton;
using DG.Tweening;
using GGM.FSM;

public class GameManager : Singleton<GameManager>
{
#region inicialize player

    [Header("Player")]
    public GameObject playerPrefab;

    [Header("Enemies")]
    public List<GameObject> enemies;

    [Header("References")]
    public Transform startPoint;
    
    [Header("Animations")]
    public float duration = 1.2f;
    public float delay = .05f;
    public Ease ease = Ease.OutBack;

    private GameObject _currentPlayer;

    void Start()
    {
        Init();
        StartFSM();
    }

    public void Init()
    {
        SpawPlayer();
    }

    public void SpawPlayer()
    {
        if (playerPrefab == null || startPoint == null)
        {
            Debug.LogWarning("Player Prefab or Start Point is not assigned.");
            return;
        }
        _currentPlayer = Instantiate(playerPrefab);
        _currentPlayer.transform.position = startPoint.transform.position;
        _currentPlayer.transform.DOScale(0, duration).SetEase(ease).From().SetDelay(delay);

    }

#endregion

#region Game States

    public enum GameStates
    {
        NONE,
        INTRO,
        MENU,
        START,
        GAMEPLAY,
        PAUSE,
        WIN,
        LOSE,
        GAMEOVER
    }

    public StateMachine<GameStates> stateMachine;

    public void StartFSM() 
    {
        InitFSM();    
    }

    public void InitFSM()
    {
        stateMachine = new StateMachine<GameStates>(/*GameStates.INTRO*/);
        stateMachine.Init();
        stateMachine.RegisterStates(GameStates.INTRO, new GMState_Intro());
        stateMachine.RegisterStates(GameStates.GAMEPLAY, new StateBase());
        stateMachine.RegisterStates(GameStates.PAUSE, new StateBase());
        stateMachine.RegisterStates(GameStates.WIN, new StateBase());
        stateMachine.RegisterStates(GameStates.LOSE, new StateBase());
        stateMachine.RegisterStates(GameStates.GAMEOVER, new StateBase());
        stateMachine.SwitchState(GameStates.INTRO);
    }




#endregion


}
