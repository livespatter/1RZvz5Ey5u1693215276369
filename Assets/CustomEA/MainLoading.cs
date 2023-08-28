using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLoading : MonoBehaviour
{
    public Image imgLoading;

    private void Awake() {
        if(TestUi.instance && TestUi.instance.spriteLoading) {
            imgLoading.sprite = TestUi.instance.spriteLoading;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
