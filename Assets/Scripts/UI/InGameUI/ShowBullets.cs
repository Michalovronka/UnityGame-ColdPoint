using TMPro;
using UnityEngine;

public class ShowBullets : MonoBehaviour
{
    public TextMeshProUGUI uiText;
    public PlayerController player;
    void Update()
    {
        if (player.weapon != null)
        {
            uiText.text = $"{player.weapon.numOfBullets} / {player.weapon.maxNumOfBullets}";
            return;
        }
        uiText.text = "";
    }
}
