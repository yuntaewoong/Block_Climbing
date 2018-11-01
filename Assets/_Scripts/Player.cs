using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isAlive = true;
    public bool isPlayerOnLava = false;
    public bool isPlayerOnIce = false;
    public bool isPlayerVeryVeryHot = false;
    public bool isPlayerVeryVeryCold = false;
    public float playerHeat;
    public float naturalRestoreSpeed;
    public float maximumPlayerHeat;
    public float minimumPlayerHeat;
    public Sprite iceImage;
    public SpriteRenderer spr;
    public Animator anim;
   
    private void Update()
    {
        SettingMaximumMinimumPlayerHit();
        ChangingPlayerSkin();
        NaturalRestore();
    }

    private void ChangingPlayerSkin()
    {
        if(playerHeat > 0)
        {
            spr.color = Color.Lerp(new Color(1, 1, 1,1), new Color(1, 0, 0,1), playerHeat/maximumPlayerHeat);
        }
        else
        {
            spr.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(0, 0.5f, 1, 1), playerHeat / minimumPlayerHeat);
        }
    }
    private void SettingMaximumMinimumPlayerHit()
    {
        if (playerHeat <= maximumPlayerHeat && playerHeat >= minimumPlayerHeat)
            return;
        if (playerHeat > maximumPlayerHeat)
        {
            playerHeat = maximumPlayerHeat;
            isPlayerVeryVeryHot = true;
        }
        else
        {
            playerHeat = minimumPlayerHeat;
            isPlayerVeryVeryCold = true;


            anim.enabled = false;
            spr.sprite = iceImage;
        }
    }

    private void NaturalRestore()
    {
        if(!isPlayerOnIce && !isPlayerOnLava && !isPlayerVeryVeryHot && !isPlayerVeryVeryCold) 
        {
            if(playerHeat>0)
            {
                playerHeat -= naturalRestoreSpeed * Time.deltaTime;
            }
            else
            {
                playerHeat += naturalRestoreSpeed * Time.deltaTime;
            }
        }
    }

}
