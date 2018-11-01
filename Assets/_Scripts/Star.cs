using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    
    public float timeBeforeShow;
    public SpriteRenderer spr;

    private float timeAfterSpawn;
    private UiManager ui;
    private StarMaker starMaker;

    private void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UiManager>();
        starMaker = GameObject.Find("StarMaker").GetComponent<StarMaker>();
    }


    private void Update()
    {
        //별이 생성되고 일정시간후에 눈에보이도록함 (별이 재생성될때 화면 여기저기에 지저분하게 별이 나타났다가 사라지는 현상방지)
        timeAfterSpawn += Time.deltaTime;
        if(timeAfterSpawn > timeBeforeShow)
        {
            spr.enabled = true;
        }
    }
    


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            Destroy(gameObject);
            starMaker.readyToGet = false;
        }
        if (collision.tag == "Player" || collision.tag == "GameZone")
        {
            Destroy(gameObject);
            starMaker.readyToGet = false;
            if (collision.tag == "Player")
            {
                ui.takenStars += 1;
                SoundManager.starGet.Play();
            }
        }
    }
    
}
