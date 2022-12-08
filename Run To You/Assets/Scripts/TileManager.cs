using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;  //타일프리팹배열 넣어두는곳
    private Transform playerTransform; //개인변환을 설정
    private float spawnZ = 0.0f;  //맵을 스폰할 위치x와y는 항상 동일한곳에있기에 이객체를 정확히 확장해야하기때문에0
    private float tileLength = 8.0f; // 맵중앙과 맵중앙의 거리길이8
    private int amnTilesOnScreen = 20;  //파일의 개수타일의 양끝개수20
    private List<GameObject> activeTiles;
    private float safezone = 20.0f; //15
    private int lastPrefabIndex = 0;
    public GameObject[] randomPrefabs;
    
    //private float zDistance = 20; //구역사의 거리 (z축)
    
    
    



    //int prefabIndex = -1;
    //Start is called before the first frame update

   
    private void Start()
    {
        activeTiles = new List<GameObject>();
        
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; //플레이어그를 카메라로 태그하기
        for (int i = 0; i < amnTilesOnScreen; i++)  //한화면 타일수보다 적으면
        {
            SpawnTile();  //타일생성
            

        }
        for(int i = 0; i > safezone; i++)
        {
            RandomItem();
        }
       

    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTransform.position.z - safezone > (spawnZ - amnTilesOnScreen * tileLength))//플레이어 위치z가 화면타일의수곱하기 타일의길이보다 크면 
        {
            SpawnTile(); //계속생성
            RandomItem();
            DeleteTile(); // 타일삭제
            
            
        }
       
    }
    private void RandomItem(int prefabIndex = -1)
    {
        GameObject item;
        item = Instantiate(randomPrefabs[RandomPrefabIndex()]) as GameObject;
        item.transform.SetParent(transform);
        item.transform.position = Vector3.forward * spawnZ + Vector3.right * Random.Range(-7, 7);
    }
    private void SpawnTile(int prefabIndex = -1) //맵 스폰 
    {
        GameObject go; //게임오브젝트 만들기       
        go = Instantiate(tilePrefabs[0]) as GameObject; //게임개체 배열 순서 정하기
        go.transform.SetParent(transform);//변환점을 내변환점으로 확장하기
        go.transform.position = Vector3.forward * spawnZ; ; //타일생성위치는z가 가는방향으로 간다
                                                       
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
