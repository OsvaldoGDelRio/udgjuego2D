using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public JohnMovement John;
    public TextMeshProUGUI HealthText;

    void Update()
    {
        if(John != null)
        {
            HealthText.text = John.Health.ToString(); 
        }
        
    }
}
