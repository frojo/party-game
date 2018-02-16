using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player", order = 1)]
public class PlayerConfig : ScriptableObject {

	public int number = 0;
	public Color32 color = new Color32(69, 69, 69, 255);
}
