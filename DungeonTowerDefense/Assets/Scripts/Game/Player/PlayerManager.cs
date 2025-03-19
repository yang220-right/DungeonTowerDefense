using UnityEngine;

public class PlayerManager : SingletonMono<PlayerManager>{
  public float MoveSpeed;
  private float xieMoveSpeed => MoveSpeed / 1.414f;
  private float currentMoveSpeed;
  private Vector3 moveDir;

  private void Update(){
    float h = Input.GetAxisRaw("Horizontal");
    float v = Input.GetAxisRaw("Vertical");
    if (h != 0 || v != 0){
      if (Mathf.Abs(h) >= 1 && Mathf.Abs(v) >= 1){
        currentMoveSpeed = xieMoveSpeed;
      }
      else{
        currentMoveSpeed = MoveSpeed;
      }

      moveDir = new Vector3(h, 0, v);
      transform.Translate(moveDir * Time.deltaTime * currentMoveSpeed, Space.World);
    }
    else moveDir = Vector3.zero;

    PlayerShoot.Ins.Fire();
  }

  public void MoveToSelf(Transform trans, float moveSpeed){
    var dir = trans.position - transform.position;
    dir = -new Vector3(dir.x, 0, dir.z).normalized;
    trans.Translate(dir * Time.deltaTime * moveSpeed, Space.World);
  }
}