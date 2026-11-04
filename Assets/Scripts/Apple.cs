using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private float maxLifeTime = 15f;

    [Header("Skin")]
    [SerializeField] private SkinCatalog skinCatalog;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ApplySkin();
        Destroy(gameObject, maxLifeTime);
    }

    private void ApplySkin()
    {
        if (skinCatalog == null || spriteRenderer == null) return;
        if (skinCatalog.appleSkins == null || skinCatalog.appleSkins.Length == 0) return;

        int idx = Mathf.Clamp(SkinSettings.AppleSkinIndex, 0, skinCatalog.appleSkins.Length - 1);
        Sprite sp = skinCatalog.appleSkins[idx];
        if (sp != null) spriteRenderer.sprite = sp;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            GameManager.Instance.GameOver();

        if (other.CompareTag("Ground"))
            Destroy(gameObject);
    }
}
