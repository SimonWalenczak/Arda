using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private bool _canGoToMainMenu;

    public int SecondBeforeCredits;

    private void Start()
    {
        StartCoroutine(WaitingForCredits());
    }

    IEnumerator WaitingForCredits()
    {
        yield return new WaitForSeconds(SecondBeforeCredits);
        SceneManager.LoadScene("Credits");
    }
}