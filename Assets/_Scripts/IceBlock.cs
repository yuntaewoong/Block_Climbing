using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlock : Blocks
{
    public float speedToFreezePlayer;
    private float iceContactTime;
    private Player player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.isPlayerOnIce = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        player.isPlayerOnIce = false;
    }
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (player.isAlive)
        {
            if (player.isPlayerOnIce && !player.isPlayerVeryVeryHot)
            {
                iceContactTime = Time.deltaTime;
                player.playerHeat -= iceContactTime * speedToFreezePlayer;
            }
        }
    }
}
//iceBlock은 플레이어가 밟을시 접촉한 시간만큼 피부가 파래지며(시간이 늦을수록 점점 파래짐) 일정시간이상 접촉하면 촹ㅊ!!!! 소리가 나면서
// 플레이어가 몇초간 제자리에 경직된다. + 얼음 특유의 미끄러지는 재질구현 경직지속시간이 끝나면 평범한 상태로 돌아온다.
