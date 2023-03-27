using UnityEngine;

public class LookCamera : MonoBehaviour
{
    public Transform cam;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePos = transform.position - cam.transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

        transform.rotation = rotation;
    }
}
