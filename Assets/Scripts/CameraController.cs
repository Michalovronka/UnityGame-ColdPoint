using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float displacementMultiplier = 0.10f;
    private const float ZPosition = -10;
    
    private void Update()
    {
        if (playerTransform != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 cameraDisplacement = (mousePosition - playerTransform.position) * displacementMultiplier;

            Vector3 finalCameraPosition = playerTransform.position + cameraDisplacement;
            finalCameraPosition.z = ZPosition;
            transform.position = finalCameraPosition;
        }
        
    }
}
