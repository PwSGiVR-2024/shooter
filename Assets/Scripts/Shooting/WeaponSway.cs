using StarterAssets;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] private Transform _weaponsSlot;
    [SerializeField] private float _step = 0.01f;
    [SerializeField] private float _maxStepDistance = 0.06f;
    [SerializeField] private float _rotationStep = 4f;
    [SerializeField] private float _maxRotationStep = 5f;
    [SerializeField] private float _smoothPos = 10f;
    [SerializeField] private float _smoothRot = 12f;

    private Vector3 _swayEulerRot;
    private Vector3 _swatPos;
    private StarterAssetsInputs _input;

    void Start()
    {
        _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        GetSwayWeaponPosition();
        GetSwayWeaponRotation();
        SwayUpdate();
    }

    private void GetSwayWeaponPosition()
    {
        if (_input.look.x == 0 && _input.look.y == 0)
        {
            _swatPos = Vector3.zero;
            return;
        }

        Vector2 invertLook = _input.look * -_step;
        invertLook.x = Mathf.Clamp(invertLook.x, -_maxStepDistance, _maxStepDistance);
        invertLook.y = Mathf.Clamp(invertLook.y, -_maxStepDistance, _maxStepDistance);

        _swatPos = invertLook;
    }

    private void GetSwayWeaponRotation()
    {
        if (_input.look.x == 0 && _input.look.y == 0)
        {
            _swayEulerRot = Vector3.zero;
            return;
        }

        Vector2 invertLook = _input.look * -_rotationStep;
        invertLook.x = Mathf.Clamp(invertLook.x, -_maxRotationStep, _maxRotationStep);
        invertLook.y = Mathf.Clamp(invertLook.y, -_maxRotationStep, _maxRotationStep);
        _swayEulerRot = new Vector3(invertLook.y, invertLook.x, invertLook.x);
    }

    private void SwayUpdate()
    {
        _weaponsSlot.localPosition = Vector3.Lerp(_weaponsSlot.localPosition, _swatPos, Time.deltaTime * _smoothPos);
        _weaponsSlot.localRotation = Quaternion.Slerp(_weaponsSlot.localRotation, Quaternion.Euler(_swayEulerRot), Time.deltaTime * _smoothRot);
    }
}
