using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class IKFootBehavior : MonoBehaviour
{
    [SerializeField] private Transform _footTransformRF;
    [SerializeField] private Transform _footTransformRB;
    [SerializeField] private Transform _footTransformLB;
    [SerializeField] private Transform _footTransformLF;
    private Transform[] _allFootTransforms;
    [SerializeField] private Transform _footTargetTransformRF;
    [SerializeField] private Transform _footTargetTransformRB;
    [SerializeField] private Transform _footTargetTransformLB;
    [SerializeField] private Transform _footTargetTransformLF;
    private Transform[] _allTargetTransforms;
    [SerializeField] private GameObject _footRigRF;
    [SerializeField] private GameObject _footRigRB;
    [SerializeField] private GameObject _footRigLB;
    [SerializeField] private GameObject _footRigLF;
    private TwoBoneIKConstraint[] _allFootIkConstraints;
    private LayerMask _groundLayerMask;
    public float _maxHitDistance = 5f; //can adjust this value of not working properly
    public float _addedHeight = 2.25f;
    private bool[] _allGroundSpherecastHits;
    private LayerMask _hitLayer;
    private Vector3[] _allHitNormals;
    private float angleAboutX;
    private float angleAboutZ;
    public float yOffset = 0.02f;//end joint and the ground between the foots thicknes so that the bone dosent go into the ground
    public float spherecastRadius = .2f;
    [SerializeField] private Animator _animator;
    private float[] _allFootWeights;
    private Vector3 averageHitNormal;


    void Start()
    {
        _allFootTransforms = new Transform[4];
        _allFootTransforms[0] = _footTransformRF;
        _allFootTransforms[1] = _footTransformRB;
        _allFootTransforms[2] = _footTransformLB;
        _allFootTransforms[3] = _footTransformLF;

        _allTargetTransforms = new Transform[4];
        _allTargetTransforms[0] = _footTargetTransformRF;
        _allTargetTransforms[1] = _footTargetTransformRB;
        _allTargetTransforms[2] = _footTargetTransformLB;
        _allTargetTransforms[3] = _footTargetTransformLF;

        _allFootIkConstraints = new TwoBoneIKConstraint[4];
        _allFootIkConstraints[0] = _footRigRF.GetComponent<TwoBoneIKConstraint>();
        _allFootIkConstraints[1] = _footRigRB.GetComponent<TwoBoneIKConstraint>();
        _allFootIkConstraints[2] = _footRigLB.GetComponent<TwoBoneIKConstraint>();
        _allFootIkConstraints[3] = _footRigLF.GetComponent<TwoBoneIKConstraint>();
        _groundLayerMask = LayerMask.NameToLayer("Walkable");
        _allGroundSpherecastHits = new bool[5];

        _allHitNormals = new Vector3[4];
        _allFootWeights = new float[4];
    }


    // Update is called once per frame
    void Update()
    {
        RotateCharacterFeet();
        RotateCharactertBody();
    }

    //We could also create a method that we can switch too if the pet is playing with a toy with his paws to project the corret angle the paw should be at
    private void CheckGroundBelow(out Vector3 hitPoint, out bool gotGroundSphearcastHit, out Vector3 hitNormal, out LayerMask _hitLayer, out float currentHitDistance, Transform objectTransform, int checkForLayerMask, float maxHitDistance, float addedHeight)
    {
        RaycastHit hit;
        Vector3 startSpherecast = objectTransform.position + new Vector3(0f, addedHeight, 0f);
        if (checkForLayerMask == -1)
        {
            Debug.LogError("Layer Does Not Exist for walking on hit Layer");
            gotGroundSphearcastHit = false;
            currentHitDistance = 0f;
            _hitLayer = LayerMask.NameToLayer("Pet");
            hitNormal = Vector3.up;
            hitPoint = objectTransform.position;
        }
        else
        {
            int layerMask = (1 << checkForLayerMask);
            if (Physics.SphereCast(startSpherecast, spherecastRadius, Vector3.down, out hit, maxHitDistance, layerMask, QueryTriggerInteraction.UseGlobal))//might be able to use raycast instead since the foot point is tiny and not a wide foot
            {
                _hitLayer = hit.transform.gameObject.layer;
                currentHitDistance = hit.distance - addedHeight;
                hitNormal = hit.normal;
                gotGroundSphearcastHit = true;
                hitPoint = hit.point;
            }
            else
            {
                gotGroundSphearcastHit = false;
                currentHitDistance = 0f;
                _hitLayer = LayerMask.NameToLayer("Pet");
                hitNormal = Vector3.up;
                hitPoint = objectTransform.position;
            }
        }
    }

    Vector3 ProjectOnContactPlane(Vector3 vector, Vector3 hitNormal)
    {
        return vector - hitNormal * Vector3.Dot(vector, hitNormal);
    }

    // takes the x and z targets of the feet and project them on the ground
    private void ProjectedAxisAngles(out float angleAboutX, out float angleAboutZ, Transform footTargetTransform, Vector3 hitNormal)
    {
        Vector3 xAxisProjected = ProjectOnContactPlane(footTargetTransform.forward, hitNormal).normalized;
        Vector3 ZAxisProjected = ProjectOnContactPlane(footTargetTransform.right, hitNormal).normalized;

        angleAboutX = Vector3.SignedAngle(footTargetTransform.forward, xAxisProjected, footTargetTransform.right);
        angleAboutZ = Vector3.SignedAngle(footTargetTransform.right, ZAxisProjected, footTargetTransform.forward);
    }
    private void RotateCharacterFeet()
    {
        _allFootWeights[0] = _animator.GetFloat("RF foot weight");
        _allFootWeights[1] = _animator.GetFloat("RB foot weight");
        _allFootWeights[2] = _animator.GetFloat("LF foot weight");
        _allFootWeights[3] = _animator.GetFloat("LB foot weight");

        for (int i = 0; i < 4; i++)
        {
            _allFootIkConstraints[i].weight = _allFootWeights[i];

            CheckGroundBelow(out Vector3 hitPoint, out _allGroundSpherecastHits[i], out Vector3 hitNormal, out _hitLayer, out _, _allFootTransforms[i], _groundLayerMask, _maxHitDistance, _addedHeight);
            _allHitNormals[i] = hitNormal;

            if (_allGroundSpherecastHits[i] == true)
            {
                yOffset = 0.02f;
                if (_allFootTransforms[i].position.y < _allTargetTransforms[i].position.y - 0.1f)
                {
                    yOffset += _allTargetTransforms[i].position.y - _allFootTransforms[i].position.y;
                }
                ProjectedAxisAngles(out angleAboutX, out angleAboutZ, _allFootTransforms[i], _allHitNormals[i]);

                _allTargetTransforms[i].position = new Vector3(_allFootTransforms[i].position.x, hitPoint.y + yOffset, _allFootTransforms[i].position.z);

                _allTargetTransforms[i].rotation = _allFootTransforms[i].rotation;

                _allTargetTransforms[i].localEulerAngles = new Vector3(_allTargetTransforms[i].localEulerAngles.x + angleAboutX, _allTargetTransforms[i].localEulerAngles.y, _allTargetTransforms[i].localEulerAngles.z + angleAboutZ);
            }
            else
            {
                _allTargetTransforms[i].position = _allFootTransforms[i].position;
                _allTargetTransforms[i].rotation = _allFootTransforms[i].rotation;
            }
        }
    }

    private void RotateCharactertBody()
    {
        float _maxRotationStep = 1f;
        float averageHitNormalX = 0f;
        float averageHitNormalY = 0f;
        float averageHitNormalZ = 0f;
        for (int i = 0; i < 4; i++)
        {
            averageHitNormalX = _allHitNormals[i].x;
            averageHitNormalY = _allHitNormals[i].y;
            averageHitNormalZ = _allHitNormals[i].z;
        }
        averageHitNormal = new Vector3(averageHitNormalX / 4, averageHitNormalY / 4, averageHitNormalZ / 4).normalized;

        ProjectedAxisAngles(out angleAboutX, out angleAboutZ, transform, averageHitNormal);

        float maxRotationX = 50f;
        float maxRotationZ = 20f;

        float characterXRotation = transform.eulerAngles.x;
        float characterZRotation = transform.eulerAngles.z;

        if (characterXRotation > 180f)
        {
            characterXRotation -= 360f;
        }
        if (characterZRotation > 180f)
        {
            characterZRotation -= 360f;
        }

        if (characterXRotation + angleAboutX < -maxRotationX)
        {
            angleAboutX = maxRotationX + characterXRotation;
        }
        else if (characterXRotation + angleAboutX > maxRotationX)
        {
            angleAboutX = maxRotationX - characterXRotation;
        }
        if (characterZRotation + angleAboutZ < -maxRotationZ)
        {
            angleAboutZ = maxRotationZ + characterZRotation;
        }
        else if (characterZRotation + angleAboutZ > maxRotationZ)
        {
            angleAboutZ = maxRotationZ - characterZRotation;
        }

        float bodyEulerX = Mathf.MoveTowardsAngle(0, angleAboutX, _maxRotationStep);
        float bodyEulerZ = Mathf.MoveTowardsAngle(0, angleAboutZ, _maxRotationStep);

        transform.eulerAngles = new Vector3(transform.eulerAngles.x + bodyEulerX, transform.eulerAngles.y, transform.eulerAngles.z + angleAboutZ);
    }
}
