using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CharacterModel {
    public GameObject player;
    public int price;
}

public class ListCharacter : MonoBehaviour
{
    public static ListCharacter instance;

    public List<CharacterModel> listCharacter;

    private void Awake() {
        if(instance) {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

}
