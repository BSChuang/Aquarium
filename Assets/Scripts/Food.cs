using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("fish")) {
            other.gameObject.GetComponent<FishBasic>().EatLeaf(gameObject);
        }
    }
}
