using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float moveSpeed = 40f;
    public float rotationSpeed = 50f;
    private Vector3 rotationAxis;

    void Start()
    {
        rotationAxis = new Vector3(Random.value, Random.value, Random.value);
    }

    void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);

        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);

        if (transform.position.z < -500f)
        {
            Destroy(gameObject);
        }
    }
}