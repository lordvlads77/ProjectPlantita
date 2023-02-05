using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIController : MonoBehaviour
{
    public static GUIController Instance { get; private set; }

    [Header("Panels")] 
    [SerializeField] private GameObject _mainMenuPanel = default;
    [SerializeField] private GameObject _levelSelectPanel = default;
    
    public void ButtonPlay()
    {
        _mainMenuPanel.SetActive(false);
        _levelSelectPanel.SetActive(true);
    }

    public void FirstLevel()
    {
        // The Logic for the acces to the first level goes here.
    }

    public void SecondLevel()
    {
        // The Logic to access to the second level goes here.
    }

    public void ThirdLevel()
    {
        // The Logic to access to the third level goes here.
    }
}
