using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class BodyControl : MonoBehaviour {
    public static bool isUp;
    public static bool isDown;
    public static bool isLeft;
    public static bool isRight;

    public static bool isLeftStep;
    public static bool isRightStep;
    public static bool isClap;

    public GameObject BodySourceManager;
    private List<ulong> knownIds = new List<ulong>();
    private BodySourceManager _BodyManager;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (BodySourceManager == null)
        {
            return;
        }

        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }

        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }

        //List<ulong> trackedIds = new List<ulong>();
        //foreach (var body in data)
        //{
        //    if (body == null)
        //    {
        //        continue;
        //    }

        //    if (body.IsTracked)
        //    {
        //        trackedIds.Add(body.TrackingId);
        //        if (!knownIds.Contains(body.TrackingId))
        //        {
        //            knownIds.Add(body.TrackingId);
        //        }
        //    }
        //}

        //// First delete untracked bodies
        //foreach (ulong trackingId in knownIds)
        //{
        //    if (!trackedIds.Contains(trackingId))
        //    {
        //        knownIds.Remove(trackingId);
        //    }
        //}

        foreach (var body in data)
        {
            if (body.IsTracked)
            {

                Kinect.Joint head = body.Joints[Kinect.JointType.Head];
                Kinect.Joint neck = body.Joints[Kinect.JointType.Neck];
                Kinect.Joint spineShoulder = body.Joints[Kinect.JointType.SpineShoulder];
                Kinect.Joint spineBase = body.Joints[Kinect.JointType.SpineBase];
                Kinect.Joint handTipLeft = body.Joints[Kinect.JointType.HandTipLeft];
                Kinect.Joint handTipRight = body.Joints[Kinect.JointType.HandTipRight];
                Kinect.Joint knee = body.Joints[Kinect.JointType.KneeRight];
                Kinect.Joint lHand = body.Joints[Kinect.JointType.HandLeft];
                Kinect.Joint rHand = body.Joints[Kinect.JointType.HandRight];
                Kinect.Joint lShoulder = body.Joints[Kinect.JointType.ShoulderLeft];
                Kinect.Joint rShoulder = body.Joints[Kinect.JointType.ShoulderRight];
                Kinect.Joint lFoot = body.Joints[Kinect.JointType.FootLeft];
                Kinect.Joint rFoot = body.Joints[Kinect.JointType.FootRight];
                Kinect.Joint lAnkle = body.Joints[Kinect.JointType.AnkleLeft];
                Kinect.Joint rAnkle = body.Joints[Kinect.JointType.AnkleRight];

                float deltaHorizon1 = head.Position.X - neck.Position.X;
                float deltaHorizon2 = neck.Position.X - spineShoulder.Position.X;
                
                //Debug.Log("delta 1: " + deltaHorizon1+"\n");
                //Debug.Log("delta 2: " + deltaHorizon2+ "\n");

                if (deltaHorizon1 > 5.3 * deltaHorizon2)//left
                {
                    isLeft = true;
                    isRight = false;
                }

                else if (deltaHorizon1 < 3.3 * deltaHorizon2)//right
                {

                    isRight = true;
                    isLeft = false;
                }
                else
                {
                    isLeft = false;
                    isRight = false;
                }

                if (handTipLeft.Position.Y > spineShoulder.Position.Y || handTipRight.Position.Y > spineShoulder.Position.Y)
                {
                    isUp = true;
                    isDown = false;
                }
                else if (handTipLeft.Position.Y < knee.Position.Y || handTipRight.Position.Y < knee.Position.Y)
                {
                    isUp = false;
                    isDown = true;
                }
                else
                {
                    isDown = false;
                    isUp = false;
                }

                if (
                    (rHand.Position.X - lHand.Position.X) * 2
                    <
                    (rShoulder.Position.X - lHand.Position.X)
                    )
                {
                    isClap = true;
                }
                else
                {
                    isClap = false;
                }

                if (lAnkle.Position.Y < rFoot.Position.Y)
                {
                    isRightStep = true;
                }
                else
                {
                    isRightStep = false;
                }

                if (rAnkle.Position.Y < lFoot.Position.Y)
                {
                    isLeftStep = true;
                }
                else
                {
                    isLeftStep = false;
                }
            }
        }
    }
}
