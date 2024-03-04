using UnityEngine;
public class CameraPaningSystem : MonoBehaviour
{
    public Transform player1; 
    public Transform player2; 
    public float minDistance = 5f; 
    public float maxDistance = 10f; 
    public float panSpeed = 1f; 

    private Vector3 initialPosition; 

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (player1 == null || player2 == null)
            return;
        
        float distanceBetweenPlayers = Vector3.Distance(player1.position, player2.position);
        float desiredDistance = Mathf.Clamp(distanceBetweenPlayers, minDistance, maxDistance);
        Vector3 targetPosition = initialPosition + /*(Vector3.back * desiredDistance)*/  (Vector3.up * desiredDistance);
        transform.position = Vector3.Lerp(transform.position, targetPosition, panSpeed * Time.deltaTime);
    }

}
