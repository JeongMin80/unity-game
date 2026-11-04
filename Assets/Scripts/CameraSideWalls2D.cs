using UnityEngine;

public class CameraSideWalls2D : MonoBehaviour
{
    [Header("Wall Size")]
    [SerializeField] private float wallThickness = 1f;
    [SerializeField] private float wallHeightExtra = 5f; // 화면보다 조금 더 높게

    [Header("Offset Outside Camera")]
    [SerializeField] private float outsideOffset = 0.2f;

    private void Awake()
    {
        CreateSideWalls();
    }

    private void CreateSideWalls()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("Main Camera not found.");
            return;
        }

        if (!cam.orthographic)
        {
            Debug.LogError("Camera must be Orthographic for 2D bounds calculation.");
            return;
        }

        float camHalfHeight = cam.orthographicSize;
        float camHalfWidth = camHalfHeight * cam.aspect;

        float wallHeight = (camHalfHeight * 2f) + wallHeightExtra;

        // Left wall
        GameObject left = new GameObject("LeftWall");
        left.transform.SetParent(transform);
        left.transform.position = new Vector3(cam.transform.position.x - camHalfWidth - outsideOffset, cam.transform.position.y, 0f);
        BoxCollider2D leftCol = left.AddComponent<BoxCollider2D>();
        leftCol.size = new Vector2(wallThickness, wallHeight);

        // Right wall
        GameObject right = new GameObject("RightWall");
        right.transform.SetParent(transform);
        right.transform.position = new Vector3(cam.transform.position.x + camHalfWidth + outsideOffset, cam.transform.position.y, 0f);
        BoxCollider2D rightCol = right.AddComponent<BoxCollider2D>();
        rightCol.size = new Vector2(wallThickness, wallHeight);
    }
}
