using System;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class EnemySpawnManager : SingletonMono<EnemySpawnManager>{
  public bool BeginSpawn;
  public GameObject Prefab_CommonEnemy;

  public int MaxNum = 10;
  public int OneFrameSpawn = 5;
  public float MaxSpawnInterval = 0.5f;
  public float Distance2Player = 10;
  private float SpawnInterval;

  private int CurrentNum = 0;

  private void Start(){
    SpawnInterval = MaxSpawnInterval;
  }

  private void Update(){
    if (!BeginSpawn) return;
    if (SpawnInterval > 0){
      SpawnInterval -= Time.deltaTime;
    }

    if (CurrentNum <= MaxNum && SpawnInterval < 0){
      SpawnInterval = MaxSpawnInterval;
      for (int i = 0; i < OneFrameSpawn && CurrentNum <= MaxNum; i++){
        AddNum();
        uint seed = (uint)(Time.time * 100 % 9876543) + (uint)i;
        float2 dir = Random.CreateFromIndex(seed).NextFloat2Direction() * Distance2Player;
        var pos = PlayerManager.Ins.transform.position + new Vector3(dir.x, 0, dir.y);
        Instantiate(Prefab_CommonEnemy, pos, Quaternion.identity);
      }
    }
  }

  public void AddNum(int num = 1){
    CurrentNum += num;
  }

  public void SubNum(int num = 1){
    CurrentNum -= num;
  }
}