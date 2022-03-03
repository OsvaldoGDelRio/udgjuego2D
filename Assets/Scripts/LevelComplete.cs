using UnityEngine;
using System.Collections;

public class LevelComplete : MonoBehaviour
{
    public GameManagment managment;

    void OnTriggerEnter2D(Collider2D colliderInfo)
    {
        if(colliderInfo.tag == "Player")
        {
            managment.LevelComplete();
            Destroy(gameObject);
        }
    }
}
