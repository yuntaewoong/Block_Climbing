using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityReverseBlock : Blocks
{
    private Rigidbody2D playerRgb;
    private Transform playerTransform;
    private int touchNum;
    public SpriteRenderer[] retryButtonRenderer;
    public int maximumTouchNum;

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //한번 충돌을 감지하면 아래코드는 더이상 실행되지 않음
        if (!(touchNum == maximumTouchNum))
        {
            playerRgb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerTransform = collision.gameObject.GetComponent<Transform>();

            //player의 중력을 전환시킴
            playerRgb.gravityScale = -playerRgb.gravityScale;

            //player의 방향을 전환시킴
            playerTransform.localScale = new Vector2(playerTransform.localScale.x, -playerTransform.localScale.y);


            //속도증폭 방지
            playerRgb.velocity = new Vector2(0, 0);

            //중력전환가능 아이콘을 더이상 안보이도록 처리
            retryButtonRenderer[touchNum].color = new Color(255f, 255f, 255f, 0f);

            SoundManager.gravityReverse.Play();


            touchNum += 1;
        }
    }
}
