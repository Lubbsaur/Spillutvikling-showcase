using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {

            transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        }
    }
}

