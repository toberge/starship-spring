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
        mesh.position = transform.position + new Vector3(offset.x / 2, offset.y / 2, 0);
        mesh.localScale = new Vector3(mesh.localScale.x, offset.magnitude / 2, mesh.localScale.z);
    }
}
