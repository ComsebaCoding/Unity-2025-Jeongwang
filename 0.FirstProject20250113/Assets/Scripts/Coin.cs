using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // if (other.gameObject.CompareTag("Enemy")) return;
        Vector3 nextPosition = new Vector3(
            Random.Range(-4.5f, 4.5f),
            1.0f,
            Random.Range(-4.5f, 4.5f)
            );
        transform.position = nextPosition;

        if (other.gameObject.CompareTag("Player"))
            GameManager.score += 10;
    }
}
