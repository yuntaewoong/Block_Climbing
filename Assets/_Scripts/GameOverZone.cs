using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverZone : MonoBehaviour
{
    private Player player;
    private BoxCollider2D playerBoxCollider;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        playerBoxCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player.isAlive = false;
            Destroy(playerBoxCollider);
        }
    }
}
