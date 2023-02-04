using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    public float totalTime;
    private float currentTime;
    public float xSunOffset;
    public float ySunOffset;
    public float ySunAditionalOffset;
    [SerializeField]private Material skyMaterial;
    [SerializeField]private Color startHorizonColor;
    [SerializeField]private Color endtHorizonColor;
    [SerializeField]private Color startSkyColor;
    [SerializeField]private Color endtSkyColor;
    int _sunPosID;
    int _skyColorID;
    int _HorizonColorID;
    Coroutine cHighNoon;

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
        skyMaterial.SetVector(_sunPosID, new Vector3(transform.position.x - (transform.parent.position.x + xSunOffset) + (xSunOffset *  (currentTime/totalTime)),
            transform.position.y - (transform.parent.position.y + ySunOffset) + (ySunAditionalOffset * (currentTime / totalTime)), -5));
    }

    IEnumerator HighNoon()
    {
        while(currentTime < totalTime)
        {
            currentTime += Time.deltaTime;
            skyMaterial.SetColor(_skyColorID, Color.Lerp(startSkyColor, endtSkyColor, (currentTime / totalTime)));
            skyMaterial.SetColor(_HorizonColorID, Color.Lerp(startHorizonColor, endtHorizonColor, (currentTime / totalTime)));
            yield return null;
        }

        cHighNoon = null;
    }
}
