using UnityEngine;

public class RecoilShooting : MonoBehaviour
{
    [SerializeField] private Transform _weaponRoot;
    [SerializeField] private float _recoilX = -5f;
    [SerializeField] private float _recoilY = 3f;
    [SerializeField] private float _recoilZ = 0.35f;
    [SerializeField] private float _snappiness = 6f;
    [SerializeField] private float _returnSpeed = 2f;

    private Vector3 _currentRotation;
    private Vector3 _targetRotation;

    void Start()
    {
        _currentRotation = _weaponRoot.localRotation.eulerAngles;
        _targetRotation = _currentRotation;
    }

    void LateUpdate()
    {
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _returnSpeed * Time.deltaTime);
        _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _snappiness * Time.deltaTime);
        _weaponRoot.localRotation = Quaternion.Euler(_currentRotation);
    }

    public void RecoilFire()
    {
        _targetRotation += new Vector3(_recoilX, Random.Range(-_recoilY, _recoilZ), Random.Range(-_recoilZ, _recoilZ));
    }
}