using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateLevel : MonoBehaviour
{
    public GameObject obj;

    private void Awake() {
        if(TestUi.instance && TestUi.instance.objTest) {
            obj = TestUi.instance.objTest;
        }
        var item = Instantiate(obj);
        item.transform.DetachChildren();
        var playerMoving = FindObjectOfType<Playermuving>();
        // Debug.Log("playerMoving: "+playerMoving);
        FindObjectOfType<MainPlayer>().setUpMainPlayer(playerMoving.Allplayer);
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
