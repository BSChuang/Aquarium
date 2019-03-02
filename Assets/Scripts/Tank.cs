using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {
    public bool playing;

    public GameObject foodPrefab;
    public GameObject fishPrefab;
    public GameObject playerPrefab;

    public int fishSpawnSize;
    public int leafSpawnSize;

    public int leavesCount;
    public int fishCount;

    public int fishSpeed;

    public List<GameObject> leaves = new List<GameObject>();
    public List<GameObject> fishes = new List<GameObject>();

    private void Awake() {
        for (int i = 0; i < fishCount; i++) {
            GameObject fish = Instantiate(fishPrefab, transform);
            fish.transform.localPosition = GetRandomSpawnPos(fishSpawnSize);
            fishes.Add(fish);
        }

        if (playing) {
            GameObject fish = Instantiate(playerPrefab, transform);
            fish.transform.localPosition = GetRandomSpawnPos(fishSpawnSize);
            fishes.Add(fish);
        }
    }

    void ResetLeaves() {
        foreach (GameObject leaf in leaves) {
            Destroy(leaf);
        }
        leaves.Clear();
        for (int i = 0; i < leavesCount; i++) {
            GameObject leaf = Instantiate(foodPrefab, transform);
            leaf.transform.localPosition = GetRandomSpawnPos(leafSpawnSize);
            leaves.Add(leaf);
        }
    }

    void ResetFish() {
        foreach (GameObject fish in fishes) {
            fish.transform.localPosition = GetRandomSpawnPos(fishSpawnSize);
            fish.GetComponent<FishBasic>().ChangeSize(fish.transform, 1);
            fish.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public Vector3 GetRandomSpawnPos(int tankSize) {
        return new Vector3(Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize), 0);
    }

    public void ResetTank() {
        ResetFish();
        ResetLeaves();
    }
}
