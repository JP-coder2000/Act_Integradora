using UnityEngine;
using TMPro;

public class UIBulletCounter : MonoBehaviour
{
    public TextMeshProUGUI bulletCountText;

    void Update()
    {
        if (bulletCountText != null)
        {
            bulletCountText.text = "Enemies: " + BulletCounter.Count;
        }
    }
}
