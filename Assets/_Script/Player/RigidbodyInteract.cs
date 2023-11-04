using UnityEngine;

public class RigidbodyInteract : MonoBehaviour
{
    [SerializeField] private LayerMask pushLayers;
    [SerializeField] private bool canPush;
    [Range(0.01f, 5f), SerializeField] private float normalStrength = 1.1f, smallSizeStrenth = 0.1f;
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (canPush) PushRigidBodies(hit);
    }
    private void PushRigidBodies(ControllerColliderHit hit)
    {
        // make sure we hit a non kinematic rigidbody
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic) return;
        // make sure we only push desired layer(s)
        var bodyLayerMask = 1 << body.gameObject.layer;
        if ((bodyLayerMask & pushLayers.value) == 0) return;
        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f) return;
        // Calculate push direction from move direction, horizontal motion only
        Vector3 pushDir = new(hit.moveDirection.x, 0.0f, hit.moveDirection.z);
        // Apply the push and take strength into account
        if (!GetComponent<ShapeShiftPower>().IsCurrentlySmall)
        {
            body.AddForce(pushDir * normalStrength, ForceMode.Impulse);
        }
        else
        {
            body.AddForce(pushDir * smallSizeStrenth, ForceMode.Impulse);
        }
    }
}
