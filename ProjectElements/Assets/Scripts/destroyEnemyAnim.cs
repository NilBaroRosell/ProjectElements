using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyEnemyAnim : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    
    public void stopEnemy()
    {
        enemy.transform.GetChild(0).GetComponent<EnemieController>().enabled = false;
        enemy.transform.GetChild(0).GetComponent<Rigidbody>().AddForce((Vector3.left + Vector3.up).normalized * 10, ForceMode.Impulse);
        enemy.transform.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }

    public void startEnemy()
    {
        enemy.transform.GetChild(1).position = enemy.transform.GetChild(0).position;
        enemy.transform.GetChild(2).position = enemy.transform.GetChild(0).position + Vector3.right * 5.0f;
        enemy.transform.GetChild(0).GetComponent<EnemieController>().enabled = true;
        enemy.transform.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
    }
}
