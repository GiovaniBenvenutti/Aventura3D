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
        public float startLife = 10f;
        [SerializeField] private float _currentLife;
        
        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;

        [Header("Start Animation")]
        public float startAnimationDuration = 0.5f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

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
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
                OnDamage(5f);
            }
        }
        #endregion

        public void Damage(float damage)
        {
            OnDamage(damage);
        }

    }
}