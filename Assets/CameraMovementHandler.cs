using UnityEngine;

public class CameraMovementHandler : MonoBehaviour
{
    [SerializeField] GameObject camera;
    [SerializeField] GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newVector = player.transform.position;
        newVector.z = -10f;
        camera.transform.position = newVector;
    }
}
