using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHighScore : MonoBehaviour
{
    public List<GameObject> listChar;

    // Start is called before the first frame update
   void Start()
    {
       setUpPlayer();
    }

    private void setUpPlayer()
    {
        if(ListCharacter.instance) {
            var index = 0;
            foreach (var item in ListCharacter.instance.listCharacter)
            {
                if (listChar.Count > index && item.player)
                {
                    var newObj = Instantiate(item.player, listChar[index].transform);
                    var animNewObj = newObj.GetComponent<Animator>();
                    animNewObj.runtimeAnimatorController = listChar[index].GetComponent<Animator>().runtimeAnimatorController;
                    animNewObj.applyRootMotion = false;
                }
                index++;
            }
        }
       

    }
}
