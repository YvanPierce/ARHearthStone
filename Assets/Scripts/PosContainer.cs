using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosContainer : MonoBehaviour {
    public List<Vector3> CardsPos;
    public List<Vector3> HeroPos;
	// Use this for initialization
	void Start () {
        CardsPos = new List<Vector3>();
        HeroPos = new List<Vector3>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
