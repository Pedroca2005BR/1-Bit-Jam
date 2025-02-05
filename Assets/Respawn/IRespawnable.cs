using System.Collections;
using UnityEngine;

public interface IRespawnable
{
    public void SetSpawnPoint(Transform transform);
    public IEnumerator Respawn();
}
