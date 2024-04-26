using StarterAssets;
using UnityEngine;

public class WeaponBob : MonoBehaviour
{
    [SerializeField] private Transform _bobTransform;
    [SerializeField] private AnimationCurve _curveY;
    [SerializeField] private AnimationCurve _curveX;
    [SerializeField] private Vector2 _bobSpeed = new Vector2(1, 1);
    [SerializeField] private float _returnSpeed = 5;
    [SerializeField] private float _stopSpeed = 2;
    [SerializeField] private float _xOffset = 0;
    private StarterAssetsInputs _input;
    private FirstPersonController _controller;
    private float _evaluateTimeY;
    private float _evaluateTimeX;
    private Vector3 _initialWeaponPosition;
    private Vector3 _newWeaponPosition;


    private void Start()
    {
        _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();
        _controller = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        _initialWeaponPosition = _bobTransform.localPosition;
        _newWeaponPosition = _initialWeaponPosition;
    }

    private void Update()
    {
        if (_input.move.y != 0 || _input.move.x != 0)
        {
            _evaluateTimeY += Time.deltaTime * _bobSpeed.y;
            _evaluateTimeX += Time.deltaTime * _bobSpeed.x;
            _newWeaponPosition.y = _curveY.Evaluate(_evaluateTimeY);
            _newWeaponPosition.x = _curveX.Evaluate(_evaluateTimeX);

            if (_input.move.x == 1)
            {
                _newWeaponPosition.x += _xOffset;
            }
            else if (_input.move.x == -1)
            {
                _newWeaponPosition.x -= _xOffset;
            }

            _bobTransform.localPosition = Vector3.Lerp(_bobTransform.localPosition, _newWeaponPosition, Time.deltaTime * _returnSpeed);
            
        }
        else if (_input.move.x == 0 && _input.move.y == 0)
        {
            _evaluateTimeX = _evaluateTimeY = 0;
            _bobTransform.localPosition = Vector3.Lerp(_bobTransform.localPosition, _initialWeaponPosition, Time.deltaTime * _stopSpeed);
        }
    }
}
