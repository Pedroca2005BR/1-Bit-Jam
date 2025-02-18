using UnityEngine;
using UnityEngine.SceneManagement;

public class Ir_Main_Menu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GoMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
