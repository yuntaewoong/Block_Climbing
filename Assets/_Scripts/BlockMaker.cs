using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMaker : MonoBehaviour
{
    public GameObject[] Blocks;
    public float blockAlphaValue;
    public Camera mainCamera;
    private Vector2 mousePosition;
    private GameObject currentBlock;
    private bool readyToNewOne = true;
    private SpriteRenderer spriteRenderer;

    private int ChooseBlockIndex()
    {
        return Random.Range(0, Blocks.Length);
    }
    private void MakeBlock(Vector2 mousePosition)
    {
        //마우스 위치에 블럭의 인스턴스 생성
        currentBlock = Instantiate(Blocks[ChooseBlockIndex()], mousePosition, Quaternion.identity);
        
        //알파값조절
        SpriteRenderer blockSpriteRenderer = currentBlock.GetComponent<SpriteRenderer>();
        blockSpriteRenderer.color = new Color(blockSpriteRenderer.color.r, blockSpriteRenderer.color.g, blockSpriteRenderer.color.b, blockAlphaValue);
        
        //collider2d의 istrigger값조절로 설치된블럭과 충돌 방지
        Collider2D[] collider = currentBlock.GetComponentsInChildren<Collider2D>();
        for(int a = 0; a < collider.Length;a++)
            collider[a].isTrigger = true;
        
        //클릭전까지는 더 만들지않도록
        readyToNewOne = false;
    }
    private void FollowingMouse(Vector2 mousePosition)
    {
        //currentBlock이 마우스위치 추적
        currentBlock.transform.position = mousePosition;
    }
    private void Depositiong()
    {
        // block들의 blocks 스크립트를 참조함.(모든블럭은 blocks를 상속함) 
        Blocks block = currentBlock.GetComponent<Blocks>();
        if (block.readyToBuild)
        {
            //블럭의 태그를 Block으로 설정(대쉬기능과 관련)
            currentBlock.tag = "Block";

            //블럭의 알파값을 원래대로 돌림
            SpriteRenderer blockSpriteRenderer = currentBlock.GetComponent<SpriteRenderer>();
            blockSpriteRenderer.color = new Color(blockSpriteRenderer.color.r, blockSpriteRenderer.color.g, blockSpriteRenderer.color.b, 255f);

            //새 블럭의 인스턴스를 만들수 있도록
            readyToNewOne = true;
            
            //블럭이 오브젝트들과 충돌할수 있도록 함.
            Collider2D[] collider = currentBlock.GetComponentsInChildren<Collider2D>();
            for (int a = 0; a < collider.Length; a++)
                collider[a].isTrigger = false;
        }
    }
    private void Update()
    {
        //마우스 좌표를 얻어온다
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (readyToNewOne)
        {
            MakeBlock(mousePosition);
        }
        FollowingMouse(mousePosition);
        if(Input.GetButtonDown("Fire1"))
        {
            Depositiong();
        }
    }
    






}

/*1. 플레이어로부터 마우스의 x좌표, y좌표를 입력받음. //
 * 2. 블럭은 alpha값이 줄어든상태로 다른 오브젝트들과 충돌하지않은 상태로 마우스의 좌표를 따라다님
 * 3. 마우스 좌클릭시 블럭의 alpha값은 255가되고 다른 오브젝트들과 충돌가능한 상태가 됨(다른 오브젝트와 겹치게
 * 되면 좌클릭해도 아무반응없도록)
 * 4. 놓을 블럭은 랜덤으로 선택
 * */
