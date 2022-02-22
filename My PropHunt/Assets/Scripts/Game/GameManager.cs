using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _spawnPointPlayer;
    [SerializeField] private float _rangeRandomSpawn;

    private void Start()
    {
        Vector3 spawnPosition = GetRandomVector(_spawnPointPlayer.position, _rangeRandomSpawn);
        PhotonNetwork.Instantiate(_playerPrefab.name, spawnPosition, Quaternion.identity);

        SetInvisibleCursor();
    }

    private Vector3 GetRandomVector(Vector3 vector, float range)
    {
        float xValue = Random.Range(vector.x - +_rangeRandomSpawn, vector.x + +_rangeRandomSpawn);
        float zValue = Random.Range(vector.z - +_rangeRandomSpawn, vector.z + +_rangeRandomSpawn);
        return new Vector3(xValue, vector.y, zValue);
    }

    private void SetInvisibleCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}