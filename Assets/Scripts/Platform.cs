using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform _leftPosition;
    [SerializeField] private Transform _rightPosition;

    public Transform LeftPosition => _leftPosition;
    public Transform RightPosition => _rightPosition;
}