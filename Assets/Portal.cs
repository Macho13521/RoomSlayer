using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Statistics playerStats = other.gameObject.GetComponent<Statistics>();
        if(playerStats != null)
        {
            GameManager.Instance.changeFloorLevel();
        }
    }
}
