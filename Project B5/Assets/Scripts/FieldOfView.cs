using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

    public GameObject caughtPanel;
    public GameObject player;

    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    public float meshResolution;
    public int edgeResolveIterations;
    public float edgeDstThreshold;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    private void Start()//initializes the fov visualizer (the red mesh in the game view showing the agent's sight radius and angle)
    {
        caughtPanel.SetActive(false);
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        StartCoroutine("FindTargetsWithDelay", 0.2f);//this will run the FindTargetsWithDelay method every 0.2 seconds
    }

    IEnumerator FindTargetsWithDelay(float delay)//this will run the FindVisibleTargets method after a delay
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    private void LateUpdate()//DrawFieldOfView is run in LateUpdate so all the calculations are done already and the view visualization mesh is smoother and less jerky
    {
        DrawFieldOfView();
    }

    void FindVisibleTargets()//this method is what actually detects targets
    {
        visibleTargets.Clear();//clear out the list first so you don't get a million repeats
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);//this creates an array of all colliders inside the detect radius from before. All of them. In a sphere.

        for (int i = 0; i < targetsInViewRadius.Length; i++)//This is what will actually select which targets we want from the collider array
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)//If the target that the collider belongs to is within our detection angle..
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))//And if the target is not blocked by any obstacles in the obstacle layer
                {
                    visibleTargets.Add(target);//Then add that target to the list of visibleTargets. Since we just have one target, the player, we don't even need this. We can just pop up the caught UI.
                    Destroy(player);//Destroys player object
                    caughtPanel.SetActive(true);//Pops up restart level UI
                }
            }
        }
    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);//Amount of rays sent out to detect targets in sight
        float stepAngleSize = viewAngle / stepCount;//The amount of space between the rays
        List<Vector3> viewPoints = new List<Vector3>();//list of all points that the viewcast hits
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for (int i = 0; i <= stepCount; i++)//This creates the rays
        {
            float angle = transform.eulerAngles.y - viewAngle/2 + stepAngleSize* i;//Creating rays one by one in a clockwise manner
            //Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * viewRadius, Color.red);//This creates a visualization of the rays in the editor. Unnecessary, just bogs down the view.
            ViewCastInfo newViewCast = ViewCast(angle);

            if (i > 0)
            {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
                if(oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if(edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;//this stuff is used
        Vector3[] vertices = new Vector3[vertexCount];//to build a view mesh from
        int[] triangles = new int[(vertexCount-2)*3];//all the raycast hits

        vertices[0] = Vector3.zero;//the first vertex is always the source of the rays (the guard)
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);//makes the vertices relative to player instead of global

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minpoint = Vector3.zero;
        Vector3 maxpoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast (angle);

            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minpoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxpoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minpoint, maxpoint);
    }
    

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);//direction ray is cast in
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))//if ray hits something
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);//return a new ViewCastInfo with the info about it
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);//if it doesn't hit an obstacle, then return that stuff
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool hit;//if raycast hits something
        public Vector3 point;//end point
        public float dst;//distance of ray
        public float angle;//angle ray was fired at

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)//constructor
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}
