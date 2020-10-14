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
    public Transform cubesParent;
    
    private CubePos _currentCube = new CubePos(0, 1, 0);
    private Rigidbody cubesParentRigidBody;
    private Coroutine _showPossibleCubePlace;
    private bool _isLose;

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
    }

    public void SetCube()
    {
        if (cubeThatDefinesPlace != null)
        {
            GameObject newCube = Instantiate(cubePref, cubeThatDefinesPlace.position, Quaternion.identity);
            newCube.transform.SetParent(cubesParent);
            _currentCube.SetVector(cubeThatDefinesPlace.position);
            _cubesPositions.Add(_currentCube.GetVector());

            cubesParentRigidBody.isKinematic = true;
            cubesParentRigidBody.isKinematic = false;

            SpawnPossiblePositions();
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

        cubeThatDefinesPlace.position = positions[Random.Range(0, positions.Count)];
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
