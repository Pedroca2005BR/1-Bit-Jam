using UnityEngine;

public class FinalBomTrigger : MonoBehaviour
{
    public bool FinalBom = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FinalBom = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FinalBom = true;
        }
    }
}
