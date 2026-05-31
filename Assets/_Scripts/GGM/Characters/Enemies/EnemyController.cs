using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GGM.Animation;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    private Action updateBehavior;          // delegate para guardar o comportamento
    private CharacterController controller;
    public AnimationBase animationBase;
    public enum EnemyType { Melee, Shooter, Jumper }

    public EnemyType enemyType;             // configurável no Inspector
    public Transform player;
    public float moveSpeed = 7f;
    public float SightRange = 50f;
    private float verticalVelocity = 0f;
    public float gravity = -9.81f;


    [Header("Melee Settings")]
    public float biteAttackRange = 5.0f;


    [Header("Shooter Settings")]
    public Transform shootPoint;
    public ProjectileBase3D projectilePrefab;
    public float shootDistance = 50f;
    public float avoidDistance = 20f;
    public float shootCooldown = 1.5f;
    private bool isShooting = false;


    [Header("Jumper Settings")]
    private bool isJumping = false;
    public float jumpForce = 10f;
    public float forwardForce = 15f;



    void Awake()
    {
        animationBase = GetComponentInChildren<AnimationBase>();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shootPoint = GetComponentInChildren<Transform>().Find("ShootPoint");

        // define o comportamento uma vez
        switch (enemyType)
        {
            case EnemyType.Melee:
                updateBehavior = MeleeBehavior;
                break;
            case EnemyType.Shooter:
                updateBehavior = ShooterBehavior;
                break;
            case EnemyType.Jumper:
                updateBehavior = JumperBehavior;
                break;
        }
    }


    void Update()
    {
        if (player == null || controller == null) return;

        // aplica gravidade sempre
        if (controller.isGrounded && verticalVelocity < 0)
        {
            // reseta a velocidade vertical quando toca o chão
            verticalVelocity = -1f;
        }
        else
        {
            // acumula gravidade
            verticalVelocity += gravity * Time.deltaTime;
        }

        // movimento vertical aplicado independente do comportamento
        Vector3 gravityMove = new Vector3(0, verticalVelocity * Time.deltaTime, 0);
        controller.Move(gravityMove);

        // chama apenas o comportamento definido (Melee, Ranged, Jumper etc.)
        updateBehavior?.Invoke();
    }


    private void MeleeBehavior()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= SightRange && distance > biteAttackRange) // segue o player, mas para um pouco antes de chegar muito perto
        {
            Vector3 direction = (player.position - transform.position);
            direction.y = 0;
            direction.Normalize();

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            
            // aplica rotação adicional de 180° no eixo X
            lookRotation *= Quaternion.Euler(0f, 180f, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f * Time.deltaTime);

            controller.Move(-transform.forward * moveSpeed * Time.deltaTime);

            if (animationBase != null)
            {
                animationBase.PlayAnimationByTrigger(AnimationType.RUN);
            }
        }
        else if (distance <= biteAttackRange)
        {
            BiteAttack();
        }
        else
        {
            if (animationBase != null)
            {
                animationBase.PlayAnimationByTrigger(AnimationType.IDLE);
            }
        }
    }
    private void BiteAttack()
    {
        Debug.Log("Inimigo realizou ataque de mordida!");
        if (animationBase != null)
        {
            animationBase.PlayAnimationByTrigger(AnimationType.ATTACK);
        }
    }

    private void ShooterBehavior()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        Vector3 direction = (player.position - transform.position);
        direction.y = 0;
        direction.Normalize();
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        // aplica rotação adicional de 180° no eixo X
        lookRotation *= Quaternion.Euler(0f, 180f, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f * Time.deltaTime);

        if (distance <= shootDistance)
        {
            if (!isShooting)
            {
                if (animationBase != null)
                {
                    animationBase.PlayAnimationByTrigger(AnimationType.ATTACK);
                }

                StartCoroutine(ShootAttackCoroutine());
            }
        }
        else
        {
            if (animationBase != null)
            {
                animationBase.PlayAnimationByTrigger(AnimationType.IDLE);
            }

            StopCoroutine(ShootAttackCoroutine());

            isShooting = false;
        }

        if (distance <= avoidDistance ) // segue o player, mas para um pouco antes de chegar muito perto
        {
            controller.Move(transform.forward * moveSpeed * Time.deltaTime);
        }
    }
    
    private IEnumerator ShootAttackCoroutine()
    {
        isShooting = true;
        var oldSpeed = moveSpeed;
        moveSpeed = 2f; // para o inimigo enquanto atira

        Debug.Log("Inimigo iniciou ataque de tiro!");


        if (projectilePrefab != null && shootPoint != null)
        {
            var projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            // Configurações adicionais do projétil podem ser feitas aqui
        }

        // espera o cooldown antes de permitir outro disparo
        yield return new WaitForSeconds(shootCooldown);

        isShooting = false;
        moveSpeed = oldSpeed; // restaura a velocidade original
    }

    private void JumperBehavior()
    {    
        float distance = Vector3.Distance(transform.position, player.position);

        // Se o player estiver dentro do raio de visão e o inimigo não estiver já preparando um salto
        if (distance <= SightRange && !isJumping)
        {
            StartCoroutine(JumpTowardsPlayer());
        }
    }


private IEnumerator JumpTowardsPlayer()
{
    isJumping = true;

    // calcula direção até a posição atual do player
    Vector3 direction = (player.position - transform.position);
    direction.y = 0;
    direction.Normalize();

    // aplica rotação para olhar o player
    Quaternion lookRotation = Quaternion.LookRotation(-direction);
    transform.rotation = lookRotation;

    // impulso inicial: vertical + horizontal juntos
    verticalVelocity = jumpForce;

    // enquanto não tocar efetivamente o chão
    while (!controller.isGrounded)
    {
        // aplica gravidade
        verticalVelocity += gravity * Time.deltaTime;

        // movimento horizontal contínuo + vertical
        Vector3 move = direction * forwardForce * Time.deltaTime;
        move.y = verticalVelocity * Time.deltaTime;

        controller.Move(move);

        yield return null; // espera próximo frame
    }

    // cooldown antes de permitir novo salto
    yield return new WaitForSeconds(0.2f);

    isJumping = false;
}




}

