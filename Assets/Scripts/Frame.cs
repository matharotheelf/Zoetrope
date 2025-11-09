using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Frame : MonoBehaviour
{
    public Material[] animationMaterials;
    public string animationName;

    public bool isSelected;
    public AnimationSelection animationSelection;

    public abstract void Select();

    public abstract void Deselect();
}
