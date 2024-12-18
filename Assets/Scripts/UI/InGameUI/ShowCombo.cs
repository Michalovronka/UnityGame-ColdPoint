using TMPro;
using UnityEngine;


public class ShowCombo : MonoBehaviour
{
    public TextMeshProUGUI uiText;
    public PlayerController player;
    void Update()
    {
        if (player.combo > 1)
        {
            uiText.text = player.combo.ToString();
            return;
        }
        uiText.text = "";
    }
}
