using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] 
    private float moveSpeed = 7f;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameManager gameManager;

    private Rigidbody2D rb;
    private float inputX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (gameManager == null)
            gameManager = GameManager.Instance;

        if (gameManager != null)
        {
            gameManager.OnGameOver += OnGameOver;
            gameManager.Continuegame += Continuegame;
        }
    }

    private void Continuegame()
    {
        animator.SetBool("Dead", false);
    }

    private void OnDestroy()
    {
        if (gameManager == null) return;
        gameManager.OnGameOver -= OnGameOver;
        gameManager.Continuegame -= Continuegame;
    }

    private void OnGameOver()
    {
        animator.SetBool("Dead", true);
    }

    private void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsPlaying())
        {
            inputX = 0f;
            return;
        }

        inputX = Input.GetAxisRaw("Horizontal");
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsPlaying())
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 v = rb.velocity;
        v.x = inputX * moveSpeed;
        v.y = 0f;
        rb.velocity = v;
    }
}
