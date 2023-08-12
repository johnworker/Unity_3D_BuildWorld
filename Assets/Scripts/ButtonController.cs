using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public DessertManager dessertManager; // 引用 DessertManager 脚本

    private void Start()
    {
        dessertManager = FindObjectOfType<DessertManager>();
    }

    public void OnButtonClick()
    {
        dessertManager.DessertDown();
    }
}