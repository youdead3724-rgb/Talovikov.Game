using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Text scoreText;
    [SerializeField] private float lowerDeathY = -4.8f;
    [SerializeField] private float upperDeathY = 4.8f;

    private Rigidbody2D rb;
    private int score;
    private bool gameOver;

    private void Awake()
    {
        Time.timeScale = 1f;
        score = 0;
        gameOver = false;
        rb = GetComponent<Rigidbody2D>();

        if (scoreText != null)
        {
            scoreText.text = "Score: 0";
        }
    }

    private void Update()
    {
        // These controls still work while Time.timeScale is 0, so the player
        // can restart or exit after losing.
        if (gameOver)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitGame();
            }

            return;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(0f, jumpForce);
        }

        // There is no ground collider in the original scene, so explicitly
        // treat leaving the camera bounds as a loss instead of letting the
        // bird fall forever.
        if (transform.position.y <= lowerDeathY || transform.position.y >= upperDeathY)
        {
            GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameOver && collision.transform.CompareTag("Enemy"))
        {
            GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameOver && collision.transform.CompareTag("Score"))
        {
            score++;

            if (scoreText != null)
            {
                scoreText.text = $"Score: {score}";
            }
        }
    }

    private void GameOver()
    {
        if (gameOver)
        {
            return;
        }

        gameOver = true;
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;
        Time.timeScale = 0f;

        if (scoreText != null)
        {
            scoreText.text = $"Game Over!\nScore: {score}\nClick / Space to restart";
        }
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void QuitGame()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
