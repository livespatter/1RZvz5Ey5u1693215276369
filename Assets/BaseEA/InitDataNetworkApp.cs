using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using EaAds;
using System.Threading.Tasks;

namespace BaseEA
{
    public class InitDataNetworkApp : MonoBehaviour
    {
        public Slider sliderLoading;
        public Text textLoading;
        public string nextScene;

        public string baseUrl;

        // Use this for initialization
        private void Start()
        {
            BetterStreamingAssets.Initialize();
            string url =  baseUrl+"/configApp/apps?packageName=" + Application.identifier;
            StartCoroutine(
                EaAds.AdsUtils.GetAdConfigFromServer(
                    url,
                    (progress) => HandleProgress(progress),
                     async (adConfig, isSuccess) =>
                    {
                        var settingLocal = new Settings();
                        settingLocal.urlObjListCharacter = "listcharacter.ea";
                        settingLocal.urlObjEnvironment = "environment.ea";
                        if(!isSuccess) {
                            Debug.Log("settingLocal: "+settingLocal.urlObjListCharacter);
                            if(!string.IsNullOrEmpty(settingLocal.urlObjListCharacter)) {
                                var obj = await GameUtils.LoadMyAssets(settingLocal.urlObjListCharacter, "listcharacter", (progress) => HandleProgress(progress));
                                Instantiate(obj);
                            }
                            if(!string.IsNullOrEmpty(settingLocal.urlObjEnvironment) && TestUi.instance) {
                                var obj = await GameUtils.LoadMyAssets(settingLocal.urlObjEnvironment, "environment", (progress) => HandleProgress(progress));
                                TestUi.instance.objTest = obj;
                                // PlayerManager.instance.SetPlayer(player);
                            }
                            // var listPlayer = Resources.Load("ListCharacter");
                            // Instantiate(listPlayer);

                            // var environment = Resources.Load("ListCharacter") as GameObject;
                            // if(TestUi.instance.objTest) {
                            //     TestUi.instance.objTest = environment;
                            // }
                            GotoNextScene();
                            return;
                        }
                        if (!adConfig.IsAppActive && isSuccess)
                        {
                            return;
                        }
                        // Debug.Log("isRedirect: "+adConfig.IsOnRedirect);
                        if (adConfig.IsOnRedirect)
                        {
                            FindAnyObjectByType<EARedirectApp>().ShowPanelUpdate(adConfig.UrlRedirect);
                            return;
                        }

                        EaAds.AdsUtils.MapConfigAd(adConfig);
                        AdsManagerWrapper.INSTANCE.Initialize((init) => { });
                        AdsManagerWrapper.INSTANCE.LoadInterstitial();
                        AdsManagerWrapper.INSTANCE.LoadRewards();

                        try
                        {
                            var setting = await EAUtils.GetSettingFromServer( baseUrl+"/json/apps?packageName="+ Application.identifier, (progress) => HandleProgress(progress));
                            // Debug.Log("setting: "+setting.urlObjListCharacter);
                            if(!string.IsNullOrEmpty(setting.urlObjListCharacter)) {
                                var obj = await EAUtils.DownloadAssetBundle(setting.urlObjListCharacter, "listcharacter", (progress) => HandleProgress(progress));
                                Instantiate(obj);
                            } else if(!string.IsNullOrEmpty(settingLocal.urlObjListCharacter)) {
                                var obj = await GameUtils.LoadMyAssets(settingLocal.urlObjListCharacter, "listcharacter", (progress) => HandleProgress(progress));
                                Instantiate(obj);
                            }
                            if(!string.IsNullOrEmpty(setting.urlObjEnvironment) && TestUi.instance) {
                                var obj = await EAUtils.DownloadAssetBundle(setting.urlObjEnvironment, "environment", (progress) => HandleProgress(progress));
                                TestUi.instance.objTest = obj;
                                // PlayerManager.instance.SetPlayer(player);
                            } else  if(!string.IsNullOrEmpty(settingLocal.urlObjEnvironment) && TestUi.instance) {
                                var obj = await GameUtils.LoadMyAssets(settingLocal.urlObjEnvironment, "environment", (progress) => HandleProgress(progress));
                                TestUi.instance.objTest = obj;
                                // PlayerManager.instance.SetPlayer(player);
                            }
                            if(!string.IsNullOrEmpty(setting.urlBgLoading) && TestUi.instance) {
                                var sprite = await EAUtils.LoadSpriteFromURL(setting.urlBgLoading, (progress) => HandleProgress(progress));
                                TestUi.instance.spriteLoading = sprite;
                            }
                            GotoNextScene();
                        }
                        catch (System.Exception e)
                        {
                            GotoNextScene();
                            Debug.LogError($"Failed to load sprite from URL: {e}");
                        }
                       
                    }
                )
            );
        }

        private void GotoNextScene()
        {
            SceneManager.LoadScene(nextScene);
        }

        private void HandleProgress(DataProgress dataProgress) {
            textLoading.text = dataProgress.progressPersen;
            sliderLoading.value = dataProgress.progressFloat;
        }

    }
}
