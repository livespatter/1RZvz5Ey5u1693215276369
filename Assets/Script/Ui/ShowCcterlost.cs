using UnityEngine;
using System.Collections;

public class ShowCcterlost : MonoBehaviour {
    bool intshow;
    public GameObject cameraLost;
	// Use this for initialization
	void Awake () {
        intshow = false;

    }
    void Start()
    {
        intshow = true;
    }
    void OnEnable()
    {
        if (intshow)
        {
            UImanager.uimanager.ShowCharacterLost(true);
        }
        prepareCamera();
        cameraLost.SetActive(true);
    }
     void OnDisable()
    {
        prepareCamera();
        cameraLost.SetActive(false);
          UImanager.uimanager.ShowCharacterLost(false);
    } 

    private void prepareCamera() {
        if(!cameraLost) {
            cameraLost = FindObjectOfType<Camerafolow>().cameraLost;
        }
    }
   
}
