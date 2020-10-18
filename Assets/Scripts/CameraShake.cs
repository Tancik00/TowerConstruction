using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    private Transform _camTransform;
    private float _shakeDuration = 1f;
    private float _shakeAmount = 0.04f;
    private float _decreaseFactor = 1.5f;
    private Vector3 _startPos;

    private void Start()
    {
        _camTransform = GetComponent<Transform>();
        _startPos = _camTransform.localPosition;
    }

    private void Update()
    {
        if (_shakeDuration > 0f)
        {
            _camTransform.localPosition = _startPos + Random.insideUnitSphere * _shakeAmount;
            _shakeDuration -= Time.deltaTime * _decreaseFactor;
        }
        else
        {
            _shakeDuration = 0f;
            _camTransform.localPosition = _startPos;
        }
    }
}
