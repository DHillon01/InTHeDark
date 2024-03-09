using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _progress;

    [SerializeField] private float _speed = 40f;

    // Start is called before the first frame update
    void Start()
    {
        // Destroy the bullet after a certain time (optional)
        Destroy(gameObject, 2f); // Adjust the time as needed
    }

    // Update is called once per frame
    void Update()
    {
        _progress += Time.deltaTime * _speed;
        transform.position = Vector3.Lerp(_startPosition, _targetPosition, _progress);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        // Get the forward direction of the game object (assuming the gun is attached to this object)
        Vector3 forwardDirection = transform.up;

        // Calculate the target position based on the forward direction and a distance (adjust distance as needed)
        _targetPosition = transform.position + forwardDirection * 20f;

        // Set the starting position as the current position
        _startPosition = transform.position;
    }
  
}
