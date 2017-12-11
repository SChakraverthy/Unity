using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Spawn : MonoBehaviour {

	public GameObject character;
	public Text characterCount;
	public Text characterCost;
	public Text resource_count;

	void Start()
	{
		this.GetComponent<Button>().onClick.AddListener(generateCharacter);
	}

	void generateCharacter(){
		float range = 60.0f;
		float difference = float.Parse(resource_count.text) - float.Parse(characterCost.text);

		if (difference >= 0f) {
			resource_count.text = difference.ToString ();

			Instantiate (character);

			Vector3 center = new Vector3 (-100.4f, 0.0f, -35.0f);
			Vector3 result;

			bool spotFound = false;
			while (!spotFound) {
				for (int i = 0; i < 30; i++) {
					
					Vector3 randomPoint = center + Random.insideUnitSphere * range;
					NavMeshHit hit;

					if (NavMesh.SamplePosition (randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
						result = hit.position;
						if (Physics.OverlapSphere (result, 1.0f).Length <= 2) {
							character.transform.position = result;
							spotFound = true;
							characterCount.text = (int.Parse (characterCount.text) + 1).ToString ();
							break;
						}
					}
				}
			}
		}
	}
}
