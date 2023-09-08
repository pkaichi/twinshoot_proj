using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShotControl
{
    public void Fire(Transform t, Vector3 forward);
}
