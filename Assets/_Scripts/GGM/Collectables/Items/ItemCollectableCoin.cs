using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    public Collider collider;
    public bool isCollected = false;
    public float lerp = 5f;
    public float minDistance = 0.5f;

    public void Start()
    {
        CoinsAnimationManager.Instance.RegisterCoin(this);
    }

    protected override void OnCollect()
    {
        base.OnCollect();
        collider.enabled = false;
        isCollected = true;
        PlayerController.Instance.Bounce();
    }

    protected override void Collect()
    {
        OnCollect();
    }

    private void Update()
    {
        if (isCollected)
        {

            Vector3 playerPosition = PlayerController.Instance.transform.position;
            transform.position = Vector3.Lerp(transform.position, playerPosition, lerp * Time.deltaTime);

            if(Vector3.Distance(transform.position, playerPosition) < minDistance)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy() 
    {
        CoinsAnimationManager.Instance.UnRegisterCoin(this);
    }

    private void OnDisable() 
    {
        CoinsAnimationManager.Instance.UnRegisterCoin(this);
    }
}

