using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UiManager : MonoBehaviour
{
    public int takenStars = 0;
    public Text starText;
    public Text directTimeText;
    public Slider slider;
    public Image sliderFillImage;
    public RectTransform arrow;
    public Image panealImage;
    public Text gameOverText;
    public Text highScoreText;
    private Player player;
    private int highScore = 0;
    

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        starText.gameObject.SetActive(true);
        arrow.gameObject.SetActive(true);
        slider.gameObject.SetActive(true);
        panealImage.gameObject.SetActive(false);
        
    }
    private void Update()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScore <= takenStars)
        {
            PlayerPrefs.SetInt("HighScore", takenStars);
            PlayerPrefs.Save();
        }
        ShowStar();
        ShowArrow();
        ShowSlider();
        ChangingToGameOver();
        ShowResult();
        
    }
    private void ShowStar()
    {
        starText.text = takenStars + "개";
    }
    private void ShowArrow()
    {
        arrow.right = -CameraController.cameraDirect;
        arrow.localScale = new Vector3(CameraController.cameraSpeed / 3f, 0.4f,1);
        
    }
    private void ShowSlider()
    {
        slider.value = player.playerHeat;
        if (slider.minValue >= 0)
            sliderFillImage.color = Color.Lerp(Color.blue, Color.red, slider.value / slider.maxValue);
        else if (slider.minValue < 0 && slider.maxValue > 0)
        {
            sliderFillImage.color = Color.Lerp(Color.blue, Color.red, (slider.value - slider.minValue) / (slider.maxValue - slider.minValue));
        }
        else
            sliderFillImage.color = Color.Lerp(Color.red, Color.blue, slider.value / slider.maxValue);
    }
    private void ShowResult()
    {
        if(!player.isAlive)
        {
            gameOverText.text = "GameOver!!\nResult : " + takenStars;
            highScoreText.text = "High Score : " + highScore;
        }
    }

    private void ChangingToGameOver()
    {
        if (player.isAlive)
            return;
        starText.gameObject.SetActive(false);
        arrow.gameObject.SetActive(false);
        slider.gameObject.SetActive(false);
        panealImage.gameObject.SetActive(true);
        
    }
}
