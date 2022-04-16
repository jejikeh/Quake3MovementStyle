using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Quake3MovementStyle.Quake3HoldAndDropObjects))]
public class DebugPlayer : MonoBehaviour
{
    [Header("Spawn Objects")]
    [SerializeField] private GameObject _createPoint;
    [Header("Default Cube")]
    [SerializeField] private GameObject _boxPrefab;
    [SerializeField] private KeyCode _boxSpawnKey;
    [Header("Player")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerStartPoint;
    [SerializeField] private KeyCode _playerSpawnKey;


    private Quake3MovementStyle.Quake3HoldAndDropObjects _quake3MovementStyle;
    // Update is called once per frame
    private void Start()
    {
        _quake3MovementStyle = GetComponent<Quake3MovementStyle.Quake3HoldAndDropObjects>();
    }
    void Update()
    {
        if (Input.GetKeyDown(_boxSpawnKey))
        {
            
            //newBox.GetComponent<Renderer>().material.color = Color.white;
            //Destroy(newBox);
            _createPoint.GetComponent<MeshFilter>().mesh = _boxPrefab.GetComponent<MeshFilter>().sharedMesh;
            _createPoint.transform.localRotation = Quaternion.identity;

        }else if (Input.GetKeyUp(_boxSpawnKey))
        {
            GameObject newBox = Instantiate(_boxPrefab) as GameObject;
            newBox.transform.position = _createPoint.transform.position;
            _createPoint.GetComponent<MeshFilter>().mesh = null;

        }

        if (Input.GetKeyDown(_playerSpawnKey))
        {
            GameObject Player = Instantiate(_playerPrefab) as GameObject;
            Player.transform.position = _playerStartPoint.position;
            Destroy(this.gameObject);
        }
    }
}
