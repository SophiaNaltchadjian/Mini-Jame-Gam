using UnityEngine;

public class CameraMovementHandler : MonoBehaviour
{
    [SerializeField] GameObject CameraObject;
    [SerializeField] GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    [SerializeField] float smoothSpeed = 5f;

    void LateUpdate()
    {
        Vector3 target = player.transform.position;
        target.z = -10f;
        CameraObject.transform.position = Vector3.Lerp(CameraObject.transform.position, target, smoothSpeed * Time.deltaTime);
    }
}
