using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceable
{
    void Placed();

    void NotPlaced();

    void DestroyPlacedObject();
}
