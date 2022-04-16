using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Quake3MovementStyle.Quake3HoldAndDropObjects))]
public class DebugPlayer : MonoBehaviour
{
    [Header("Spawn Objects")]
    [SerializeField] private GameObject _createPoint;
    [SerializeField] private Transform _cameraTransform;
    [Header("Default Cube")]
    [SerializeField] private GameObject _boxPrefab;
    [SerializeField] private KeyCode _boxSpawnKey;
    [Header("Player")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerStartPoint;
    [SerializeField] private KeyCode _playerSpawnKey;


    private Quake3MovementStyle.Quake3HoldAndDropObjects _quake3HoldAndDropObjects;
    // Update is called once per frame
    private void Start()
    {
        _quake3HoldAndDropObjects = GetComponent<Quake3MovementStyle.Quake3HoldAndDropObjects>();
    }
    void Update()
    {

        SpawnObjectOnKey(_boxPrefab, _boxSpawnKey);


        if (Input.GetKeyDown(_playerSpawnKey))
        {
            GameObject Player = Instantiate(_playerPrefab) as GameObject;
            Player.transform.position = _playerStartPoint.position;
            Destroy(this.gameObject);
        }
    }

    private void SpawnObjectOnKey(GameObject newObject, KeyCode spawnKey)
    {
        if (Input.GetKeyDown(spawnKey))
        {
            GameObject newBox = Instantiate(newObject) as GameObject;
            newBox.transform.position = _createPoint.transform.position;
            _quake3HoldAndDropObjects.CheckForPickUpObject(_cameraTransform);
        }else if (Input.GetKeyUp(spawnKey))
        {
            _quake3HoldAndDropObjects.CheckForPickUpObject(_cameraTransform);
        }
    }
}
