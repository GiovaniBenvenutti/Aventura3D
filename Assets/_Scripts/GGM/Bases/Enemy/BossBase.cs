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
        PHASE1,
        PHASE2,
        PHASE3
    }
    public class BossBase : MonoBehaviour
    {
        private StateMachine<BossAction> _stateMachine;

        [Header("Animation")]
        public float startAnimationDuration = 1f;
        public Ease startAnimationEase = Ease.OutBack;

        [Header("Movement")]
        public float speed = 5f;
        public List<Transform> wayPoints;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _stateMachine = new StateMachine<BossAction>();
            _stateMachine.Init();

            _stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            _stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
        }

        #region Moviment

        public void GoToRandomPoint()
        {
            StartCoroutine(GoToPointCoroutine(wayPoints[Random.Range(0, wayPoints.Count)]));
        }

        IEnumerator GoToPointCoroutine(Transform t)
        {
            while (Vector3.Distance(transform.position, t.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                
                yield return new WaitForEndOfFrame();
            }
            
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

        #endregion




        #region State Machine

        public void SwitchState(BossAction state)
        {
            _stateMachine.SwitchState(state, this);
            
        }


        #endregion
    }
}