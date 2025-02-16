using System.Collections;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se o objeto de colisão tiver o componente IRespawnable, entra no if
        if (collision.gameObject.TryGetComponent<IRespawnable>(out var respawnable))
        {
            collision.gameObject.GetComponent<PlayerMovement>().ChangeAnimation("resting_ani", 0.2f);
            collision.gameObject.GetComponent<PlayerMovement>().isDisabled = true;
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocityX = 0;
            // Salva o novo spawnpoint e desliga esse código para não rodar nada aqui novamente
            respawnable.SetSpawnPoint(transform);
            //Destroy(this.GetComponent<BoxCollider2D>());
            this.GetComponent < BoxCollider2D>().enabled = false;
            StartCoroutine(Waits());
            
        }

        IEnumerator Waits()
        {
            yield return new WaitForSeconds(4);
            collision.gameObject.GetComponent<PlayerMovement>().isDisabled = false;
            Destroy(this);
        }

    }

   
}
