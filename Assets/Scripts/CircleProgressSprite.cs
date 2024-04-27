using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleProgressSprite : MonoBehaviour
{
    [SerializeField] Transform grey1;
    [SerializeField] Transform grey2;
    [SerializeField] Transform grey3;
    [SerializeField] Transform grey4;
    [SerializeField] Transform grey5;

    [SerializeField] Transform blue1;
    [SerializeField] Transform blue2;
    [SerializeField] Transform blue3;
    [SerializeField] Transform blue4;
    [SerializeField] Transform blue5;

    [SerializeField] Vector3 speed1;
    [SerializeField] Vector3 speed2;
    [SerializeField] Vector3 speed3;
    [SerializeField] Vector3 speed4;
    [SerializeField] Vector3 speed5;

    [SerializeField] SpriteMask mask;

    [Range(0, 1)] public float value;

    void Update()
    {
        mask.alphaCutoff = 1 - (value / 2);

        grey1.Rotate(speed1);
        blue1.Rotate(speed1);

        grey2.Rotate(speed2);
        blue2.Rotate(speed2);

        grey3.Rotate(speed3);
        blue3.Rotate(speed3);

        grey4.Rotate(speed4);
        blue4.Rotate(speed4);

        grey5.Rotate(speed5);
        blue5.Rotate(speed5);
    }
}
