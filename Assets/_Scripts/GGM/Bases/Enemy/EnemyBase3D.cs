using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GGM.Animation;

namespace Enemy
{
    public class EnemyBase3D : MonoBehaviour, IDamageable
    {
        public Collider collider;
        public FlashColor3D flashColor;
        public ParticleSystem hitParticleSystem;
        public float startLife = 10f;
        [SerializeField] private float _currentLife;
        
        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;

        [Header("Start Animation")]
        public float startAnimationDuration = 0.5f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        private void OnValidate()
        {
            if (collider == null) collider = GetComponent<Collider>();
            if (flashColor == null) flashColor = GetComponentInChildren<FlashColor3D>();
            if (hitParticleSystem == null) hitParticleSystem = GetComponentInChildren<ParticleSystem>();
            if (_animationBase == null) _animationBase = GetComponentInChildren<AnimationBase>();
        }

        private void Awake()
        {
            Init();
        }

        protected virtual void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Init()
        {
            ResetLife();
            if(startWithBornAnimation) BornAnimation();
        }

        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
            if (collider != null) collider.enabled = false;
            Destroy(gameObject, 3f);
            playAnimationByTrigger(AnimationType.DEATH);

        }

        public void OnDamage(float damage)
        {
            if (flashColor != null) flashColor.Flash();
            if (hitParticleSystem != null) hitParticleSystem.Play();
            _currentLife -= damage;
            if(_currentLife <= 0)
            {
                Kill();
            }
        }

        #region Animation
        private void BornAnimation()
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
        // private void Update()
        // {
        //     if(Input.GetKeyDown(KeyCode.T))
        //     {
        //         OnDamage(5f);
        //     }
        // }
        #endregion

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


    }
}