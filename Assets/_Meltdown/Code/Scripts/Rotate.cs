using UnityEngine;

public class Rotate : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 10f;

    [SerializeField] private bool canReverse;
    private int _reverseModifier; 
    void Update()
    {
        _reverseModifier= canReverse ?  -1 : 1; 
        this.transform.Rotate(0,rotationSpeed  * _reverseModifier * Time.deltaTime, 0 ); 
    }
}
