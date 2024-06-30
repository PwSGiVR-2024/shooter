using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{ 
    [SerializeField] private TextMeshProUGUI _currentAmmoText;
    [SerializeField] private TextMeshProUGUI _backbackAmmoText;
    [SerializeField] private RawImage _ammoImage;
    //[SerializeField] private TextMeshProUGUI _healthText;


    public static UIController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void UpdateAmmoUI(int currentAmmo, int backbackAmmo)
    {
        _currentAmmoText.text = $"{currentAmmo}";
        _backbackAmmoText.text = $"{backbackAmmo}";
    }

    public void ShowAmmoUI(bool show)
    {
        _currentAmmoText.gameObject.SetActive(show);
        _backbackAmmoText.gameObject.SetActive(show);
        _ammoImage.gameObject.SetActive(show);
    }
}
