using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IItem
{
    string Name { get; }
    Sprite UIIcon { get; }
    string Description { get; }

    bool IsKey { get; }
}