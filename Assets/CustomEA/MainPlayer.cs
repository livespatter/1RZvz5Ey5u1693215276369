using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    public List<GameObject> listMainPlayer;
    public ListCharacter listCharacter;


    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void setUpMainPlayer(List<GameObject> listMainPlayer)
    {
        listCharacter = ListCharacter.instance;
        //  Debug.Log("listChar: "+listCharacter);
        // setUpMainPlayer();
        this.listMainPlayer = listMainPlayer;
        if (!listCharacter) return;
        var index = 0;
        foreach (var item in listCharacter.listCharacter)
        {
            if (listMainPlayer[index] && listCharacter && item.player)
            {
                var newPlayer = item.player;
                // Debug.Log("newPlayer:"+newPlayer);
                var newObj = Instantiate(newPlayer, listMainPlayer[index].transform);
                var oldAnim = listMainPlayer[index].GetComponent<Animator>();
                listMainPlayer[index].GetComponent<CharMainPlayer>().player = newObj;
            }
            index++;
        }

    }
}
