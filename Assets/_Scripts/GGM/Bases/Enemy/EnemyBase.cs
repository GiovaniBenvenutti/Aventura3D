using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public int damage = 1;  

        public Animator animator;
        public string triggerAttack = "Attack";
        public string triggerDeath = "Death";
        public HealthBase healthBase;

        public AudioSource onKillSound;


        void Awake()
        {
            //_healthBase = GetComponent<HealthBase>();

            if(healthBase != null)
            {
                healthBase.OnKill += OnEnemyKill;
                healthBase.OnDamage += Damage;
            }
        }

        private void Damage(HealthBase @base)
        {
            //throw new NotImplementedException();
        }

        private void OnEnemyKill(HealthBase healthBase)
        {
            healthBase.OnKill -= OnEnemyKill;
            if (onKillSound != null) onKillSound.Play();
            PlayDeathAnimation();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var health = collision.gameObject.GetComponent<HealthBase>();

            if(health != null)
            {
                health.Damage(damage);
                PlayAttackAnimation();
            }
        }

        private void PlayAttackAnimation()
        {
            if(animator != null)
            {
                animator.SetTrigger(triggerAttack);
            }
        }

        private void PlayDeathAnimation()
        {
            if(animator != null)
            {
                animator.SetTrigger(triggerDeath);
            }
        }
    }

}