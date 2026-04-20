using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.Singleton;
using DG.Tweening;

public class CoinsAnimationManager : Singleton<CoinsAnimationManager>
{
    public List<ItemCollectableCoin> itens;

    [Header("Level Animation")]
    public float scaleDuration = 0.5f;
    public float scaleDelay = 0.1f;
    public float coinsScale = 0.5f;
    public Ease ease = Ease.OutBack;

    // Start is called before the first frame update
    void Start()
    {
        itens = new List<ItemCollectableCoin>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)) StartAnimations();       
    }

    public void RegisterCoin(ItemCollectableCoin i)
    {
        if(!itens.Contains(i))
        {
            itens.Add(i);
            i.transform.localScale = Vector3.zero;
        }
    }

    public void UnRegisterCoin(ItemCollectableCoin i)
    {
        if(itens.Contains(i)) itens.Remove(i);
    }

    public void StartAnimations()
    {
        StartCoroutine(ScaleCoinsByTime());
    }


    IEnumerator ScaleCoinsByTime()
    {
        foreach(var piece in itens)
        {
            piece.transform.localScale = Vector3.zero;
        
        }

        Sort();
        
        yield return null;

        for(int i = 0; i < itens.Count; i++)
        {
            itens[i].transform.DOScale(new Vector3(coinsScale, coinsScale, coinsScale), scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleDelay);
        }
    }

    private void Sort()
    {
        itens = itens.OrderBy(i => Vector3.Distance(this.transform.position, i.transform.position)).ToList();
    }
}
