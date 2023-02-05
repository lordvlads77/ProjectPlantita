
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    [SerializeField]private bool _itsWindy;
    [SerializeField]private bool _itsSunny;
    public Transform PlayerPos;
    [Range(0,5)][SerializeField]private float _gameSpeed = 1;
    private static GameManager _gameManager{get; set;}
    private bool _winner;
    private bool _gameOver;
    public CharacterLife playerLife;
    [SerializeField]private UnityEvent OnGameOver;
    [SerializeField]private UnityEvent OnWin;
    public float damageTickDelay;
    float contador;
    public int sunDamage;
    [SerializeField] private Transform sunRayOrigin;

    public BlowAwayPlayer windConditioner;
    private void Start() {
        Application.targetFrameRate = 60;
    }
    public static GameManager Instance 
    {
        get => _gameManager;
    }

    public float GameSpeed
    {
        get => _gameSpeed;
        set
        {
            _gameSpeed = value;
        }
    }

    public bool Winner
    {
        get => _winner;
        set {
            _winner = value;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            OnWin?.Invoke();
        } 
    }
    public bool GameOver
    {
        get => _gameOver;
        set
        {
            _gameOver = value;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            OnGameOver?.Invoke();
        } 
    }

    public bool ItsSunny
    {
        get => _itsSunny;
        set
        {
            _itsSunny = value;
        }
    }
    public bool ItsWindy
    {
        get => _itsWindy;
        set
        {
            _itsWindy = value;
        }
    }


    private void Awake() 
    {
        if(Instance == null)
        {
            _gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameOver && !Winner)
        {
            if (ItsSunny)
            {
                if (contador >= damageTickDelay)
                 {
                    contador += Time.deltaTime;
            
                    contador = 0;
                    RaycastHit2D hit = Physics2D.Linecast(sunRayOrigin.position, PlayerPos.position);

                    Debug.DrawLine(sunRayOrigin.position, PlayerPos.position, Color.red);

                    if (hit.collider != null && hit.collider.transform.CompareTag("Player"))
                    {
                        playerLife.Health.Damage(new LifePoint(sunDamage));
                    }
                }
            }

            if(ItsWindy)
            {
                if(!windConditioner.asalvo && !GameOver)
                {
                    GameOver = false;
                    windConditioner.FlyPlayer();
                }

            }
        }
        
    }
}
