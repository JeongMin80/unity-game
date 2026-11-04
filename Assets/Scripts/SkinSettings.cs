using UnityEngine;

public static class SkinSettings
{
    private const string AppleSkinKey = "APPLE_SKIN_INDEX";
    private const string IcebergSkinKey = "ICEBERG_SKIN_INDEX";

    public static int AppleSkinIndex
    {
        get => PlayerPrefs.GetInt(AppleSkinKey, 0);
        set
        {
            PlayerPrefs.SetInt(AppleSkinKey, Mathf.Max(0, value));
            PlayerPrefs.Save();
        }
    }

    public static int IcebergSkinIndex
    {
        get => PlayerPrefs.GetInt(IcebergSkinKey, 0);
        set
        {
            PlayerPrefs.SetInt(IcebergSkinKey, Mathf.Max(0, value));
            PlayerPrefs.Save();
        }
    }
}
