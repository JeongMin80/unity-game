using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { Title, Playing, GameOver }
    public GameState State { get; private set; } = GameState.Title;

    [Header("UI Panels")]
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private AppleSpawner spawner;

    [Header("HUD Text")]
    [SerializeField] private TMP_Text timeText;      // 현재 기록
    [SerializeField] private TMP_Text bestText;      // 최고 기록(HUD에 표시)

    [Header("GameOver Text")]
    [SerializeField] private TMP_Text resultText;    // GameOverPanel 현재 기록
    [SerializeField] private TMP_Text bestResultText; // (선택) GameOverPanel 최고 기록

    public event Action OnGameOver;
    public event Action Continuegame;

    private float elapsedTime;   // 이번 시도 기록(초)
    private float bestTime;      // 최고 기록(초) - QuitToTitle에서만 초기화

    private const string BestTimeKey = "BEST_TIME";

    [Header("Scenes")]
    [SerializeField] private string shopSceneName = "ShopScene";

    private Vector3 playerStartPos;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (player != null) playerStartPos = player.position;

        // 저장된 Best 불러오기
        bestTime = PlayerPrefs.GetFloat(BestTimeKey, 0f);

        RefreshAllTexts();

        // 처음엔 타이틀로 (Best 유지)
        GoToTitle(resetBest: false);
    }

    public bool IsPlaying()
    {
        return State == GameState.Playing;
    }

    // UI 버튼: Start
    public void StartGame()
    {
        elapsedTime = 0f;

        ClearApples();
        ResetPlayer();

        State = GameState.Playing;
        SetPanels(title: false, hud: true, gameOver: false);

        if (spawner != null) spawner.ResetSpawner();

        RefreshAllTexts();
    }

    // Apple에 맞으면 호출
    public void GameOver()
    {
        if (State != GameState.Playing) return;

        if (elapsedTime > bestTime)
        {
            bestTime = elapsedTime;
            PlayerPrefs.SetFloat(BestTimeKey, bestTime);
            PlayerPrefs.Save();
        }

        State = GameState.GameOver;
        SetPanels(title: false, hud: false, gameOver: true);

        RefreshAllTexts();
        OnGameOver?.Invoke();
    }

    // UI 버튼: Continue
    public void ContinueGame()
    {
        // 이번 시도 기록만 초기화, Best는 유지
        elapsedTime = 0f;

        ClearApples();
        ResetPlayer();

        State = GameState.Playing;
        SetPanels(title: false, hud: true, gameOver: false);

        if (spawner != null) spawner.ResetSpawner();

        RefreshAllTexts();

        Continuegame?.Invoke();
    }

    public void QuitToTitle()
    {
        GoToTitle(resetBest: false);
    }

    // 실제 앱 종료 버튼(있다면)
    public void QuitGame()
    {
        // Quit에서만 전체 초기화
        PlayerPrefs.DeleteKey(BestTimeKey);
        PlayerPrefs.DeleteKey("APPLE_SKIN_INDEX");
        PlayerPrefs.DeleteKey("ICEBERG_SKIN_INDEX");
        PlayerPrefs.Save();

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // 내부: 타이틀로 이동
    private void GoToTitle(bool resetBest)
    {
        elapsedTime = 0f;

        if (resetBest)
            bestTime = 0f;

        ClearApples();
        ResetPlayer();

        State = GameState.Title;
        SetPanels(title: true, hud: false, gameOver: false);

        if (spawner != null) spawner.ResetSpawner();

        RefreshAllTexts();
    }

    private void Update()
    {
        if (State != GameState.Playing) return;

        elapsedTime += Time.deltaTime;

        // HUD 텍스트 둘 다 갱신
        if (timeText != null)
            timeText.text = $"TIME {elapsedTime:0.0}s";

        if (bestText != null)
            bestText.text = $"BEST {bestTime:0.0}s";
    }

    // 텍스트들을 한 번에 정리해서 갱신
    private void RefreshAllTexts()
    {
        if (timeText != null)
            timeText.text = $"TIME {elapsedTime:0.0}s";

        if (bestText != null)
            bestText.text = $"BEST {bestTime:0.0}s";

        if (resultText != null)
            resultText.text = $"TIME {elapsedTime:0.0}s";

        if (bestResultText != null)
            bestResultText.text = $"BEST {bestTime:0.0}s";
    }

    private void SetPanels(bool title, bool hud, bool gameOver)
    {
        if (titlePanel != null) titlePanel.SetActive(title);
        if (hudPanel != null) hudPanel.SetActive(hud);
        if (gameOverPanel != null) gameOverPanel.SetActive(gameOver);
    }

    private void ResetPlayer()
    {
        if (player == null) return;
        player.position = playerStartPos;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // ✅ 추가: 죽음 애니메이션 상태 리셋
        Animator anim = player.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetBool("Dead", false);
            // 필요하면 현재 상태를 즉시 반영
            anim.Rebind();
            anim.Update(0f);
        }
    }


    private void ClearApples()
    {
        Apple[] apples = FindObjectsOfType<Apple>();
        for (int i = 0; i < apples.Length; i++)
        {
            Destroy(apples[i].gameObject);
        }
    }
    // GameOver 화면에서 "Title" 버튼을 눌렀을 때
    public void BackToTitle()
    {
        GoToTitle(resetBest: false);
    }

    public void OpenShop()
    {
        SceneManager.LoadScene(shopSceneName);
    }
}
