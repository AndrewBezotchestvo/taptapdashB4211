using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField] private Transform _player;
    [SerializeField] private Transform _startPoint;

    [SerializeField] private List<GameObject> _verticalChunks;
    [SerializeField] private List<GameObject> _horizontalChunks;

    [SerializeField] private float _triggerDistance;

    private List<GameObject> _spawnedchunks;

    private GameObject startChunkPoint;
    private GameObject endChunkPoint;

    private Vector3 _startNextPoint;

    private bool _isVertical = false;

    void Start()
    {
        _isVertical = false;
        _startNextPoint = SpawnNewChunk(_startPoint.position);
    }

    void Update()
    {
        if (Vector3.Distance(_startNextPoint, _player.position) < _triggerDistance)
        {
            _startNextPoint = SpawnNewChunk(_startNextPoint);
            _isVertical = !_isVertical;
        }
    }

    private Vector3 SpawnNewChunk(Vector3 startPoint)
    {
        GameObject newChunk;

        if (!_isVertical)
        {   //создать рандомный элемент из списка наших горизонтальных чанков
            newChunk = Instantiate(_horizontalChunks[Random.Range(0, _horizontalChunks.Count)]); 
        }
        else
        {   //создать рандомный элемент из списка наших вертикальных чанков
            newChunk = Instantiate(_verticalChunks[Random.Range(0, _verticalChunks.Count)]); 
        }

        startChunkPoint = newChunk.transform.GetChild(0).gameObject;
        endChunkPoint = newChunk.transform.GetChild(1).gameObject;

        newChunk.transform.position = startPoint - startChunkPoint.transform.localPosition;

        startPoint = endChunkPoint.transform.position;
        return startPoint;
    }
}
