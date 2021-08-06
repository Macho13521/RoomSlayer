using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashParticle : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1.0f);
    }
}
