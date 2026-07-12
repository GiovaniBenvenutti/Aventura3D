using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GGM.FSM;
using DG.Tweening;
using GGM.Animation;
//using UnityEngine.SceneManagement;



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
        public LoadSceneHelper loadSceneHelper;
        public new Collider collider;

        public CharacterController characterController;

        public Player _player;
        public bool lookAtPlayer = true;
        //private bool checkingDistance = false;

        public FlashColor3D flashColor;
        public ParticleSystem hitParticleSystem;
        public Animator animator;
        public BossHealth health;   // usando outro nome porque já existia um doc healthBase
        private StateMachine<BossAction> _stateMachine;

        [Header("Animation")]
        public float startAnimationDuration = 1f;
        public Ease startAnimationEase = Ease.OutBack;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBtweenAttacks = .5f;
        private bool checkingDistance = false;
        public float distanceToStart = 5f;
        private float verticalVelocity = 0f;
        public float gravity = -9.81f;


        [Header("Events")]
        public UnityEvent OnKillEvent;


        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;


        [Header("Movement")]
        public float speed = 5f;
        public List<Transform> wayPoints;

        void OnValidate()
        {
            if (animator == null) animator = GetComponentInChildren<Animator>();
            if (health == null) health = GetComponent<BossHealth>();
            
            if (collider == null) collider = GetComponent<Collider>();
            if (flashColor == null) flashColor = GetComponentInChildren<FlashColor3D>();
            if (hitParticleSystem == null) hitParticleSystem = GetComponentInChildren<ParticleSystem>();
            if (_animationBase == null) _animationBase = GetComponentInChildren<AnimationBase>();
            if (characterController == null) characterController = GetComponent<CharacterController>();
        }

        private void Awake()
        {
            Init();
            health.OnKill += OnBossKill;
            health.OnDamage += Damage;
        }

        // private void Start()
        // {
        //     //SwitchState(BossAction.INIT);
        // }

        public virtual void Update()
        {
            if (characterController != null)
            {
                // aplica gravidade
                if (characterController.isGrounded)
                {
                    // se está no chão, zera a velocidade vertical
                    verticalVelocity = 0f;
                }
                else
                {
                    // acumula gravidade
                    verticalVelocity += gravity * Time.deltaTime;
                }

                // move apenas no eixo Y
                Vector3 move = new Vector3(0, verticalVelocity * Time.deltaTime, 0);
                characterController.Move(move);
            }

            if (lookAtPlayer && _player != null)
            {                
                transform.LookAt(_player.transform.position);

                if (!checkingDistance)
                {
                    StartCoroutine(CheckPlayerDistance());
                }
            }
        }


        IEnumerator CheckPlayerDistance()
        {
            checkingDistance = true;

            while (_player != null) // mantém loop enquanto houver player
            {
                float distance = Vector3.Distance(transform.position, _player.transform.position);

                if (distance < distanceToStart)
                {
                    SwitchState(BossAction.INIT);
                    Debug.Log("Player perto, iniciando combate");
                    // Se quiser parar aqui, use break; 
                    // Se quiser continuar verificando, apenas deixa o loop rodar
                }

                yield return new WaitForSeconds(1f); // espera 1 segundo antes da próxima checagem
            }

            checkingDistance = false;
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
            //StartCoroutine(RestartGame(2f)); // REINICIA O JOGO APÓS 2 SEGUNDOS

            // if (flashColor != null) flashColor.Flash();
            // if (hitParticleSystem != null) hitParticleSystem.Play();
            if (collider != null) collider.enabled = false;
            playAnimationByTrigger(AnimationType.DEATH);

            SwitchState(BossAction.DEATH);

            // chama o método RestartGame após 3 segundos
            // Debug.Log("Boss morreu");
            OnKillEvent?.Invoke();
        }


        private IEnumerator RestartGame(float delay)
        {
            // espera o tempo definido
            yield return new WaitForSeconds(delay);

            Debug.Log("coroutine funcionou");
           //loadSceneHelper.Load(loadSceneHelper.GetActiveScene().name);
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
            OnDamage(1f);
        }

        public void OnDamage(float damage)
        {
            if (flashColor != null) flashColor.Flash();
            if (hitParticleSystem != null) hitParticleSystem.Play();
            health.Damage(damage);
            if(health.currentLife <= 0)
            {
                OnBossKill(health);
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
                p.health.Damage(1f);
            }
        }

        #endregion
    }
}