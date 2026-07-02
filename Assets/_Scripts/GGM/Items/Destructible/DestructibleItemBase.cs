using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestructibleItemBase : MonoBehaviour
{
    public HealthBase healthBase;
    public float shakeDuration = 0.1f;
    public int shakeForce = 1;

    public int dropCoinAmount = 10;
    public GameObject coinPrefab;
    public Transform dropPosition;

    public float coinSpawnRange = .1f;

    private void OnValidate()
    {
        if(healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    void Start()
    {
        healthBase.OnDamage += OnDamage;
    }

    private void OnDamage(HealthBase h)
    {
        transform.DOShakeScale(shakeDuration, Vector3.up/10, shakeForce);
        DropCoins();
    }

    [NaughtyAttributes.Button]
    private void DropCoins()
    {
        var i = Instantiate(coinPrefab, dropPosition.position, Quaternion.identity);

        i.transform.localScale = Vector3.zero;
        i.transform.DOScale(1f, 0.1f).SetEase(Ease.OutBack);

        // move para uma posição aleatória
        Vector3 randomTarget = dropPosition.position + new Vector3(
            Random.Range(-coinSpawnRange, coinSpawnRange),
            Random.Range(-coinSpawnRange, coinSpawnRange),
            Random.Range(-coinSpawnRange, coinSpawnRange)
        );

        i.transform.DOMove(randomTarget, 0.5f).SetEase(Ease.OutQuad);
    }

    [NaughtyAttributes.Button]
    private void DropGroupOfCoins()
    {
        StartCoroutine(DropGroupOfCoinsCorroutine());
        // for(int i = 0; i < dropCoinAmount; i ++)
        // {
        //     DropCoins();
        // }
    }

    IEnumerator DropGroupOfCoinsCorroutine()
    {        
        for(int i = 0; i < dropCoinAmount; i ++)
        {
            DropCoins();
            yield return new WaitForSeconds(.1f);
        }
    }

}
