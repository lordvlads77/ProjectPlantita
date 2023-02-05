using System;
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
    [SerializeField] private GameObject _WinPanel = default;
    [SerializeField] private GameObject _LostPanel = default;
    [SerializeField] private GameObject _credits = default;
    
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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            _LostPanel.SetActive(true);
            _WinPanel.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _WinPanel.SetActive(true);
            _LostPanel.SetActive(false);
        }
    }

    public void toMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void toLvlSelect()
    {
        _WinPanel.SetActive(false);
        _LostPanel.SetActive(false);
        SceneManager.LoadScene(0);
        _levelSelectPanel.SetActive(true);
    }

    public void credits()
    {
        _WinPanel.SetActive(false);
        _LostPanel.SetActive(false);
        _credits.SetActive(true);   
    }

    public void CargarEscena(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    
}
