using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class IcebergSkinApplier : MonoBehaviour
{
    [SerializeField] private SkinCatalog skinCatalog;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Apply();
    }

    public void Apply()
    {
        if (skinCatalog == null || spriteRenderer == null) return;
        if (skinCatalog.icebergSkins == null || skinCatalog.icebergSkins.Length == 0) return;

        int idx = Mathf.Clamp(SkinSettings.IcebergSkinIndex, 0, skinCatalog.icebergSkins.Length - 1);
        Sprite sp = skinCatalog.icebergSkins[idx];
        if (sp != null) spriteRenderer.sprite = sp;
    }
}
