using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StickyBlock : Blocks
{
    public int stickyMovingSpeed;
    private PlayerController pc;
    private int beforeMovingSpeed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        pc = collision.gameObject.GetComponent<PlayerController>();
        beforeMovingSpeed = pc.RemMovingSpeed;
        pc.movingSpeed = stickyMovingSpeed;
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        pc.movingSpeed = beforeMovingSpeed;
    }
}
