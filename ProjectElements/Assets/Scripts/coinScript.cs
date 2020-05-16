using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinScript : MonoBehaviour
{
    [SerializeField] inventory inv;
    [SerializeField] GameObject coin;
    [SerializeField] Transform player;

    private void Update()
    {
        if(Vector2.Distance(player.transform.position, transform.position) < (GetComponent<CircleCollider2D>().radius / 2.0f))
        {
            inv.updateCoins(coin);
            Destroy(gameObject);
        }
    }
}
