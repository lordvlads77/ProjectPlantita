
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public Slider reloj;

    public BlowAwayPlayer windConditioner;
    public Volume globalVolume;
    Coroutine cVignette;

    public AudioSource playerAudio;
    public AudioSource musicAudio;
    private void Start() {
        Application.targetFrameRate = 60;
        VolumeProfile profile = globalVolume.sharedProfile;
        if (profile.TryGet<Vignette>(out var vignette))
            vignette.intensity.value = 0;

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

    public void CargarEscena(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    IEnumerator ModifyVignette(float endValue, bool increase)
    {
        VolumeProfile profile = globalVolume.sharedProfile;
        float counter = 0;
        if (profile.TryGet<Vignette>(out var vignette))
        {
            if(increase)
            {
                while(counter < endValue)
                {
                    counter += Time.deltaTime;
                    vignette.intensity.value = counter;
                    yield return null;
                }
            }
            else
            {
                counter = vignette.intensity.value;
                while (counter > endValue)
                {
                    counter -= Time.deltaTime;
                    vignette.intensity.value = counter;
                    yield return null;
                }
            }
        }
        cVignette = null;
    }

    public void EnableVignnete()
    {
        cVignette = StartCoroutine(ModifyVignette(0.55f, true));
    }
    public void DisableVignnete()
    {
        cVignette = StartCoroutine(ModifyVignette(0.0f, false));
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
