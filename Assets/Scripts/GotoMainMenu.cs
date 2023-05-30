using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GotoMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject FadeOut;
    
    private void Update()
    {
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            StartCoroutine(GoToMainMenu());
        }
    }

    IEnumerator GoToMainMenu()
    {
        FadeOut.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenu");
    }
}
