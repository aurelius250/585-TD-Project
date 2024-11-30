using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{

    [Header("References")]
    [SerializeField] TextMeshProUGUI moneyUI;

    private void OnGUI()
    {
        moneyUI.text = LevelManager.main.money.ToString();
    }

    public void SetSelected()
    {

    }
}
