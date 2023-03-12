using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _parentAmbulance;
    [SerializeField] private bool _isPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPaused)
            {
                GameData.CanPlay = false;
                Time.timeScale = 0;
                _parentAmbulance.SetActive(false);
                _pausePanel.SetActive(true);
                _isPaused = true;
            }
            else
            {
                GameData.CanPlay = true;
                Time.timeScale = 1;
                _parentAmbulance.SetActive(true);
                _pausePanel.SetActive(false);
                _isPaused = false;
            }
        }
    }

    public void Quit()
    {
        //If we are running in a standalone build of the game
#if UNITY_STANDALONE
        //Quit the application
        Application.Quit();
#endif

        //If we are running in the editor
#if UNITY_EDITOR
        //Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Continue()
    {
        GameData.CanPlay = true;
        Time.timeScale = 1;
        _parentAmbulance.SetActive(!_parentAmbulance.activeSelf);
        _pausePanel.SetActive(!_pausePanel.activeSelf);
        _isPaused = false;
    }
}