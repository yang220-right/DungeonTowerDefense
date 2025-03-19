using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerShoot : SingletonMono<PlayerShoot>{
   public GameObject Prefab_Projectile;
   public float FireInterval;
   private float currentFireInterval = 0;

   private Ray ray;//射线检测
   private RaycastHit hit;//碰撞信息类
   private bool res;//是否碰到
   private Vector3 currentFireDir;//当前点击方向
   private int ignoreMask = 1 << 10;
   private Vector3 playerPos => PlayerManager.Ins.transform.position;
   public void Fire(){
      if (currentFireInterval > 0){
         currentFireInterval -= Time.deltaTime;
      }
      if (Input.GetMouseButton(0)) {
         if (currentFireInterval <= 0){
            currentFireInterval = FireInterval;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);//鼠标点击到哪,就在哪发射一条射线
            res = Physics.Raycast(ray, out hit,100,ignoreMask);//开始检测
            if (res){
               currentFireDir = hit.point - playerPos;
               currentFireDir = new Vector3(currentFireDir.x, 0, currentFireDir.z);
               var spawnPos = new Vector3(playerPos.x,1,playerPos.z);
               var g = Instantiate(Prefab_Projectile,spawnPos,Quaternion.identity);
               var com = g.transform.GetChild(0).gameObject.AddComponent<BaseProjectile>();
               com.Init(currentFireDir.normalized);
            }
         }
      }
   }

   public void OnGUI(){
      // var dir = GetPos(currentFireDir);
      // Debug.DrawLine(playerPos, playerPos + dir.normalized * 3, Color.yellow,0.1f);
   }

   public Vector3 GetPos(Vector3 pos){
      // 获取相机的视图矩阵
      Matrix4x4 viewMatrix = Camera.main.worldToCameraMatrix;
      // 获取物体的世界坐标
      Vector3 worldPosition = pos;
      // 将世界坐标转换为视图坐标
      Vector3 viewPosition = viewMatrix.MultiplyPoint(worldPosition);
      // 在视图空间中调整位置（例如沿X轴偏移1个单位）
      viewPosition.y += 1f;
      // 将视图坐标转换回世界坐标
      Matrix4x4 inverseViewMatrix = viewMatrix.inverse;
      Vector3 newWorldPosition = inverseViewMatrix.MultiplyPoint(viewPosition);
      // 更新物体的位置
      // transform.position = newWorldPosition;
      return newWorldPosition;
   }
}
