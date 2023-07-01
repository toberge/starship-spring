using UnityEngine;

[RequireComponent(typeof(SpringJoint2D))]
public class Spring : MonoBehaviour
{

    [SerializeField]
    private Transform mesh;

    private SpringJoint2D joint;
    private Hitbox otherHitbox;

    private void Start()
    {
        joint = GetComponent<SpringJoint2D>();
        otherHitbox = joint.connectedBody.GetComponent<Hitbox>();
        otherHitbox.OnDeath += OnConnectedDeath;
    }

    private void OnDestroy()
    {
        otherHitbox.OnDeath -= OnConnectedDeath;
    }

    private void OnConnectedDeath(float damage, float remainingHealth)
    {
        // TODO explosion maybe?
        Destroy(mesh.gameObject);
        Destroy(this);
        joint.enabled = false;
    }

    private void Update()
    {
        if (joint.connectedBody == null)
        {
            Debug.LogWarning($"");
        }
        var offset = (joint.connectedBody.transform.position - transform.position);
        mesh.up = offset.normalized;
        mesh.position = transform.position;
        var length = (offset.magnitude) / 2;
        // TODO vary thickness slightly
        var thickness = 1;
        mesh.localScale = new Vector3(thickness, length, thickness);
    }
}
