using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public float cubeChangingPlaceSpeed = 0.5f;
    public Transform cubeThatDefinesPlace;
    public GameObject cubePref;
    public GameObject cubeEffectPref;
    public Transform cubesParent;
    public GameObject[] objectsAtStartGame;
    
    private CubePos _currentCube = new CubePos(0, 1, 0);
    private Rigidbody cubesParentRigidBody;
    private Coroutine _showPossibleCubePlace;
    private Transform mainCam;
    private bool _isLose;
    private bool _isGameStarted;
    private float _camMoveYPos;
    private float _camMoveSpeed = 2f;
    private int _previousMaxHor;

    private List<Vector3> _cubesPositions = new List<Vector3>()
    {
        new Vector3(0, 0, 0),
        new Vector3(1, 0, 0),
        new Vector3(-1, 0, 0),
        new Vector3(0, 1, 0),
        new Vector3(0, 0, 1),
        new Vector3(0, 0, -1),
        new Vector3(1, 0, 1),
        new Vector3(-1, 0, -1),
        new Vector3(-1, 0, 1),
        new Vector3(1, 0, -1)
    };

    private void Start()
    {
        mainCam = Camera.main.transform;
        _camMoveYPos = 5.9f + _currentCube.yPos - 1f;
        cubesParentRigidBody = cubesParent.gameObject.GetComponent<Rigidbody>();
        _showPossibleCubePlace = StartCoroutine(ShowPossibleCubePlace());
    }

    private void Update() 
    {
        if (!_isLose && cubesParentRigidBody.velocity.magnitude > 0.1f)
        {
            Destroy(cubeThatDefinesPlace.gameObject);
            _isLose = true;
            StopCoroutine(_showPossibleCubePlace);
        }
        
        mainCam.localPosition = Vector3.MoveTowards(mainCam.localPosition,
            new Vector3(mainCam.localPosition.x, _camMoveYPos, mainCam.localPosition.z),
            Time.deltaTime * _camMoveSpeed);
    }

    public void SetCube()
    {
        if (cubeThatDefinesPlace != null)
        {
            if (!_isGameStarted)
            {
                _isGameStarted = true;
                foreach (var obj in objectsAtStartGame)
                {
                    obj.SetActive(false);
                }
            }
            GameObject newCube = Instantiate(cubePref, cubeThatDefinesPlace.position, Quaternion.identity);
            newCube.transform.SetParent(cubesParent);
            _currentCube.SetVector(cubeThatDefinesPlace.position);
            _cubesPositions.Add(_currentCube.GetVector());

            Instantiate(cubeEffectPref, newCube.transform.position, Quaternion.identity);
            
            cubesParentRigidBody.isKinematic = true;
            cubesParentRigidBody.isKinematic = false;

            SpawnPossiblePositions();
            MoveCamera();
        }
    }

    private IEnumerator ShowPossibleCubePlace()
    {
        while (true)
        {
            SpawnPossiblePositions();
            yield return new WaitForSeconds(cubeChangingPlaceSpeed);
        }
    }

    private void SpawnPossiblePositions()
    {
        List<Vector3> positions = new List<Vector3>();
        
        if (IsPositionEmpty(new Vector3(_currentCube.xPos + 1, _currentCube.yPos, _currentCube.zPos)) 
        && _currentCube.xPos + 1 != cubeThatDefinesPlace.position.x)
            positions.Add(new Vector3(_currentCube.xPos + 1, _currentCube.yPos, _currentCube.zPos));
        if (IsPositionEmpty(new Vector3(_currentCube.xPos - 1, _currentCube.yPos, _currentCube.zPos))
            && _currentCube.xPos - 1 != cubeThatDefinesPlace.position.x)
            positions.Add(new Vector3(_currentCube.xPos - 1, _currentCube.yPos, _currentCube.zPos));
        if (IsPositionEmpty(new Vector3(_currentCube.xPos, _currentCube.yPos + 1, _currentCube.zPos))
            && _currentCube.yPos + 1 != cubeThatDefinesPlace.position.y)
            positions.Add(new Vector3(_currentCube.xPos, _currentCube.yPos + 1, _currentCube.zPos));
        if (IsPositionEmpty(new Vector3(_currentCube.xPos, _currentCube.yPos - 1, _currentCube.zPos))
            && _currentCube.yPos - 1 != cubeThatDefinesPlace.position.y)
            positions.Add(new Vector3(_currentCube.xPos, _currentCube.yPos - 1, _currentCube.zPos));
        if (IsPositionEmpty(new Vector3(_currentCube.xPos, _currentCube.yPos, _currentCube.zPos + 1))
            && _currentCube.zPos + 1 != cubeThatDefinesPlace.position.z)
            positions.Add(new Vector3(_currentCube.xPos, _currentCube.yPos, _currentCube.zPos + 1));
        if (IsPositionEmpty(new Vector3(_currentCube.xPos, _currentCube.yPos, _currentCube.zPos - 1))
            && _currentCube.zPos - 1 != cubeThatDefinesPlace.position.z)
            positions.Add(new Vector3(_currentCube.xPos, _currentCube.yPos, _currentCube.zPos - 1));

        if (positions.Count > 1)
            cubeThatDefinesPlace.position = positions[Random.Range(0, positions.Count)];
        else if (positions.Count <= 0)
            _isLose = true;
        else
            cubeThatDefinesPlace.position = positions[0];
    }

    private bool IsPositionEmpty(Vector3 targetPos)
    {
        if (targetPos.y <= 0) 
            return false;
        
        foreach (Vector3 pos in _cubesPositions)
        {
            if (pos.x == targetPos.x && pos.y == targetPos.y && pos.z == targetPos.z)
                return false;
        }

        return true;
    }

    private void MoveCamera()
    {
        int maxX = 0;
        int maxY = 0;
        int maxZ = 0;
        int maxHor = 0;

        foreach (var pos in _cubesPositions)
        {
            if (Mathf.Abs((int)pos.x) > maxX)
                maxX = (int)pos.x;
            if ((int)pos.y > maxY)
                maxY = (int)pos.y;
            if (Mathf.Abs((int)pos.z) > maxZ)
                maxZ = (int)pos.z;
        }
        
        _camMoveYPos = 5.9f + _currentCube.yPos - 1f;

        maxHor = maxX > maxZ ? maxX : maxZ;

        if (maxHor % 3 == 0 && _previousMaxHor!=maxHor)
        {
            mainCam.localPosition -= new Vector3(0, 0, 2f);
            _previousMaxHor = maxHor;
        }
    }
}

struct CubePos
{
    public int xPos;
    public int yPos;
    public int zPos;

    public CubePos(int xPos, int yPos, int zPos)
    {
        this.xPos = xPos;
        this.yPos = yPos;
        this.zPos = zPos;
    }

    public Vector3 GetVector()
    {
        return new Vector3(xPos, yPos, zPos);
    }

    public void SetVector(Vector3 pos)
    {
        xPos = (int) pos.x;
        yPos = (int) pos.y;
        zPos = (int) pos.z;
    }
}
