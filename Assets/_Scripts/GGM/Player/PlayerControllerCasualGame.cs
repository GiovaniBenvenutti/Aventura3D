using UnityEngine;
using TMPro;
using DG.Tweening;
using GGM.Singleton;

public class PlayerControllerCasualGame : Singleton<PlayerControllerCasualGame>
{
    //  public variables
    public LevelManager levelManager;

    [Header("Player Boundries")]
    public float axisX = 4.5f;

    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 0.1f;

    [Header("TextMeshPro")]
    public TextMeshPro uiTextPowerUp;

    [Header("Coin Collector SetUp")]
    public GameObject coinCollector;

    [Header("Death VFX")]
    public ParticleSystem deathVFX;
    public AudioSource deathSFX;

    [Header("Animation Setup")]
    public AnimatorManager animatorManager;

    public float speed = 7f;

    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";

    public GameObject endScreen;

    [SerializeField] private BounceHelper _bounceHelper;

    public bool invencible = false;

    //  private variables
    private Vector3 _pos;
    private Vector3 _startPosition;
    private bool _canRun;
    private float _currentSpeed = 5f;
    private float _baseSpeedToAnimation = 7f;

    void Start() 
    {
        _startPosition = transform.position;
        ResetSpeed();
        _currentSpeed = speed;
        //startToRun(); 
        transform.localScale = Vector3.zero;
        FirstScale();
    }

    [Header("First Scale Setings")]
    public float scaleDuration = 0.9f;
    public float playerScale = 1.0f;
    public Ease scaleEase = Ease.OutBack;

    public void FirstScale()
    {
        transform.DOScale(new Vector3(playerScale, playerScale, playerScale), scaleDuration).SetEase(scaleEase);
    }

    public void Bounce(float bounce = 0f)
    {
        if(_bounceHelper != null) _bounceHelper.Bounce(bounce);
    }


    public void startToRun()
    {
        _canRun = true;
       animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseSpeedToAnimation);   
    }

    // Update is called once per frame
    void Update()
    {
        if(!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        if(_pos.x > axisX) _pos.x = axisX;
        else if(_pos.x < -axisX) _pos.x = -axisX;


        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * Time.deltaTime * _currentSpeed);
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.transform.CompareTag(tagToCheckEnemy))
        {
            if(!invencible)    
            {
                Debug.Log("Colidiu com inimigo!");
                EndGame(AnimatorManager.AnimationType.DEAD);
                moveBack(collision.transform);
                if(deathVFX != null) 
                {
                    deathVFX.Play();
                }
                Bounce(.8f);
            }
        }

        if (collision.transform.CompareTag(tagToCheckEndLine))
        { 
            Debug.Log("Chegou ao fim da pista!");
            EndGame();
            Invoke(nameof(levelManager.CreateLevelPieces), 2f);
        }
    }

    // private void OnTriggerEnter(Collider other) {
    //     if (other.CompareTag(tagToCheckEndLine))
    //     {
    //         //if(!invencible)
    //         //{ 
    //             Debug.Log("Chegou ao fim da pista!");
    //             EndGame();
    //         //}
    //     }
    // }

    private void moveBack(Transform target)
    {
        target.DOMoveZ(2f, 1f).SetRelative(true).SetEase(Ease.OutBack);
    }

    public void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)
    {
        _canRun = false;
        animatorManager.Play(animationType);   

        endScreen.SetActive(true);
        //levelManager.SpawnNextLevel();
    }


    #region PowerUp Methods

    public void SetPowerUpText (string powerUpName)
    {
       uiTextPowerUp.text = powerUpName;
    }

    public void PowerUpSpeedUp (float speedUp) 
    {
        _currentSpeed += speedUp;
    }

    public void ResetSpeed () 
    {
        _currentSpeed = speed;
    }

    public void SetInvencible (bool state = true) 
    {
        invencible = state;
    }

    public void ChangeHeight(float amountToHeight, float duration, float animationDuration, Ease ease)
    {
        transform.DOMoveY(_startPosition.y + amountToHeight, animationDuration).SetEase(ease);
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight(float animationDuration, Ease ease)
    {
        transform.DOMoveY(_startPosition.y, animationDuration).SetEase(ease);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }



    #endregion

}
