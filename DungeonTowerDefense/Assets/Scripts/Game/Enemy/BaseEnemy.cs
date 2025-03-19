using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour{
    public float MoveSpeed;

    public int MaxHP;
    public int CurrentHP;
    
    private void Start(){
        CurrentHP = MaxHP;
    }

    private void Update(){
        PlayerManager.Ins.MoveToSelf(transform, MoveSpeed);
    }

    public void Damage(int damage){
        CurrentHP -= damage;
        if (CurrentHP <= 0){
            EnemySpawnManager.Ins.SubNum();
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
    
}

