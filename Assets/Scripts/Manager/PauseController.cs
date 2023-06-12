using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private bool _isPaused;

    void Update()
    {
        if (Gamepad.current.startButton.wasPressedThisFrame)
        {
            if (_isPaused == false)
            {
                //GameData.CanPlay = false;
                Time.timeScale = 0;
                _pausePanel.SetActive(true);
                _isPaused = true;
            }
            else
            {
                //GameData.CanPlay = true;
                Time.timeScale = 1;
                _pausePanel.SetActive(false);
                _isPaused = false;
            }
        }
    }


    
    // Boutons dans le Pause Panel
    
    public void Continue()
    {
        //GameData.CanPlay = true;
        Time.timeScale = 1;
        _pausePanel.SetActive(!_pausePanel.activeSelf);
        _isPaused = false;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(sceneName:"MainMenu");
    }

    public void ActiveOption()
    {
        
    }
    
    
    //PAS TOUCHE
    
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
}