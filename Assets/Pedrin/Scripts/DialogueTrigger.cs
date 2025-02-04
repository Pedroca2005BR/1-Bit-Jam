using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
   public Dialogue dialogue;

    //Detectar trigger com o player
    private void OnTriggerEnter2D(Collider2D collission) {
        if(collission.CompareTag("Player")) {
            Debug.Log("Player detected");
            dialogue.StartDialogue();
        }
    }
}
