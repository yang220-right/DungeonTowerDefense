using System;
using UnityEngine;

public class BaseProjectile : MonoBehaviour{
    protected Vector3 MoveDir;
    protected float MoveSpeed;
    protected float LifeTime;
    protected int Damage = 10; 
    
    public virtual void Init(Vector3 moveDir = default, float moveSpeed = 15,float lifeTime = 5f){
        if (moveDir == default){
            MoveDir = Vector3.right;
        }
        else{
            MoveDir = moveDir;
        }
        MoveSpeed = moveSpeed;
        LifeTime = lifeTime; 
    }
        
    public virtual void Move(){
        if (LifeTime >= 0){
            LifeTime -= Time.deltaTime;
            transform.Translate(MoveDir*MoveSpeed*Time.deltaTime, Space.World);
            if (LifeTime <= 0){
                Dead();
            }
        }
    }

    protected virtual void Update(){
        Move();
    }

    protected virtual void OnTriggerEnter(Collider other){
        if (other.TryGetComponent(out BaseEnemy enemy)){
            enemy.Damage(Damage);
            Dead();
        }
    }

    protected virtual void Dead(){
        Destroy(gameObject.transform.parent.gameObject);
    }
}
