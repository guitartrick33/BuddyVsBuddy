using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHit : MonoBehaviour
{
    public float destructionTimer = 1f;
    void Update()
    {
        destructionTimer -= Time.deltaTime;
        if (destructionTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
