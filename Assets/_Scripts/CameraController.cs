using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int minimumSpeed;
    public int maximumSpeed;
    public int minimumLastTime;
    public int maximumLastTime;
    public static int cameraSpeed;
    public static Vector2 cameraDirect;
    public static float cameraDirectTime;
    public static float remainingTimeBeforeChange;

    private Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        StartCoroutine("UpdateNewCameraMove");
        StartCoroutine("UpdateRemainingTime");
    }
    //카메라의 랜덤이동방향
    Vector2 RandomlyChoosingDirect()
    {
        int oneOrMinusOne = 0;
        while(oneOrMinusOne == 0)
            oneOrMinusOne = Random.Range(-1, 2);

        return new Vector2(oneOrMinusOne, Random.Range(-2f, 2f)).normalized;
    }
    //한방향으로 어느시간동안 이동할지
    int HowManyTimeToMove()
    {
        return Random.Range(minimumLastTime, maximumLastTime);
    }
    //어떤 스피드로 이동할지
    int HowBigSpeed()
    {
        return Random.Range(minimumSpeed, maximumSpeed);
    }
    //설정된 시간만큼 시간간격마다 카메라의 방향,속도,지속시간을 재설정해주는 코루틴
    IEnumerator UpdateNewCameraMove()
    {
        while (player.isAlive)
        {
            cameraDirect = RandomlyChoosingDirect();
            cameraDirectTime = (float)HowManyTimeToMove();
            remainingTimeBeforeChange = cameraDirectTime + 1;
            cameraSpeed = HowBigSpeed();
            yield return new WaitForSeconds(cameraDirectTime);
        }
    }
    IEnumerator UpdateRemainingTime()
    {
        while (player.isAlive)
        {
            remainingTimeBeforeChange -= 1;
            yield return new WaitForSeconds(1);
        }
    }

	void LateUpdate ()
    {
        transform.Translate(cameraDirect * Time.deltaTime * cameraSpeed);	
	}
}
/*
 * 
 * GUI로 화면 우측하단에 현재 향하는 방향과 방향전환까지 남은시간 표시.
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
*/