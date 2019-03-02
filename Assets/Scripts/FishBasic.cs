using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class FishBasic : Agent {
    public bool debug;
    AquariumAcademy academy;
    Tank tank;
    public int size = 1;
    public float speed = 1;
    float prevDist = 0;
    float foodDist = 0;

    float sizeDiff = 0;

    private void Awake() {
        academy = FindObjectOfType<AquariumAcademy>();
        tank = gameObject.transform.parent.GetComponent<Tank>();
    }

    public override void InitializeAgent() {
        base.InitializeAgent();
    }

    public override void CollectObservations() {
        List<float> perceptionBuffer = new List<float>();
        float tempCloseDist;
        float[] sublist;

        tempCloseDist = Mathf.Infinity;
        sublist = new float[2] { Mathf.Infinity, Mathf.Infinity };
        foreach (GameObject leaf in tank.leaves) {
            if (leaf.activeSelf) {
                Vector3 dist = Distance(transform, leaf.transform);

                if (dist.magnitude < tempCloseDist) {
                    tempCloseDist = dist.magnitude;
                    sublist[0] = dist.x;
                    sublist[1] = dist.y;
                }
            }
        }
        prevDist = foodDist;
        foodDist = new Vector3(sublist[0], sublist[1], 0).magnitude;
        perceptionBuffer.AddRange(sublist);


        sublist = new float[3] { Mathf.Infinity, Mathf.Infinity, 0 };
        tempCloseDist = Mathf.Infinity;
        foreach (GameObject fish in tank.fishes) {
            if (fish.activeSelf && fish != gameObject) {
                Vector3 dist = Distance(transform, fish.transform);

                if (dist.magnitude < tempCloseDist) {
                    tempCloseDist = dist.magnitude;
                    sublist[0] = dist.x;
                    sublist[1] = dist.y;
                    if (size - fish.GetComponent<FishBasic>().size != 0) {
                        sizeDiff = size - fish.GetComponent<FishBasic>().size;
                        sublist[2] = sizeDiff;
                    }
                }
            }
        }
        perceptionBuffer.AddRange(sublist);

        if (debug)
            Debug.Log(perceptionBuffer[0] + " " + perceptionBuffer[1] + " " + perceptionBuffer[2] + " " + perceptionBuffer[3] + " " + perceptionBuffer[4]);

        AddVectorObs(perceptionBuffer);
    }

    public Vector3 Distance(Transform trans1, Transform trans2) {
        float dist = Vector3.Distance(trans1.localPosition, trans2.localPosition) - trans1.localScale.x/2 - trans2.localScale.x/2;
        return dist * (trans2.localPosition - trans1.localPosition).normalized;
    }

    public void MoveAgent(float[] act) {

        Vector3 dirToGo = Vector3.zero;

        dirToGo += transform.up * act[0];
        dirToGo += transform.right * act[1];

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(dirToGo * tank.fishSpeed * speed, ForceMode.Acceleration);
        if (rb.velocity.magnitude > tank.fishSpeed * speed) {
            rb.velocity = rb.velocity.normalized * tank.fishSpeed * speed;
        }
    }

    public override void AgentAction(float[] vectorAction, string textAction) {
        // Move the agent using the action.
        MoveAgent(vectorAction);
        
        AddReward(-1f / agentParameters.maxStep);

        if (foodDist < prevDist) {
            AddReward(0.25f / agentParameters.maxStep);
        }
    }


    public void EatLeaf(GameObject leaf) {
        AddReward(1f);
        ChangeSize(transform, size + 1);
        leaf.transform.localPosition = tank.GetRandomSpawnPos(tank.leafSpawnSize);
    }

    public void EatFish(FishBasic fish) {
        AddReward(7.5f);
        ChangeSize(transform, size + fish.size);
        fish.Eaten();
    }

    public void Eaten() {
        AddReward(-7.5f);
        transform.localPosition = tank.GetRandomSpawnPos(tank.fishSpawnSize);
        ChangeSize(transform, 1);
    }

    public void ChangeSize(Transform fishTrans, int fishSize) {
        size = fishSize;
        fishTrans.transform.localScale = Vector3.one * Mathf.Sqrt(fishSize) / 2;
        speed = Mathf.Max(0.5f, 1 - (fishSize / 10f));
    }

    private void OnCollisionEnter(Collision collision) {
        Col(collision);
    }
    private void OnCollisionStay(Collision collision) {
        Col(collision);
    }
    private void Col(Collision collision) {
        if (collision.gameObject.CompareTag("fish")) {
            FishBasic fish1 = this;
            FishBasic fish2 = collision.gameObject.GetComponent<FishBasic>();

            if (fish1.size > fish2.size) {
                EatFish(fish2);
            }

        }
    }

    public override void AgentReset() {
        tank.ResetTank();
    }
}
