using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using Unity.Services.Core;
using System.Threading.Tasks;

public class Auth : MonoBehaviour
{
    public void Start()
    {
        PlayGamesPlatform.Activate();
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate(ProcessAuthentication);
        }
    }

    void ProcessAuthentication(bool success)
    {
        if (success)
        {
            Debug.Log("Успешная аутентификация");
        }
        else
        {
            Debug.Log("Аутентификация не удалась");
        }
    }
}
