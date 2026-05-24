using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBase3D : MonoBehaviour
    {
        public float startLife = 10f;
        [SerializeField] private float _currentLife;

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
        }

        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
            Destroy(gameObject);

        }

        public void OnDamage(float damage)
        {
            _currentLife -= damage;
            if(_currentLife <= 0)
            {
                Kill();
            }
        }

        #region Debug
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
                OnDamage(5f);
            }
        }

        #endregion




        // public int damage = 1;

        // public Animator animator;
        // public string triggerAttack = "Attack";
        // public string triggerDeath = "Death";
        // public HealthBase healthBase;

        // public AudioSource onKillSound;


        // // void Awake()
        // // {
        // //     //_healthBase = GetComponent<HealthBase>();

        // //     if(healthBase != null)
        // //     {
        // //         healthBase.OnKill += OnEnemyKill;
        // //     }
        // // }

        // private void OnEnemyKill()
        // {
        //     healthBase.OnKill -= OnEnemyKill;
        //     if (onKillSound != null) onKillSound.Play();
        //     PlayDeathAnimation();
        // }

        // private void OnCollisionEnter2D(Collision2D collision)
        // {
        //     var health = collision.gameObject.GetComponent<HealthBase>();

        //     if(health != null)
        //     {
        //         health.Damage(damage);
        //         PlayAttackAnimation();
        //     }
        // }

        // private void PlayAttackAnimation()
        // {
        //     if(animator != null)
        //     {
        //         animator.SetTrigger(triggerAttack);
        //     }
        // }

        // private void PlayDeathAnimation()
        // {
        //     if(animator != null)
        //     {
        //         animator.SetTrigger(triggerDeath);
        //     }
        // }
    }
}