using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    
    [SerializeField] private GameObject menuCanvas;

    void Start()
    {
        menuCanvas.SetActive(false);

    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            menuCanvas.SetActive(!menuCanvas.activeSelf);
            
            if(menuCanvas.activeSelf){
                Time.timeScale = 0;
            }else{
                Time.timeScale = 1;
            }

        }
    }

    public void ReturnToMainMenu(){
        SceneManager.LoadScene("Main Menu");
    }

}
