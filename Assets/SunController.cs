using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SunController : MonoBehaviour
{
    public float totalTime;
    [SerializeField] private float currentTime;
    public float xSunOffset;
    public float ySunOffset;
    public float ySunAditionalOffset;
    public float dayPercent;
    [SerializeField]private Material skyMaterial;
    [SerializeField]private Color startHorizonColor;
    [SerializeField]private Color highnoonHorizonColor;
    [SerializeField]private Color endtHorizonColor;
    [SerializeField]private Color startSkyColor;
    [SerializeField]private Color highnoonSkyColor;
    [SerializeField]private Color endtSkyColor;
    int _sunPosID;
    int _skyColorID;
    int _HorizonColorID;
    Coroutine cHighNoon;
    bool anocheciendo = false;
    [Header("Day Events")]
    [Range(0.0f, 1.0f)]
    public float firstDayEventTrigger = 0.3f;
    [Range(0.0f, 1.0f)]
    public float secondDayEventTrigger = 0.7f;
    public UnityEvent OnFirstFlag;
    public UnityEvent OnSecondFlag;
    bool firstEventTriggered;
    bool secondEventTriggered;


    // Start is called before the first frame update
    void Start()
    {
        _sunPosID = Shader.PropertyToID("_PosAstro");
        _skyColorID = Shader.PropertyToID("_SkyColor");
        _HorizonColorID = Shader.PropertyToID("_HorizonColor");
        cHighNoon = StartCoroutine(HighNoon());
    }

    // Update is called once per frame
    void Update()
    {
        if(!anocheciendo)
        {
            skyMaterial.SetVector(_sunPosID, new Vector3(transform.position.x - (transform.parent.position.x + xSunOffset) + (xSunOffset *  (currentTime/ (totalTime / 2))),
                        transform.position.y - (transform.parent.position.y + ySunOffset) + (ySunAditionalOffset * (currentTime / (totalTime / 2))), -5));
        }
        else
        {
            skyMaterial.SetVector(_sunPosID, new Vector3(transform.position.x - (transform.parent.position.x) + (xSunOffset*2 * ((currentTime / totalTime) - 0.5f)),
                        transform.position.y - (transform.parent.position.y + ySunOffset) + ySunAditionalOffset - ((ySunAditionalOffset * 2) * ((currentTime / totalTime)-0.5f)), -5));
        }
        
    }

    IEnumerator HighNoon()
    {
        while(currentTime < totalTime)
        {
            currentTime += Time.deltaTime;
            dayPercent = currentTime / totalTime;
            if (!anocheciendo && currentTime > (totalTime / 2))
                anocheciendo = true;
            if(dayPercent >= firstDayEventTrigger && !firstEventTriggered)
            {
                print("Empieza a quemar");
                GameManager.Instance.ItsSunny = true;
                firstEventTriggered = true;
                OnFirstFlag?.Invoke();
            }
            if (dayPercent >= secondDayEventTrigger && !secondEventTriggered)
            {
                GameManager.Instance.ItsSunny = false;
                print("Dejo de quemar");
                secondEventTriggered = true;
                OnSecondFlag?.Invoke();
            }
            if (!anocheciendo)
            {
                skyMaterial.SetColor(_skyColorID, Color.Lerp(startSkyColor, highnoonSkyColor, (currentTime / (totalTime / 2))));
                skyMaterial.SetColor(_HorizonColorID, Color.Lerp(startHorizonColor, highnoonHorizonColor, (currentTime / (totalTime / 2))));
                yield return null;
            }
            else
            {
                skyMaterial.SetColor(_skyColorID, Color.Lerp(highnoonSkyColor, endtSkyColor, (currentTime / totalTime)));
                skyMaterial.SetColor(_HorizonColorID, Color.Lerp(highnoonHorizonColor, endtHorizonColor, (currentTime / totalTime)));
                yield return null;
            }
            
        }

        cHighNoon = null;
    }
}
