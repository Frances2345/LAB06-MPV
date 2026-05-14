using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public float scrollSpeed = 20f;

    void Update()
    {
        transform.Rotate(Vector3.right * scrollSpeed * Time.deltaTime);
    }
}
