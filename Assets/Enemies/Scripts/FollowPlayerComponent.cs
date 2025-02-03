using UnityEngine;

public class FollowPlayerComponent : MonoBehaviour
{
    [SerializeField] private float _followSpeed;
    [SerializeField] private float _followDistance;

    private void Start()
    {
        // TO DO: Get reference to player
    }


    private void Update()
    {
        // TO DO: Compare distance with player to see if is in follow distance
        
        // TO DO: Use rb.velocity to follow player.
    }
}
