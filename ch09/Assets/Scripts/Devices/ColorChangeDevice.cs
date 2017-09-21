using UnityEngine;
using System.Collections;

public class ColorChangeDevice : MonoBehaviour {
	public void Operate() {
		Color random = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
		GetComponent<Renderer>().material.color = random;
	}
}
