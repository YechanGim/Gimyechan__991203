using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //������ �帧�� �����ϰ� �̸�������Ʈ�� ã���ִ� ��ũ��Ʈ

    private static GameManager instance;
    ItemDataManager itemData;

    private bool CameraSwap = true; //Ÿ����ġ���, ���������ȯ�� ����

    public GameObject TS = null; //���콺 ������Ʈ ����
    public GameObject ButtonGroup; // Ÿ������ ��ư

    GameObject Mouse_Cotrol;

    private GameObject Player;
    private GameObject Player_Hp;

    private GameObject TopViewCamera;

    private GameObject MonsterSpawner;

    private GameObject MiniMap;

    GameObject StartButton;

    int monterLiveCount = 0;

    int maxWave = 2; //�ִ� ���̺��
    int wave = 1;  // ������̺�
    int stage = 1; // ���� ��������

    private GameObject[] StageText; //���������� �����ִ� �ؽ�Ʈ�� �����ϴ� ���� Ÿ����ġ�� ������嶧 ��ġ�� �ٸ��� ������ �迭�� �޾ƿ�
    private GameObject[] WaveText; //���̺긦 �����ִ� �ؽ�Ʈ�� �����ϴ� ���� Ÿ����ġ�� ������嶧 ��ġ�� �ٸ��� ������ �迭�� �޾ƿ�

    private GameObject BossMasage; //�������� ��� �޽����� �����ִ� ���ӿ�����Ʈ�� �����ϴ� ����

    InventoryUI inventoryUI;
    public InventoryUI InvenUI
    {
        get => inventoryUI;
    }

    public ItemDataManager ItemData
    {
        get => itemData;
    }

    public int MaxWave
    {
        get { return maxWave; }
    }
    public int Wave
    {
        get { return wave; }
        set { wave = value;
            WaveChange?.Invoke();
        
        }
    }

    public System.Action WaveChange; //wave�� ���Ҷ����� ��������ִ� ��������Ʈ

    public int Stage
    {
        get { return stage; }
        set { stage = value;
        StageChange?.Invoke();
        }
    }

    public System.Action StageChange; //stage�� ���Ҷ����� ��������ִ� ��������Ʈ 

    public int MONSTERLIVECOUNT
    {
        get { return monterLiveCount; }
        set
        {
            monterLiveCount = value;

            if (wave == maxWave)
            {

                if (monterLiveCount < 1)
                {
                    Cursor.lockState = CursorLockMode.None;
                    SceneManager.LoadScene(1);
                }
            }
            else
            {

                if (monterLiveCount < 2)
                {
                    TowerSwap();
                    Wave += 1;
                    Player.GetComponent<PlayerWolf>().MONEY += 500;
                }
            }
        }
    }
    public GameObject PLAYER
    {
        get { return Player; }
    }
    public bool CAMERASWAP
    {
        get { return CameraSwap; }
        set { CameraSwap = value; }
    }
    public static GameManager INSTANCE
    {
        get { return instance; }
    }

    public GameObject MOUSE
    {
        get { return Mouse_Cotrol; }
    }

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;

            //DontDestroyOnLoad(gameObject);
            //SceneManager.sceneLoaded += OnSceneLoaded;
            Initialize();

            
            
        }else
        {
            if(instance!=this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Initialize();
    }

    /// <summary>
    /// ó�� ���۽� �̸�������Ʈ�� ã�� ��Ȱ��, ���콺Ŀ�� �Ⱥ��̰� ���ִ� �Լ�
    /// </summary>
    private void Initialize()
    {
        Mouse_Cotrol = GameObject.FindGameObjectWithTag("Mouse_Control");
        Player = GameObject.FindGameObjectWithTag("Player");
        Player_Hp = GameObject.FindGameObjectWithTag("Player_Hp");
        TopViewCamera = GameObject.FindGameObjectWithTag("TopViewCamera");
        MonsterSpawner = GameObject.FindGameObjectWithTag("MonsterSpawner");
        MiniMap = GameObject.FindGameObjectWithTag("MiniMap");
        MiniMap.gameObject.SetActive(false);

        StartButton = GameObject.FindGameObjectWithTag("StartButton");
        StartButton.GetComponent<Button>().onClick.AddListener(TowerSwap);
        Cursor.visible = true;

        StageText = GameObject.FindGameObjectsWithTag("StageText");

        WaveText = GameObject.FindGameObjectsWithTag("WaveText");

        StageText[1].SetActive(false);
        WaveText[1].SetActive(false);

        BossMasage = GameObject.FindGameObjectWithTag("BossText");
        BossMasage.SetActive(false);

        itemData = GetComponent<ItemDataManager>();
        inventoryUI = FindObjectOfType<InventoryUI>();

    }
    /// <summary>
    /// Ÿ����ġ���,������带 �������ִ� �Լ�
    /// </summary>

    public void TowerSwap()
    {
        CameraSwap = !CameraSwap;
        TS.SetActive(CameraSwap);
        ButtonGroup.SetActive(CameraSwap);
        //Player_Hp.SetActive(!CameraSwap);
        TopViewCamera.SetActive(CameraSwap);
        MiniMap.SetActive(!CameraSwap);
        StartButton.SetActive(CameraSwap);
        MonsterSpawner.GetComponent<MonsterSpawner>().StartSpawn(!CameraSwap);
        monterLiveCount = MonsterSpawner.GetComponent<MonsterSpawner>().maxMonsterCount;
        MonsterSpawner.GetComponent<MonsterSpawner>().monsterCount = 0;
        StageText[0].SetActive(CameraSwap);
        StageText[1].SetActive(!CameraSwap);
        WaveText[0].SetActive(CameraSwap);
        WaveText[1].SetActive(!CameraSwap);

        towerSwapDelegate?.Invoke();

        if(!CameraSwap)
        {
            Cursor.visible=false;
            Cursor.lockState = CursorLockMode.Locked;
        }else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }

    public System.Action towerSwapDelegate;

    public IEnumerator BossMasageOn()
    {
        BossMasage.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        BossMasage.SetActive(false);
    }

}
