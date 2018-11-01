using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBlock : Blocks
{
    public float speedToHitPlayer;
    private float lavaContactTime;
    private Player player;

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.isPlayerOnLava = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        player.isPlayerOnLava = false;
    }
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent <Player>();
    }

    private void Update()
    {
        if(player.isPlayerOnLava && !player.isPlayerVeryVeryCold)
        {
            lavaContactTime = Time.deltaTime;
            player.playerHeat += lavaContactTime * speedToHitPlayer;
        }
        
    }
}
/* LavaBlock은 플레이어가 일정시간이상 밟으면 피부가 빨개지며 한계이상에 도달하면 이속과 점프력이 매우높아진다. 이 수치는 좌측에 UI로 구현한다.
 * Player와 연계된 변수를 사용한다. (ex.얼음을 밟아 피부가 파래진 상태이면 LavaBlock을 밟을때 평상시보다 더 오래 밟을 수 있다.)
 */