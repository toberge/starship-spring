using UnityEngine;

public class Spring : MonoBehaviour
{

    [SerializeField]
    private Transform mesh;

    private SpringJoint2D joint;


    void Start()
    {
        joint = GetComponent<SpringJoint2D>();
    }

    private void Update()
    {
        var offset = (joint.connectedBody.transform.position - transform.position);
        mesh.up = offset.normalized;
        mesh.position = transform.position;
        var length = (offset.magnitude) / 2;
        // TODO vary thickness slightly
        var thickness = 1;
        mesh.localScale = new Vector3(thickness, length, thickness);
    }
}
