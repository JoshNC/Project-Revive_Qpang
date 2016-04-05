using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{

    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    GameObject playerUIPrefab;
    private GameObject PlayerUIInstance;

    Camera sceneCamera;

    void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }

            PlayerUIInstance = Instantiate(playerUIPrefab);
            PlayerUIInstance.name = playerUIPrefab.name;

        }
    }

    void onDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }

        Destroy(PlayerUIInstance);

    }



}
