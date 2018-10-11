using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability", order = 1)]
public class AbilityConfig : ScriptableObject
{
    // public int number = 0;
    // public Color32 color = new Color32(69, 69, 69, 255);

    // Which button abilty will be assigned to
    // TODO: Change this to an enumerated "Button" type with values like "Primary Ability", "Secondary Ability" etc.
    public int buttonNum;

    // Icon for UI (if applicable)
    public Sprite uiIcon;

    public int cooldownDuration;

    // Actual script of Ability with all the functionality of the real ability
    public GameObject ability;
}
