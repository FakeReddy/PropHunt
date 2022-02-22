using System.Collections;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private float _waitTimeToStartLoadScene;
    [SerializeField] private Animator _loadPanel;
    [SerializeField] private InputFields _inputFields;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = GlobalStringsVars.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        _loadPanel.SetBool(GlobalStringsVars.IsLoadedAnim, true);

        /*_inputFields._name.text = "213";
        _inputFields._createRoom.text = "1";
        CreateRoom();*/
    }

    public void CreateRoom()
    {
        if (_inputFields._name.text != GlobalStringsVars.NoneInString && _inputFields._createRoom.text != GlobalStringsVars.NoneInString)
            PhotonNetwork.CreateRoom(_inputFields._createRoom.text, new Photon.Realtime.RoomOptions { MaxPlayers = GlobalStringsVars.MaxPlayersInRoom });
    }

    public void JoinRoom()
    {
        if (_inputFields._name.text != GlobalStringsVars.NoneInString && _inputFields._joinRoom.text != GlobalStringsVars.NoneInString)
            PhotonNetwork.JoinRoom(_inputFields._joinRoom.text);
    }

    public void JoinRandomRoom()
    {
        if(_inputFields._name.text != GlobalStringsVars.NoneInString)
            PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.NickName = _inputFields._name.text;
        StartCoroutine(LoadScene());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator LoadScene()
    {
        _loadPanel.SetBool(GlobalStringsVars.IsLoadedAnim, false);

        yield return new WaitForSecondsRealtime(_waitTimeToStartLoadScene);

        PhotonNetwork.LoadLevel(GlobalStringsVars.GameSceneIndex);
    }

    [System.Serializable]
    private struct InputFields
    {
        [SerializeField] internal TMP_InputField _name;
        [SerializeField] internal TMP_InputField _createRoom;
        [SerializeField] internal TMP_InputField _joinRoom;
    }
}