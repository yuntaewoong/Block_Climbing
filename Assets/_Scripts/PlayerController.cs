using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public int movingSpeed;
    public int feverMovingSpeed;
    public int jumpingSpeed;
    public int feverJumpingSpeed;
    public int spinDownSpeed;
    public int feverSpinDownSpeed;
    public int dashSpeed;
    public float timeToGetBackToNormal;
    public float rayLength;
    public Animator anim;
    public bool isSpinDowning = false;

    private int remMovingSpeed;
    private int remJumpingSpeed;
    private int remSpinDownSpeed;
    private bool readyToJump = false;
    private Rigidbody2D rgb;
    private bool isReversed = false;
    private bool readyToDash = true;
    private bool isGettingBackMethodCalled = false;
    private Player player;

    public int RemMovingSpeed
    {
        get{return remMovingSpeed;}
    }


    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rgb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Player>();

        remMovingSpeed = movingSpeed;
        remJumpingSpeed = jumpingSpeed;
        remSpinDownSpeed = spinDownSpeed;

    }
    private void Update()
    {
        CheckingReverse();
        Move();
        if(Input.GetButtonDown("Fire3"))
            Dash();
        Jump();
        SpinDown();
        FeverTime();
        IceTime();
        if ((player.isPlayerVeryVeryHot || player.isPlayerVeryVeryCold) && !isGettingBackMethodCalled)
        {
            Invoke("GettingBackToNormal", timeToGetBackToNormal);
            isGettingBackMethodCalled = true;

            if(player.isPlayerVeryVeryCold)
                SoundManager.iceTime.Play();
        }
    }
    //player가 중력역전상태인지 매프레임마다 확인.
    private void CheckingReverse()
    {
        if (transform.localScale.y == -5)
            isReversed = true;
        else
            isReversed = false;
    }
    private void Move()
    {
        //입력
        float h = Input.GetAxisRaw("Horizontal");
        Vector2 movingVector = new Vector2(h, 0);
        //방향전환
        if (h > 0)
            transform.localScale = new Vector2(5, transform.localScale.y);
        else if(h < 0)
            transform.localScale = new Vector2(-5, transform.localScale.y);
        //움직임
        transform.Translate(movingVector * movingSpeed * Time.deltaTime);
        //애니변화
        if (h == 0)
            anim.SetBool("IsWalking", false);
        else
            anim.SetBool("IsWalking", true);
    }
    private void Jump()
    {
        //더블점프방지
        if (!readyToJump)
            return;
        bool isSpacebarClicked = Input.GetButton("Jump");
        if (isSpacebarClicked)
        {
            //뒤집힌 상태와 일반상태로 나누어 점프벡터를 할당
            Vector2 jumpingVector;
            if (!isReversed)
                jumpingVector = new Vector2(0, jumpingSpeed);
            else
                jumpingVector = new Vector2(0, -jumpingSpeed);
            
            //일정한 크기로 점프하기위해 rigidbody의 velocity 초기화진행
            rgb.velocity = new Vector2(0, 0);

            //점프
            rgb.AddForce(jumpingVector, ForceMode2D.Impulse);
            SoundManager.playerJump.Play();
        }
    }
    private void Dash()
    {
        //ray를 쏴서 Dash가능한지 파악
        int layerMask = LayerMask.NameToLayer("Player");
        Ray2D ray = new Ray2D(transform.position, new Vector2(transform.localScale.x,0));
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction,rayLength,layerMask);

        //player와 hit사이의 거리가 일정수준 미만이면 Dash차단 그리고 이단대쉬 방지
        if (!readyToDash || (hit.collider.tag == "Block" && Mathf.Abs((hit.transform.position - transform.position).x) <= dashSpeed + 2))  
            return;
        if (transform.localScale.x == -5)
            transform.Translate(new Vector2(-dashSpeed, 0));
        else
            transform.Translate(new Vector2(dashSpeed, 0));
        readyToDash = false;

        SoundManager.playerDash.Play();
    }
    //바닥과 충돌체크(stay와 exit2가지 사용)
    private void OnCollisionStay2D(Collision2D collision)
    {
        //충돌 오브젝트가 "Block"일때만
        if (collision.gameObject.tag == "Block")
        {
            readyToJump = true;
            readyToDash = true;
            anim.SetBool("IsJumping", false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            isSpinDowning = false;
            readyToJump = false;
            //애니변화
            anim.SetBool("IsJumping", true);
        }
    }    
    //드릴처럼 바닥을향해 뚫을듯이 돌진하는메서드.    
    private void SpinDown()
    {
        if (readyToJump)
            return;
        //수직입력
        bool v = Input.GetButtonDown("Vertical");
        //아래화살표입력을 받고 SpinDown을 하지않을때만 + player가 살아있을때만
        if (v  && !isSpinDowning && player.isAlive)
        {
            Vector2 downVector;
            if (!isReversed)
                downVector = new Vector2(0, spinDownSpeed);
            else
                downVector = new Vector2(0, -spinDownSpeed);
            
            rgb.AddForce(downVector, ForceMode2D.Impulse);
            isSpinDowning = true;

            SoundManager.spinDown.Play();
        }
    }
    private void FeverTime()
    {
        if(player.isPlayerVeryVeryHot)
        {
            movingSpeed = feverMovingSpeed;
            jumpingSpeed = feverJumpingSpeed;
            spinDownSpeed = feverSpinDownSpeed;
        }
    }
    private void IceTime()
    {
        if (player.isPlayerVeryVeryCold)
        {
            movingSpeed = 0;
            jumpingSpeed = 0;
        }
    }
    public  void GettingBackToNormal()
    {
        movingSpeed = remMovingSpeed;
        jumpingSpeed = remJumpingSpeed;
        spinDownSpeed = remSpinDownSpeed;
        anim.enabled = true;
        player.playerHeat = 0;
        player.isPlayerVeryVeryHot = false;
        player.isPlayerVeryVeryCold = false;
        isGettingBackMethodCalled = false;
    }


}
