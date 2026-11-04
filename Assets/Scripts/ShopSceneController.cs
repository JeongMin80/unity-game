using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopSceneController : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string gameSceneName = "SampleScene"; // 너 GameScene 이름으로 바꾸기!

    public void EquipApple(int index)
    {
        SkinSettings.AppleSkinIndex = index;
        Debug.Log($"Apple skin selected: {index}");
    }

    public void EquipIceberg(int index)
    {
        SkinSettings.IcebergSkinIndex = index;
        Debug.Log($"Iceberg skin selected: {index}");
    }

    public void Back()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
