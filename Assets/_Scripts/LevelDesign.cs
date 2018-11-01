using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesign : MonoBehaviour
{
   
    public Level iceMountainLevel;
    public Level lavaMountainLevel;
    public Level deathMountainLevel;

    private CameraController camC;
    private BlockMaker blockM;
    private SpriteRenderer cameraBackground;
    private Player player;
    private UiManager ui;
    private int check = 1;

    [System.Serializable]
    public class Level
    {
        
        public int cameraSpeedMinimum;
        public int cameraSpeedMaximum;
        public int cameraLastTimeMinimum;
        public int cameraLastTimeMaximum;

        public int basicBlockChance;
        public int gravityReverseBlockChance;
        public int gravityReverseDoubleBlockChance;
        public int stickyBlockChance;
        public int iceBlockChance;
        public int lavaBlockChance;
        public int deathBlockChance;
        public Sprite backgroundSprite;
        public GameObject[] blocksToMake;

        private GameObject[] blockMaking;
        

        public void BlockLevel()
        {

            blockMaking = new GameObject[basicBlockChance + gravityReverseBlockChance + gravityReverseDoubleBlockChance + stickyBlockChance
                 + iceBlockChance + lavaBlockChance + deathBlockChance];
            int b;
            for (int a = 0; a < blockMaking.Length; a++)
            {
                if (a < basicBlockChance)
                    b = 0;
                else if (a >= basicBlockChance && a < basicBlockChance + gravityReverseBlockChance)
                    b = 1;
                else if (a >= basicBlockChance + gravityReverseBlockChance && a < basicBlockChance
                    + gravityReverseBlockChance + gravityReverseDoubleBlockChance)
                    b = 2;
                else if (a >= basicBlockChance + gravityReverseBlockChance + gravityReverseDoubleBlockChance &&
                    a < basicBlockChance + gravityReverseBlockChance + gravityReverseDoubleBlockChance + stickyBlockChance)
                    b = 3;
                else if (a >= basicBlockChance + gravityReverseBlockChance + gravityReverseDoubleBlockChance + stickyBlockChance &&
                    a < basicBlockChance + gravityReverseBlockChance + gravityReverseDoubleBlockChance + stickyBlockChance + iceBlockChance)
                    b = 4;
                else if (a >= basicBlockChance + gravityReverseBlockChance + gravityReverseDoubleBlockChance + stickyBlockChance + iceBlockChance &&
                    a < basicBlockChance + gravityReverseBlockChance + gravityReverseDoubleBlockChance + stickyBlockChance + iceBlockChance + deathBlockChance)
                    b = 5;
                else
                    b = 6;
                blockMaking[a] = blocksToMake[b];
            }
        }
        public GameObject[] Result()
        {
            return blockMaking;
        }
    }

    private void Start()
    {
        camC = GameObject.Find("Main Camera").GetComponent<CameraController>();
        blockM = GameObject.Find("Main Camera").GetComponentInChildren<BlockMaker>();
        cameraBackground = GameObject.Find("BackGroundImage").GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Player>();
        ui = GameObject.Find("UIManager").GetComponent<UiManager>();
        StartCoroutine("ChangingLevel");
    }

    private void ChangingLevelToIceMountain()
    {
        iceMountainLevel.BlockLevel();
        blockM.Blocks = iceMountainLevel.Result();
        camC.maximumSpeed = iceMountainLevel.cameraSpeedMaximum;
        camC.minimumSpeed = iceMountainLevel.cameraSpeedMinimum;
        camC.maximumLastTime = iceMountainLevel.cameraLastTimeMaximum;
        camC.minimumLastTime = iceMountainLevel.cameraLastTimeMinimum;
        cameraBackground.sprite = iceMountainLevel.backgroundSprite;
        SoundManager.fireMountain.Stop();
        SoundManager.deathMountain.Stop();
        SoundManager.iceMountain.Play();
    }
    private void ChangingLevelToLavaMountain()
    {
        lavaMountainLevel.BlockLevel();
        blockM.Blocks = lavaMountainLevel.Result();
        camC.maximumSpeed = lavaMountainLevel.cameraSpeedMaximum;
        camC.minimumSpeed = lavaMountainLevel.cameraSpeedMinimum;
        camC.maximumLastTime = lavaMountainLevel.cameraLastTimeMaximum;
        camC.minimumLastTime = lavaMountainLevel.cameraLastTimeMinimum;
        cameraBackground.sprite = lavaMountainLevel.backgroundSprite;
        SoundManager.fireMountain.Play();
        SoundManager.deathMountain.Stop();
        SoundManager.iceMountain.Stop();
    }

    private void ChangingLevelToDeathMountain()
    {
        deathMountainLevel.BlockLevel();
        blockM.Blocks = deathMountainLevel.Result();
        camC.maximumSpeed = deathMountainLevel.cameraSpeedMaximum;
        camC.minimumSpeed = deathMountainLevel.cameraSpeedMinimum;
        camC.maximumLastTime = deathMountainLevel.cameraLastTimeMaximum;
        camC.minimumLastTime = deathMountainLevel.cameraLastTimeMinimum;
        cameraBackground.sprite = deathMountainLevel.backgroundSprite;
        SoundManager.fireMountain.Stop();
        SoundManager.deathMountain.Play();
        SoundManager.iceMountain.Stop();
    }
    IEnumerator ChangingLevel()
    {
        
        while (player.isAlive)
        {
            if (ui.takenStars % 3 == 0 && check != ui.takenStars)
            {
                int stageNum = Random.Range(0, 3);
                switch (stageNum)
                {
                    case 0:
                        ChangingLevelToLavaMountain();
                        break;
                    case 1:
                        ChangingLevelToIceMountain();
                        break;
                    case 2:
                        ChangingLevelToDeathMountain();
                        break;
                }
                //ui.takenStart % 3 == 0 인 상태일때 계속 레벨이 전환되지 않도록 해주는 변수
                check = ui.takenStars;
            }
            yield return new WaitForSeconds(1.5f);
        }
    }

}
//3가지 레벨 존재 1.화산 특: 라바블록 비율 up, 카메라 속도 빠름, ice블록 x
// 2. 설산 특: ice블록 비율 up, 카메라 속도 느림, 라바블록 x
//3. 죽음존 특: ice,lava블록 안나옴 대신 밟으면 즉사하는 블록 추가;