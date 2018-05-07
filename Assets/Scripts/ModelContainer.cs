using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelContainer {
    public GameObject CardModel;
    public GameObject HeroModel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public ModelContainer(GameObject cm, GameObject hm)
    {
        CardModel = cm;
        HeroModel = hm;
    }
}
