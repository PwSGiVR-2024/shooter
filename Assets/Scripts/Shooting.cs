using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _gunEnd;
    [SerializeField] private float _bulletForce = 10f;

    private InputSystemUIInputModule _inputModule;
    private Camera _mainCamera;


    private void Start()
    {
        _inputModule = GameObject.FindObjectOfType<InputSystemUIInputModule>();
        _mainCamera = Camera.main;

        // Subscribe to the left click event
        _inputModule.leftClick.action.performed += OnLeftClick;
    }

    private void OnLeftClick(InputAction.CallbackContext context)
    {
        // ----------------  TO DO : Add conditions to check if the player can shoot --------------------- 
        Shoot();   
    }

    private void Shoot()
    {
        // Getting shoot direction
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Vector3 targetDirection = ray.direction;

        GameObject bullet = Instantiate(_bulletPrefab, _gunEnd.transform.position, _gunEnd.transform.rotation, parent: _gunEnd.transform);

        // Adding force to the bullet
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(targetDirection * _bulletForce, ForceMode.Impulse);
    }
}
