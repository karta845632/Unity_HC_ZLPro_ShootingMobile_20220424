using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 大廳管理器
/// 玩家按下對戰按鈕後開始匹配房間
/// </summary>
public class LobbyManager : MonoBehaviourPunCallbacks
{
    // GameObject 遊戲物件：存放 Unity 場景內所有物件
    // SerializeField 將私人欄位顯示在屬性面板上
    // Heder 標題，在屬性面板上顯示粗體字標題
    [SerializeField, Header("連線中畫面")]
    private GameObject goConnectView;
    [SerializeField, Header("對戰按鈕")]
    private Button btnBattle;
    [SerializeField]
    private Text textCountPlayer;

    // 註解：說明
    // 讓按鈕跟程式溝通的流程
    // 1. 提供公開的方法 Public Method
    // 2. 按鈕在點擊 On Click 後設定呼叫此方法

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        btnBattle.interactable = true;
    }

    public void StartConnect()
    {
        goConnectView.SetActive(true);

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;
        PhotonNetwork.CreateRoom("room", roomOptions);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("加入房間：" + PhotonNetwork.CurrentRoom.PlayerCount);
        textCountPlayer.text = "連線人數 " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        print("玩家加入房間：" + PhotonNetwork.CurrentRoom.PlayerCount);
        textCountPlayer.text = "連線人數 " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }
}
