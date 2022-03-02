using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public JohnMovement John;
    public TextMeshProUGUI HealthText;


    void Update()
    {
        HealthText.text = John.Health.ToString();
    }
}
