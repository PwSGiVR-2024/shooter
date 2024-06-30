using UnityEngine;

public class Shotgun : RangeWeapon
{
    [SerializeField] private int _pellets = 10;
    [SerializeField] private float _splashX = 0.2f;
    [SerializeField] private float _splashY = 0.2f;

    public int Pellets { get => _pellets; set => _pellets = value; }
    public float SplashX { get => _splashX; set => _splashX = value; }
    public float SplashY { get => _splashY; set => _splashY = value; }
}
