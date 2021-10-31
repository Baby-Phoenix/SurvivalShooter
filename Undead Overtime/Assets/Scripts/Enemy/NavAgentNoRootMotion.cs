﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentNoRootMotion : MonoBehaviour
{
    public AIWaypointNetwork WaypointNetwork = null;
    public int               CurrentIndex    = 0;
    public bool              HasPath         = false;
    public bool              PathPending     = false;
    public bool              PathStale       = false;
    public NavMeshPathStatus PathStatus      = NavMeshPathStatus.PathInvalid;
    public AnimationCurve    JumpCurve       = new AnimationCurve();

    //Private members
    private NavMeshAgent _navAgent         = null;
    private Animator     _animator         = null;
    private float        _originalMaxSpeed = 0;

    void Start()
    {
        //Cache NavMeshAgent reference
        _navAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        if (_navAgent)
            _originalMaxSpeed = _navAgent.speed;

        //Turn off auto-update
        //_navAgent.updatePosition = false;
        //_navAgent.updateRotation = false;

        //If not valid Waypoint Network has been assigned then return
        if (WaypointNetwork == null) return;

        SetNextDestination(false);
    }

    void SetNextDestination(bool increment)
    {
        //If no network return
        if (!WaypointNetwork) return;

        //Calculate how much the current waypoint index needs to be incremented
        int incStep = increment ? 1 : 0;
        Transform nextWaypointTransform = null;

        int nextWaypoint = (CurrentIndex + incStep >= WaypointNetwork.Waypoints.Count) ? 0 : CurrentIndex + incStep;
        nextWaypointTransform = WaypointNetwork.Waypoints[nextWaypoint];

        if (nextWaypointTransform != null)
        {
            //Update the current waypoint index and assign its position as the NavMeshAgents destination
            CurrentIndex = nextWaypoint;
            _navAgent.destination = nextWaypointTransform.position;
            return;
        }

        //Did not find a valid waypoint in the list for this iteration
        CurrentIndex++;
    }

    void Update()
    {
        int turnOnSpot;

        //Copy NavMeshAgents state into inspector visible variables
        HasPath     = _navAgent.hasPath;
        PathPending = _navAgent.pathPending;
        PathStale   = _navAgent.isPathStale;
        PathStatus  = _navAgent.pathStatus;

        Vector3 cross = Vector3.Cross(transform.forward, _navAgent.desiredVelocity.normalized);
        float horizontal = (cross.y < 0) ? -cross.magnitude : cross.magnitude;
        horizontal = Mathf.Clamp(horizontal * 2.32f, -2.32f, 2.32f);

        if(_navAgent.desiredVelocity.magnitude < 1.0f && Vector3.Angle(transform.forward, _navAgent.desiredVelocity) > 20.0f)
        {
            _navAgent.speed = 0.1f;
            turnOnSpot = (int)Mathf.Sign(horizontal);
        }
        else
        {
            _navAgent.speed = _originalMaxSpeed;
            turnOnSpot = 0;
        }

        _animator.SetFloat("Horizontal", horizontal, 0.1f, Time.deltaTime);
        _animator.SetFloat("Vertical", _navAgent.desiredVelocity.magnitude, 0.1f, Time.deltaTime);
        _animator.SetInteger("TurnOnSpot", turnOnSpot);

        //if(_navAgent.isOnOffMeshLink)
        //{
        //    StartCoroutine( Jump(1.0f) );
        //    return;
        //}

        //If we don't have a path and none pending, set next waypoint as target, otherwise if path is stale regenerate path
        if ( (_navAgent.remainingDistance <= _navAgent.stoppingDistance && !PathPending) || PathStatus == NavMeshPathStatus.PathInvalid /*|| PathStatus == NavMeshPathStatus.PathPartial*/)
            SetNextDestination(true);
        else if (_navAgent.isPathStale)
            SetNextDestination(false);
    }

    IEnumerator Jump (float duration)
    {
        OffMeshLinkData     data        = _navAgent.currentOffMeshLinkData;
        Vector3             startPos    = _navAgent.transform.position;
        Vector3             endPos      = data.endPos + (_navAgent.baseOffset * Vector3.up);
        float               time        = 0.0f;

        while (time <= duration)
        {
            float t = time / duration;
            _navAgent.transform.position = Vector3.Lerp(startPos, endPos, t) + (JumpCurve.Evaluate(t) * Vector3.up);
            time += Time.deltaTime;
            yield return null;
        }

        _navAgent.CompleteOffMeshLink();
    }
}
