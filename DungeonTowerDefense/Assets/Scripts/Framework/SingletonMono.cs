using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour{
  private static T ins;

  public static T Ins{
    get => ins;
  }

  protected virtual void Awake(){
    ins = this as T;
  }
}