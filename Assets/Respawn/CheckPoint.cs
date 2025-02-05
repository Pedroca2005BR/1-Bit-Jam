using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se o objeto de colisão tiver o componente IRespawnable, entra no if
        if (collision.gameObject.TryGetComponent<IRespawnable>(out var respawnable))
        {
            // Salva o novo spawnpoint e desliga esse código para não rodar nada aqui novamente
            respawnable.SetSpawnPoint(transform);
            Destroy(this);
        }
    }
}
