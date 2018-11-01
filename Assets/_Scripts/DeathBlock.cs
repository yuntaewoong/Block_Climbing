using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBlock : MonoBehaviour
{
    private Player player;
    private BoxCollider2D playerBoxCollider;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        playerBoxCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.isAlive = false;
            Destroy(playerBoxCollider);
            
        }
    }
}
