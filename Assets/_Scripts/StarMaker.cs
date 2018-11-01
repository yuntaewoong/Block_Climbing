using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMaker : MonoBehaviour
{
    public GameObject star;
    public GameObject player;
    public float lengthBetweenStars;
    public bool readyToGet = false;
    private Vector2 starPosition;
    private Vector2 lastStarPosition;

    private void Start()
    {
        lastStarPosition = Vector2.zero;;
    }

    private void Update()
    {
        if (readyToGet)
            return;
        do
        {
            starPosition = MakingStarposition();
        }
        while ((starPosition - lastStarPosition).magnitude < lengthBetweenStars);
        Instantiate(star, starPosition, Quaternion.identity);
        lastStarPosition = starPosition;
        readyToGet = true;
    }

    private Vector2 MakingStarposition()
    {
        if (CameraController.cameraDirect.x >= 0)
        {
            return new Vector2(Random.Range(-6, 10), Random.Range(-6, 10)) + (Vector2)transform.position;
        }
        else
        {
            return new Vector2(Random.Range(-10,6), Random.Range(-10, 6)) + (Vector2)transform.position;
        }
    }
}

/*먹을때마다 리젠되는 별을 만든다.
 * 이 별은 완전랜덤한 위치가 아닌 camera기준으로 봤을때 player가오른쪽위에있다면 왼쪽아래로 왼쪽위에있다면 오른쪽아래로
 * 생성됨
 */
