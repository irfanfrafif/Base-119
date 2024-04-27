using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsBackground : MonoBehaviour
{
    [SerializeField] Transform image1;
    [SerializeField] Transform image2;

    [SerializeField] Vector3 rotation;

    private void Update()
    {
        image1.Rotate(rotation * Time.deltaTime);
        image2.Rotate(rotation * Time.deltaTime);
    }
}
