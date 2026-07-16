using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class EndGame1 : MonoBehaviour
{
    public LoadSceneHelper loadSceneHelper;

    public List<GameObject> endGameObjects;

    private bool _isEndGame = false;

    public int currentLevel;


    // Start is called before the first frame update
    void Start()
    {
        loadSceneHelper = FindObjectOfType<LoadSceneHelper>();
        endGameObjects.ForEach(obj => obj.SetActive(false));
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    void OnTriggerEnter(Collider other)
    {
        if (_isEndGame) return;
        if (other.CompareTag("Player"))
        {
            ShowEndGame();
        }
    }

    [NaughtyAttributes.Button("Show End Game")]
    private void ShowEndGame()
    {
        _isEndGame = true;

        loadSceneHelper.currentLevel = currentLevel; 

       // CheckPointManager.Instance.ResetCheckPoint();
        SaveManager.Instance.SaveNewCheckPointIndex(1);
        SaveManager.Instance.SaveLastLevel(currentLevel + 1 );

  //      Debug.Log("End Game"); 
        //endGameObjects.ForEach(obj => obj.SetActive(true));

        foreach (var obj in endGameObjects)
        {
            obj.SetActive(true);
        //    obj.transform.localScale = Vector3.zero;
            obj.transform.DOScale(0, .2f).From().SetEase(Ease.OutBack);
        }
        

        StartCoroutine(RestartGame(7f)); // REINICIA O JOGO APÓS 7 SEGUNDOS

    }


    private IEnumerator RestartGame(float delay)
    {
        // espera o tempo definido
        yield return new WaitForSeconds(delay);

//        Debug.Log("coroutine funcionou");
        //loadSceneHelper.Load(loadSceneHelper.GetActiveScene().name);
        loadSceneHelper.Load(0);    // manda devolta pro menu
    }


}
