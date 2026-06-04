using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.FSM;
using DG.Tweening;


namespace Boss
{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        SHOOT,
        DEATH,
        PHASE1,
        PHASE2,
        PHASE3
    }
    public class BossBase : MonoBehaviour
    {
        public BossHealth healthBase;   // usando outro nome porque já existia um doc healthBase
        private StateMachine<BossAction> _stateMachine;

        [Header("Animation")]
        public float startAnimationDuration = 1f;
        public Ease startAnimationEase = Ease.OutBack;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBtweenAttacks = .5f;


        [Header("Movement")]
        public float speed = 5f;
        public List<Transform> wayPoints;

        private void Awake()
        {
            Init();
            healthBase.OnKill += OnBossKill;
        }

        private void Init()
        {
            _stateMachine = new StateMachine<BossAction>();
            _stateMachine.Init();

            _stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            _stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            _stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            _stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());
        }


        public void OnBossKill(BossHealth health)
        {
           // Debug.Log("Boss morreu");
            SwitchState(BossAction.DEATH);
        }

        #region Attack

        public void StartAttack(Action EndCallBack = null)
        {
            StartCoroutine(AttackCoroutine(EndCallBack));
        }

        IEnumerator AttackCoroutine(Action EndCallBack = null)
        {
            int attacks = 0;

            while (attacks < attackAmount)
            {
                attacks++;
                transform.DOScale(1.2f, .1f).SetLoops(2, LoopType.Yoyo);
                Debug.Log("Attack");
                yield return new WaitForSeconds(timeBtweenAttacks);
            }
            EndCallBack?.Invoke();
        }


        #endregion


        #region Walk

        public void GoToRandomPoint(Action OnArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(wayPoints[UnityEngine.Random.Range(0, wayPoints.Count)], OnArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t, Action OnArrive = null)
        {
            while (Vector3.Distance(transform.position, t.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                
                yield return new WaitForEndOfFrame();
            }
            //if (OnArrive != null) OnArrive.Invoke();
            OnArrive?.Invoke();     // isso é a mesma coisa que a linha de cima, só que mais simples. O "?." é o operador de acesso condicional, ele verifica se o On   Arrive é diferente de null antes de chamar o Invoke()
        }



        #endregion


        #region Animation

        [NaughtyAttributes.Button]
        public void StartInitAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        #endregion


        #region Debug

        [NaughtyAttributes.Button]
        private void SwitchStateINIT()
        {
            SwitchState(BossAction.INIT);
        }

        [NaughtyAttributes.Button]
        private void SwitchStateWALK()
        {
            SwitchState(BossAction.WALK);
        }

        [NaughtyAttributes.Button]
        private void SwitchStateATTACK()
        {
            SwitchState(BossAction.ATTACK);
        }

        #endregion




        #region State Machine

        public void SwitchState(BossAction state)
        {
            _stateMachine.SwitchState(state, this);
            
        }


        #endregion
    }
}