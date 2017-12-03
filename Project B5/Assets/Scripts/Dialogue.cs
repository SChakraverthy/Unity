using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour {

	public String [] lines;
	public String currLine;
	public bool interacting;
	public String name;
	public int lineIndex;
	StreamReader readLines;
	public int lineCount;
	public String line;
	public GameObject Sphere;

	// Use this for initialization
	void Start () {
		lines = new String[10];
		interacting = false;
		lineIndex = 0;
		name = this.name;
		lineCount = 0;
		getLines ();
		Debug.Log (lines.Length);
	}
	
	// Update is called once per frame
	void Update () {
		Sphere.transform.position = this.transform.position;
	}

	void getLines(){
		String dataPath = Application.dataPath;
		try{
			readLines = new StreamReader (dataPath + "/" + name + ".txt", Encoding.Default);
			using(readLines){
				while((line = readLines.ReadLine()) != null){
					lines[lineIndex] = line;
					lineIndex++;
					lineCount++;
				}
				readLines.Close();
				lineIndex = 0;
			}
		}
		catch(Exception e){
			Debug.Log ("No text file found for " + this.name + " at " + dataPath);
		}
	}
}

