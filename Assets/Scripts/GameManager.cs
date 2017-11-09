using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Photon.PunBehaviour
{
    [SerializeField]
    private GameObject Sperm;
    public static GameManager MyObject;

    public bool GameStarted = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        MyObject = this;
    }

    public static GameManager GetManager()
    {
        return MyObject;
    }

    public void StartGame()
    {

    }

    public void InitGame()
    {

    }

    public void EndGame()
    {

    }

    public void StartButton()
    {
        PhotonNetwork.ConnectUsingSettings("OMG");
    }

    public virtual void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public virtual void OnJoinedLobby()
    {
        Debug.Log(PhotonNetwork.connectionStateDetailed.ToString());
        PhotonNetwork.JoinOrCreateRoom("RandomRoomName", null, null);
    }

    public virtual void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.connectionStateDetailed.ToString());
        SceneManager.LoadScene(1);
    }

    public void OnLevelWasLoaded(int level)
    {        
        if (PhotonNetwork.countOfPlayers == 2)
        {
            Debug.Log("Spawning new player!");
            photonView.RPC("InitiateGame", PhotonTargets.All);
        }
    }

    [PunRPC]
    void InitiateGame()
    {
        Transform spawnPoint = GameObject.Find("SpawnPoint").transform;
        PhotonNetwork.Instantiate("PlayerSperm", spawnPoint.position, Quaternion.identity, 0);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
