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
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
            AudioManager.Instance.PlaySFX("brakeJail");
        }
    }
}
