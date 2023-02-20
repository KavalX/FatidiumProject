
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private bool _doorOpened = false;
    
    public void OpenDoor()
    {
        if (_doorOpened == false)
        {
            transform.RotateAround(target.transform.position, new Vector3(0,0,90), 90);
            _doorOpened = true;
        }
    }
}
