using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    
    public GameObject panelInstruct;

    // Start is called before the first frame update
    void Start()
    {
        panelInstruct.SetActive(false);
        
    }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Back() 
    {
        panelInstruct.SetActive(false);
    }

    public void LoadPanelInstruct()
    {
        panelInstruct.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}