using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Frame : MonoBehaviour
{
    public Material[] animationMaterials;
    public string animationName;

    public bool isSelected;
    public AnimationSelection animationSelection;

    public void Select()
    {
        // send message to animation selection that this animation is select, changing zoetrope frames
        animationSelection.SelectAnimation(this);

        // set is selected
        isSelected = true;
    }

    public void Deselect()
    {
        // set is deselected
        isSelected = false;
    }
}
