using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    public Material[] WindMaterials;
    int _WindStrenghtID;
    public float windStrength;
    [SerializeField]private float windStartDelay;
    [SerializeField]private float windStopDelay;
    [SerializeField]private float windDuration;
    Coroutine cWindCicle;

    // Start is called before the first frame update
    void Start()
    {
        _WindStrenghtID = Shader.PropertyToID("_WindStrength");
        cWindCicle = StartCoroutine(WindCicle());
    }


    IEnumerator WindCicle()
    {
        float counter = 0;
        while (counter < windStartDelay )
        {
            counter += Time.deltaTime;
            for(int i = 0; i < WindMaterials.Length; i++)
            {
                WindMaterials[i].SetFloat(_WindStrenghtID, windStrength * (counter / windStartDelay));
            }
            yield return null;
        }
        //Aqui se llaman los eventos de el viento

        yield return new WaitForSeconds(windDuration);
        counter = 0;
        while (counter < windStopDelay)
        {
            counter += Time.deltaTime;
            for (int i = 0; i < WindMaterials.Length; i++)
            {
                WindMaterials[i].SetFloat(_WindStrenghtID, windStrength - (windStrength * (counter / windStopDelay)));
            }
            yield return null;
        }
        for (int i = 0; i < WindMaterials.Length; i++)
        {
            WindMaterials[i].SetFloat(_WindStrenghtID, 0);
        }

        cWindCicle = null;
    }
}
