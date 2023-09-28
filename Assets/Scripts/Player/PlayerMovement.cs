using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Player player;
    public float movementSpeed;
    Vector3 movementDirection;
    [Header("Steps")]
    public float maxStepHeight = 0.4f;
    public float stepSearchOvershoot = 0.01f;

    private List<ContactPoint> contactPoints = new List<ContactPoint>();
    private Vector3 lastVelocity;

    private void Awake()
    {
        player = GetComponent<Player>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            contactPoints.AddRange(collision.contacts);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision != null)
        {
            contactPoints.AddRange(collision.contacts);
        }
    }
    private void FixedUpdate()
    {

        Vector3 velocity = player.rb.velocity;

        //Filter through the ContactPoints to see if we're grounded and to see if we can step up
        ContactPoint groundCP = default(ContactPoint);
        bool grounded = FindGround(out groundCP, contactPoints);

        Vector3 stepUpOffset = default(Vector3);
        bool stepUp = false;
        if (grounded)
            stepUp = FindStep(out stepUpOffset, contactPoints, groundCP, velocity);

        //Steps
        if (stepUp)
        {
            player.rb.position += stepUpOffset;
            player.rb.velocity = lastVelocity;
        }

        contactPoints.Clear();
        lastVelocity = velocity;

    }
    bool FindGround(out ContactPoint ground, List<ContactPoint> contactPoints)
    {
        ground = default(ContactPoint);
        bool found = false;
        foreach (ContactPoint point in contactPoints)
        {
            if (point.normal.y > 0.0001f && (found == false || point.normal.y > ground.normal.y))
            {
                ground = point;
                found = true;
            }
        }
        return found;
    }
    bool FindStep(out Vector3 stepUpOffset, List<ContactPoint> contactPoints, ContactPoint ground, Vector3 currVelocity)
    {
        stepUpOffset = default(Vector3);

        Vector2 velocityXZ = new Vector2(currVelocity.x, currVelocity.z);
        if (velocityXZ.sqrMagnitude < 0.0001f)
            return false;
        foreach(ContactPoint point in contactPoints)
        {
            bool test = ResolveStepUp(out stepUpOffset, point, ground);
            if(test)
            {
                return test;
            }
        }
        return false;
    }
    bool ResolveStepUp(out Vector3 stepUpOffset, ContactPoint StepPoint, ContactPoint ground)
    {
        stepUpOffset = default (Vector3);
        Collider stepCollider = StepPoint.otherCollider;

        if(Mathf.Abs(StepPoint.normal.y) >= 0.01f)
        {
            return false;
        }
        if(!(StepPoint.point.y - ground.point.y < maxStepHeight))
        {
            return false;
        }
        RaycastHit hitInfo;
        float stepHeight = ground.point.y + maxStepHeight + 0.0001f;
        Vector3 stepTestInvDir = new Vector3(-StepPoint.normal.x, 0, -StepPoint.normal.z).normalized;
        Vector3 origin = new Vector3(StepPoint.point.x, stepHeight, StepPoint.point.z) + (stepTestInvDir * stepSearchOvershoot);
        Vector3 direction = Vector3.down;
        if(!(stepCollider.Raycast(new Ray(origin, direction), out hitInfo, maxStepHeight)))
        {
            return false;
        }
        Vector3 stepUpPoint = new Vector3(StepPoint.point.x, hitInfo.point.y + 0.0001f, StepPoint.point.z) + (stepTestInvDir * stepSearchOvershoot);
        Vector3 stepUpPointOffset = stepUpPoint - new Vector3(StepPoint.point.x,ground.point.y, StepPoint.point.z);

        stepUpOffset = stepUpPointOffset;
        return true;
    }

    public void move(Vector2 moveInput)
    {
        movementDirection = new(moveInput.x, 0, moveInput.y);
        float cameraRot = Camera.main.transform.rotation.eulerAngles.y;

        Vector3 targetDirection = Quaternion.Euler(0, cameraRot, 0) * movementDirection * movementSpeed * Time.deltaTime;

        player.rb.transform.position += targetDirection;
    }
}
