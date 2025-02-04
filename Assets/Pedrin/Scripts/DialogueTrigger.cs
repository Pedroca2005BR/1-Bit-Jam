using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private bool playerDetected;
    public Dialogue dialogue;

    //Detectar trigger com o player
    private void OnTriggerEnter2D(Collider2D collission) {
        if(collission.CompareTag("Player")) {
            playerDetected = true;
            Debug.Log("Player detected");
            dialogue.StartDialogue();
        }
    }
}
