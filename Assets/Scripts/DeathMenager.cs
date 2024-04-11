using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public static DeathManager Instance;
    private readonly float _invincibilityTime = 3.0f;
    private float _invincibilityCheck;
    private bool _isDead = false;
    [HideInInspector]
    public bool Dead;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (_isDead)
        {
            if (_invincibilityCheck > 0)
            {
                _invincibilityCheck -= Time.deltaTime;
                
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                _isDead = false;
            }
        }
    }
    public void YouDied()
    {
        if (!Dead)
        {
            Dead = true;
            _isDead = true;
            _invincibilityCheck = _invincibilityTime;
            Debug.Log("You are dead!");
        }
    }

}