using System.Collections;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Stage
    {
        public string stageName; // 단계 구분을 위한 이름
        public float startTime;
        public float endTime;

        [Header("Spawn Interval (Random Range)")]
        public float minInterval = 0.5f; // 최소 대기 시간
        public float maxInterval = 1.0f; // 최대 대기 시간

        [Header("Fall Speed")]
        public float gravityScale = 1.0f;
    }

    [SerializeField] private Stage[] stages;
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private float spawnYAboveCamera = 1.0f;

    private Camera cam;
    private float survivedTime;
    private float nextSpawnTime; // 다음 스폰까지 남은 시간
    private Stage currentStage;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        SetNextSpawnInterval();
    }

    private void Update()
    {
        if (GameManager.Instance == null || !GameManager.Instance.IsPlaying())
            return;

        survivedTime += Time.deltaTime;
        UpdateStage();

        // 타이머 체크
        nextSpawnTime -= Time.deltaTime;
        if (nextSpawnTime <= 0f)
        {
            SpawnOneApple();
            SetNextSpawnInterval(); // 스폰 후 다음 랜덤 시간 설정
        }
    }

    private int stageIndex = -1;

    private void UpdateStage()
    {
        int newIndex = stages.Length - 1;
        for (int i = 0; i < stages.Length; i++)
        {
            if (survivedTime >= stages[i].startTime && survivedTime < stages[i].endTime)
            {
                newIndex = i;
                break;
            }
        }

        // 스테이지가 바뀌면 간격을 새로 뽑아서 즉시 반영
        if (newIndex != stageIndex)
        {
            stageIndex = newIndex;
            currentStage = stages[stageIndex];
            SetNextSpawnInterval(); // <- 전환 순간 리듬 갱신
            return;
        }

        currentStage = stages[stageIndex];
    }

    private void SetNextSpawnInterval()
    {
        if (currentStage == null) UpdateStage();
        // 다음 사과가 나올 때까지의 시간을 랜덤하게 설정 (이게 핵심!)
        nextSpawnTime = Random.Range(currentStage.minInterval, currentStage.maxInterval);
    }

    private void SpawnOneApple()
    {
        if (applePrefab == null) return;

        float x = GetRandomX();
        float y = GetSpawnY();

        GameObject apple = Instantiate(applePrefab, new Vector3(x, y, 0f), Quaternion.identity);
        Rigidbody2D rb = apple.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = currentStage.gravityScale;
        }
    }

    private float GetRandomX()
    {
        float halfH = cam.orthographicSize;
        float halfW = halfH * cam.aspect;
        // 여유를 주기 위해 화면 끝보다 0.5 정도 안쪽에서 생성
        return Random.Range(cam.transform.position.x - halfW + 0.5f, cam.transform.position.x + halfW - 0.5f);
    }

    private float GetSpawnY()
    {
        return cam.transform.position.y + cam.orthographicSize + spawnYAboveCamera;
    }

    public void ResetSpawner()
    {
        survivedTime = 0f;
        currentStage = null;    // <- 강제로 null
        SetNextSpawnInterval(); // <- 내부에서 UpdateStage() 타도록
    }
}