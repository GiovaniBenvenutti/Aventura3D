using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.FSM;
using DG.Tweening;
using GGM.Animation;


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

    public class BossBase : MonoBehaviour, IDamageable
    {
        public Collider collider;
        public FlashColor3D flashColor;
        public ParticleSystem hitParticleSystem;
        public Animator animator;
        public BossHealth healthBase;   // usando outro nome porque já existia um doc healthBase
        private StateMachine<BossAction> _stateMachine;

        [Header("Animation")]
        public float startAnimationDuration = 1f;
        public Ease startAnimationEase = Ease.OutBack;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBtweenAttacks = .5f;


        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;


        [Header("Movement")]
        public float speed = 5f;
        public List<Transform> wayPoints;

        void OnValidate()
        {
            if (animator == null) animator = GetComponentInChildren<Animator>();
            if (healthBase == null) healthBase = GetComponent<BossHealth>();
            
            if (collider == null) collider = GetComponent<Collider>();
            if (flashColor == null) flashColor = GetComponentInChildren<FlashColor3D>();
            if (hitParticleSystem == null) hitParticleSystem = GetComponentInChildren<ParticleSystem>();
            if (_animationBase == null) _animationBase = GetComponentInChildren<AnimationBase>();
        }

        private void Awake()
        {
            Init();
            healthBase.OnKill += OnBossKill;
            healthBase.OnDamage += Damage;
        }

        private void Start()
        {
            SwitchState(BossAction.INIT);
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


        #region KillBoss

        public void OnBossKill(BossHealth health)
        {
            if (flashColor != null) flashColor.Flash();
            if (hitParticleSystem != null) hitParticleSystem.Play();
            if (collider != null) collider.enabled = false;
            playAnimationByTrigger(AnimationType.DEATH);

           // Debug.Log("Boss morreu");
            SwitchState(BossAction.DEATH);
        }

        public void StartDeathAnimation()
        {
            animator.SetTrigger("Death");
        }

        #endregion


        #region Attack

        public void StartAttack(Action EndCallBack = null)
        {
            StartCoroutine(AttackCoroutine(EndCallBack));
            animator.SetTrigger("Attack");
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
        public void StartWalk(Action EndCallBack = null)
        {
            StartCoroutine(StartWalkCoroutine(EndCallBack));
        }

        private IEnumerator StartWalkCoroutine(Action EndCallBack)
        {
            yield return new WaitForSeconds(1.5f);
            EndCallBack?.Invoke();
        }


        IEnumerator GoToPointCoroutine(Transform t, Action OnArrive = null)
        {
            while (Vector3.Distance(transform.position, t.position) > 0.1f)
            {
             //   animator.SetTrigger("Walk");    
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

        public void playAnimationByTrigger(AnimationType animationType)
        {
            if(_animationBase != null)
            {
                _animationBase.PlayAnimationByTrigger(animationType);
            }
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

        
        #region Damage

        public void Damage(float damage)
        {
            OnDamage(damage);
        }

        public void Damage(float damage, Vector3 direction)
        {
            OnDamage(damage);

            CharacterController controller = GetComponent<CharacterController>();

            if (controller != null)
            {
                // Se tiver CharacterController, aplica deslocamento via Move
                StartCoroutine(Knockback(controller, direction));
            }
            else
            {
                // Se não tiver, usa DOTween normalmente
                transform.DOMove(transform.position - direction, 0.2f);
            }
        }

        public void Damage(BossHealth health)
        {
            OnDamage(0f);
        }

        public void OnDamage(float damage)
        {
            if (flashColor != null) flashColor.Flash();
            if (hitParticleSystem != null) hitParticleSystem.Play();
            healthBase.currentLife -= damage;
            if(healthBase.currentLife <= 0)
            {
                OnBossKill(healthBase);
            }
        }

        private IEnumerator Knockback(CharacterController controller, Vector3 direction)
        {
            float duration = 0.2f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                controller.Move(-direction * (Time.deltaTime / duration));
                elapsed += Time.deltaTime;
                yield return null;
            }
        }


        private void OnCollisionEnter(Collision collision)
        {
            Player p = collision.transform.GetComponent<Player>();

            if (p != null)
            {
                p.Damage(1f);
            }
        }

        #endregion
    }
}