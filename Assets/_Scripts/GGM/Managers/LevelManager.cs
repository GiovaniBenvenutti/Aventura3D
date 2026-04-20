using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public Transform container;
    public List<GameObject> levels;
    public List<LevelPieceBaseSetup> levelPieceBaseSetups;
    public float delayToSpawnPieces = 0.1f;

    private int _index;
    private GameObject _currentLevel;
    [SerializeField] private List<LevelPieceBase> _spawnedPieces = new List<LevelPieceBase>();
    private LevelPieceBaseSetup _currentSetup;

    [Header("Level Animation")]
    public float scaleDuration = 0.5f;
    public float scaleDelay = 0.1f;
    public Ease ease = Ease.OutBack;

    // Update is called once per frame
    void Start()
    {
        CreateLevelPieces();
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.D)) CreateLevelPieces();
    }

    public void SpawnNextLevel()
    {
        if(_currentLevel != null) 
        {
            Destroy(_currentLevel);
            _index++;

            if(_index >= levels.Count) resetLevelIndex();
        }

        _currentLevel = Instantiate(levels[_index], container);
        _currentLevel.transform.localPosition = Vector3.zero;
    }

    public void resetLevelIndex () 
    {
        _index = 0;
    }

    #region Generete Porcedural Level

        public void CreateLevelPieces() 
        {
            ClearLevelPieces();

            //SpawnNextLevel();

            if(_currentSetup != null)
            {
                _index ++;
                if(_index >= levelPieceBaseSetups.Count) resetLevelIndex();
            }
            _currentSetup = levelPieceBaseSetups[_index];


            for (int i = 0; i < _currentSetup.piecesStartCount; i++)
            {
                GenerateLevel(_currentSetup.levelPiecesStartPrefabs);
            } 
            
            for (int i = 0; i < _currentSetup.piecesCount; i++)
            {
                GenerateLevel(_currentSetup.levelPiecesPrefabs);
            } 
            
            for (int i = 0; i < _currentSetup.piecesEndCount; i++)
            {
                GenerateLevel(_currentSetup.levelPiecesEndPrefabs);
            } 

            ColorManager.Instance.ChangeColorByType(_currentSetup.artType);

        }

        IEnumerator CreateLevelCoroutine () 
        {
            
            ClearLevelPieces();

            if(_currentSetup != null)
            {
                _index ++;
                if(_index >= levelPieceBaseSetups.Count) resetLevelIndex();
            }
            _currentSetup = levelPieceBaseSetups[_index];

            
            for (int i = 0; i < _currentSetup.piecesStartCount; i++)
            {
                GenerateLevel(_currentSetup.levelPiecesStartPrefabs);
                yield return new WaitForSeconds(delayToSpawnPieces);
            } 
            
            for (int i = 0; i < _currentSetup.piecesCount; i++)
            {
                GenerateLevel(_currentSetup.levelPiecesPrefabs);
                yield return new WaitForSeconds(delayToSpawnPieces);
            } 
            
            for (int i = 0; i < _currentSetup.piecesEndCount; i++)
            {
                GenerateLevel(_currentSetup.levelPiecesEndPrefabs);
                yield return new WaitForSeconds(delayToSpawnPieces);
            } 

            ColorManager.Instance.ChangeColorByType(_currentSetup.artType);
        }

        IEnumerator ScalePiecesByTime()
        {
            foreach(var piece in _spawnedPieces)
            {
                piece.visualRoot.localScale = Vector3.zero;
            
            }
            yield return null;

            for(int i = 0; i < _spawnedPieces.Count; i++)
            {
                _spawnedPieces[i].visualRoot.DOScale(Vector3.one, scaleDuration).SetEase(ease);
                yield return new WaitForSeconds(scaleDelay);
            }
            CoinsAnimationManager.Instance.StartAnimations();  
        }

        private void GenerateLevel(List<LevelPieceBase> list = null)
        {
            var piece = list[Random.Range(0, list.Count)];
            var spawnedPiece = Instantiate(piece, container);

            if(_spawnedPieces.Count > 0) 
            {
                var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];
                spawnedPiece.transform.position = lastPiece.endPiecePoint.position;
            
                StartCoroutine(ScalePiecesByTime());
            }
            else 
            {
                spawnedPiece.transform.localPosition = Vector3.zero;
                StartCoroutine(ScalePiecesByTime());

            }

            foreach(var p in spawnedPiece.GetComponentsInChildren<ArtPiece>())
            {
                p.ChangePiece(ArtManager.Instance.GetArtSetupByType(_currentSetup.artType).artPrefab);
            }

            _spawnedPieces.Add(spawnedPiece);
            
        }

        public void ClearLevelPieces() 
        {
            if(_spawnedPieces == null) return;

            for(int i = _spawnedPieces.Count - 1; i >= 0; i--)
            {
                Destroy(_spawnedPieces[i].gameObject);
            }
            _spawnedPieces.Clear();
        }




    #endregion
}
