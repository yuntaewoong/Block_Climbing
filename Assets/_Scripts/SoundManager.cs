using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource _playerJump;
    public AudioSource _starGet;
    public AudioSource _gravityReverse;
    public AudioSource _spinDown;
    public AudioSource _iceTime;
    public AudioSource _playerDash;
    public AudioSource _fireMountain;
    public AudioSource _iceMountain;
    public AudioSource _deathMountain;


    public static AudioSource playerJump;
    public static AudioSource starGet;
    public static AudioSource gravityReverse;
    public static AudioSource spinDown;
    public static AudioSource iceTime;
    public static AudioSource playerDash;
    public static AudioSource fireMountain;
    public static AudioSource iceMountain;
    public static AudioSource deathMountain;

    private Player player;
    private bool deathCheck = true;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Awake()
    {
        playerJump = _playerJump;
        starGet = _starGet;
        gravityReverse = _gravityReverse;
        spinDown = _spinDown;
        iceTime = _iceTime;
        playerDash = _playerDash;
        fireMountain = _fireMountain;
        iceMountain = _iceMountain;
        deathMountain = _deathMountain;
    }

    private void Update()
    {
        if(!player.isAlive && deathCheck)
        {
            fireMountain.Stop();
            deathMountain.Stop();
            iceMountain.Stop();

            deathCheck = false;


        }
    }

}
