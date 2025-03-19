using UnityEngine;

public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T ins;

    public static T Ins
    {
        get
        {
            if (ins == null)
            {
                GameObject obj = new GameObject();
                //设置对象的名字为脚本名
                obj.name = typeof(T).ToString();
                //让这个单例模式对象 过场景 不移除
                //因为 单例模式对象 往往 是存在整个程序生命周期中的
                DontDestroyOnLoad(obj);
                ins = obj.AddComponent<T>();
            }
            return ins;
        }
    }
}