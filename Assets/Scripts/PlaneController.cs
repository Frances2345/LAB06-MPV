using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlaneController : MonoBehaviour
{
    public float speed = 25f;
    public float rotationSmoothness = 5f;
    public float tiltAngle = 35f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject gameOverPanel;

    private int lives = 3;
    private float score = 0f;
    private float initialRotationX;
    private bool isDead = false;

    void Awake()
    {
        initialRotationX = transform.rotation.eulerAngles.x;
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        UpdateUI();
    }

    void Update()
    {
        if (isDead) return;

        float leftRight = 0;
        float upDown = 0;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) leftRight -= 1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) leftRight += 1;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) upDown += 1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) upDown -= 1;
        }

        Vector3 direction = new Vector3(leftRight, upDown, 0);
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        float targetZ = -leftRight * tiltAngle;
        Quaternion targetRotation = Quaternion.Euler(initialRotationX, 0, targetZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothness);

        score += Time.deltaTime;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + ((int)score).ToString();
        if (livesText != null) livesText.text = "Lifes: " + lives.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Obstacle"))
        {
            lives--;
            Destroy(other.gameObject);
            UpdateUI();

            if (lives <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isDead = true;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}