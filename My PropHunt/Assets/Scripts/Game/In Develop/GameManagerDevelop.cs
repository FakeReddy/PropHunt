using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

/*internal class PlayersRole
{
    internal static int _maxPlayerWithRoleItems = 2;
    internal static int _maxPlayerWithRoleHunters = 2;

    internal static int _currentPlayerWithRoleItems;
    internal static int _currentPlayerWithRoleHunters;
}*/

public class GameManagerDevelop : MonoBehaviour
{
    /*[SerializeField] private GameObject _playerPrefabItemRole;
    [SerializeField] private GameObject _playerPrefabHunterRole;
    [SerializeField] private PlayersRole _playersRole;
    [SerializeField] private Objects _objects;

    private void Start()
    {
        PhotonNetwork.Instantiate(_playerPrefabItemRole.name, Vector3.up, Quaternion.identity);
        SetInvisibleCursor();
        UpdateRoleButtons();
    }

    public void TryJoinToItemsRole()
    {
        if (PlayersRole._currentPlayerWithRoleItems < PlayersRole._maxPlayerWithRoleItems)
            JoinToItemsRole();
        else
            UpdateRoleButtons();
    }

    public void TryJoinToHuntersRole()
    {
        if (PlayersRole._currentPlayerWithRoleHunters < PlayersRole._maxPlayerWithRoleHunters)
            JoinToHuntersRole();
        else
            UpdateRoleButtons();
    }

    private void JoinToItemsRole()
    {
        PlayersRole._currentPlayerWithRoleItems++;
        _objects._thisPlayer = PhotonNetwork.Instantiate(_playerPrefabItemRole.name, Vector3.up, Quaternion.identity);
        OnJointToRole();
    }

    private void JoinToHuntersRole()
    {
        PlayersRole._currentPlayerWithRoleHunters++;
        _objects._thisPlayer = PhotonNetwork.Instantiate(_playerPrefabHunterRole.name, Vector3.up, Quaternion.identity);
        OnJointToRole();
    }

    private void OnJointToRole()
    {
        _objects._rolePanel.SetActive(false);
        SetInvisibleCursor();
    }

    private void UpdateRoleButtons()
    {
        _objects._joinToItemsRoleButton.interactable = PlayersRole._currentPlayerWithRoleItems < PlayersRole._maxPlayerWithRoleItems;
        _objects._joinToHuntersRoleButton.interactable = PlayersRole._currentPlayerWithRoleHunters < PlayersRole._maxPlayerWithRoleHunters;
    }

    private void SetInvisibleCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    [System.Serializable]
    private struct Objects
    {
        [SerializeField] internal GameObject _rolePanel;
        [SerializeField] internal Button _joinToItemsRoleButton;
        [SerializeField] internal Button _joinToHuntersRoleButton;

        internal GameObject _thisPlayer;
    }*/
}