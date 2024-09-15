using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * 100);
    }
}
