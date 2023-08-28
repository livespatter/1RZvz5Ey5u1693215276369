using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Settings
{
    public string urlObjListCharacter;
    public string urlObjEnvironment;
    public string urlBgLoading;
}

[System.Serializable]
public class RootEASettingModel
{
    public Settings Settings;
}