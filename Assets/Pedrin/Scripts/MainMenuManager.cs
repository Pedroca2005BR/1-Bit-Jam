using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject creditsPanel;

    void Start()
    {
        AudioManager.Instance.PlayMusic("Piano");
    }

    public void Play()
    {
        SceneManager.LoadScene(sceneName);
        AudioManager.Instance.PlayMusic("nevasca");
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Quiting...");
        Application.Quit();
    }
}
