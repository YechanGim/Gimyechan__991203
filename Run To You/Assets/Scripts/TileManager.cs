using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;  //Ÿ�������չ迭 �־�δ°�
    private Transform playerTransform; //���κ�ȯ�� ����
    private float spawnZ = 0.0f;  //���� ������ ��ġx��y�� �׻� �����Ѱ����ֱ⿡ �̰�ü�� ��Ȯ�� Ȯ���ؾ��ϱ⶧����0
    private float tileLength = 8.0f; // ���߾Ӱ� ���߾��� �Ÿ�����8
    private int amnTilesOnScreen = 20;  //������ ����Ÿ���� �糡����20
    private List<GameObject> activeTiles;
    private float safezone = 20.0f; //15
    private int lastPrefabIndex = 0;
    public GameObject[] randomPrefabs;
    
    //private float zDistance = 20; //�������� �Ÿ� (z��)
    
    
    



    //int prefabIndex = -1;
    //Start is called before the first frame update

   
    private void Start()
    {
        activeTiles = new List<GameObject>();
        
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; //�÷��̾�׸� ī�޶�� �±��ϱ�
        for (int i = 0; i < amnTilesOnScreen; i++)  //��ȭ�� Ÿ�ϼ����� ������
        {
            SpawnTile();  //Ÿ�ϻ���
            

        }
        for(int i = 0; i > safezone; i++)
        {
            RandomItem();
        }
       

    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTransform.position.z - safezone > (spawnZ - amnTilesOnScreen * tileLength))//�÷��̾� ��ġz�� ȭ��Ÿ���Ǽ����ϱ� Ÿ���Ǳ��̺��� ũ�� 
        {
            SpawnTile(); //��ӻ���
            RandomItem();
            DeleteTile(); // Ÿ�ϻ���
            
            
        }
       
    }
    private void RandomItem(int prefabIndex = -1)
    {
        GameObject item;
        item = Instantiate(randomPrefabs[RandomPrefabIndex()]) as GameObject;
        item.transform.SetParent(transform);
        item.transform.position = Vector3.forward * spawnZ + Vector3.right * Random.Range(-7, 7);
    }
    private void SpawnTile(int prefabIndex = -1) //�� ���� 
    {
        GameObject go; //���ӿ�����Ʈ �����       
        go = Instantiate(tilePrefabs[0]) as GameObject; //���Ӱ�ü �迭 ���� ���ϱ�
        go.transform.SetParent(transform);//��ȯ���� ����ȯ������ Ȯ���ϱ�
        go.transform.position = Vector3.forward * spawnZ; ; //Ÿ�ϻ�����ġ��z�� ���¹������� ����
                                                       
        spawnZ += tileLength;
        activeTiles.Add(go);
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
    

    //private void RandomItem(int prefabIndex = -1)
    //{
    //    GameObject item;
    //    item = Instantiate(randomPrefabs[RandomPrefabIndex()]) as GameObject;
    //    item.transform.SetParent(transform);
    //    item.transform.position = Vector3.forward * spawnZ  + Vector3.right * Random.Range(-7, 7);
    //}
    private int RandomPrefabIndex()
    {
        
        if (randomPrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while(randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(1, randomPrefabs.Length);
        }
        
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }

    
}
