using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    ResourceManager _resouce = new ResourceManager();

    public static ResourceManager Resource { get { return Instance._resouce; } }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }


    static void Init()
    {
        if(s_instance == null)
        {
            GameObject obj = GameObject.Find("Managers");
            if(obj == null)
            {
                obj = new GameObject { name = "Managers" };
                obj.AddComponent<Managers>();
            }

            DontDestroyOnLoad(obj);
            s_instance = obj.GetComponent<Managers>();
        }
    }
}