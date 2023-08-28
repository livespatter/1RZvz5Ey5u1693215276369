using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUi : MonoBehaviour
{
    public static TestUi instance;
    public GameObject objTest;
    public Sprite spriteLoading;

    private void Awake() {
        if(instance) 
        Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

}
