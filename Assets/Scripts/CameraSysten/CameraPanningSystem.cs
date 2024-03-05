using UnityEngine;
public class CameraPanningSystem : MonoBehaviour
{
    [SerializeField] private Transform player1; 
    [SerializeField] private Transform platform; 
    [SerializeField] private Transform player2; 
    [SerializeField] private float minDistance = 5f; 
    [SerializeField] private float maxDistance = 10f; 
    [SerializeField] private float panSpeed = 1f;
    [SerializeField] private float magnitudeFactor = 3f;

    
    void Update()
    {
        var position = player1.position;
        var position1 = player2.position;
        
        Vector3 middlePoint = (position + position1 + platform.position) / 3f;
        float distanceBetweenPlayers = Vector3.Distance(position, position1);
        float desiredDistance = Mathf.Clamp(distanceBetweenPlayers, minDistance, maxDistance);

        var transform1 = transform;
        Vector3 targetPosition = middlePoint - transform1.forward * (desiredDistance + magnitudeFactor);
        transform.position = Vector3.Lerp(transform1.position, targetPosition, panSpeed * Time.deltaTime);
    }

}
