using UnityEngine;

public class TentacleFollowing : MonoBehaviour
{
    [SerializeField] private float smoothSpeed;
    [SerializeField] private Transform target;
    [SerializeField] private float targetDistance;

    [SerializeField] private Transform[] bodyParts;
    [SerializeField] private Transform tailEnd;

    private Vector3[] segmentPoses;
    private Vector3[] segmentVelocity;
    private int length;

    private void Awake() {
        length = bodyParts.Length + 1;
        segmentPoses = new Vector3[length];
        segmentVelocity = new Vector3[length];
        ResetPositions();
    }

    private void Update() {
        Following();
    }

    private void Following() {
        segmentPoses[0] = target.position;
        for (int i = 1; i < segmentPoses.Length; i++) {
            float distance = Vector2.Distance(segmentPoses[i - 1], segmentPoses[i]);
            Vector3 targetPosition = segmentPoses[i - 1] + (segmentPoses[i] - segmentPoses[i - 1]).normalized * targetDistance;
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], targetPosition, ref segmentVelocity[i], smoothSpeed);
            bodyParts[i - 1].transform.position = segmentPoses[i];
        }
        tailEnd.position = segmentPoses[segmentPoses.Length - 1];
    }

    private void ResetPositions() {
        segmentPoses[0] = target.position;
        for (int i = 1; i < length; i++)
            segmentPoses[i] = segmentPoses[i - 1] + Vector3.right * 0.01f;
    }
}
