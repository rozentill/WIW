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
        int numBody=0;
        List<ulong> trackedIds = new List<ulong>();
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                numBody++;
                trackedIds.Add(body.TrackingId);

            }
        }


        ulong leftId, rightId;

        float x_head1 = 0, x_head2 = 0;
        float left_lhand_y = 0, left_rhand_y = 0, right_rhand_y = 0, right_lhand_y = 0, left_head_y = 0, right_head_y = 0, left_knee_y = 0, right_knee_y = 0, left_lankle_y = 0, left_rfoot_y = 0, right_rankle_y = 0, right_lfoot_y = 0;
        float left_rshoulder_x = 0, left_lshoulder_x = 0, right_lshoulder_x = 0, right_rshoulder_x = 0, left_lhand_x = 0, left_rhand_x = 0, right_lhand_x = 0, right_rhand_x = 0;
        if (trackedIds.Count == 2)
        {
            foreach (var body in data)
            {
                if (body.IsTracked)
                {
                    if (body.TrackingId == trackedIds[0])
                    {
                        x_head1 = body.Joints[Kinect.JointType.Head].Position.X;
                    }
                    else
                    {
                        x_head2 = body.Joints[Kinect.JointType.Head].Position.X;
                    }
                }
            }

            if (x_head1 < x_head2)
            {
                leftId = trackedIds[0];
                rightId = trackedIds[1];
            }
            else
            {
                leftId = trackedIds[1];
                rightId = trackedIds[0];
            }

            foreach(var body in data)
            {
                if (body.IsTracked)
                {
                    if (body.TrackingId == leftId)
                    {
                        left_lhand_y = body.Joints[Kinect.JointType.HandLeft].Position.Y;
                        left_rhand_y = body.Joints[Kinect.JointType.HandRight].Position.Y;
                        left_lhand_x = body.Joints[Kinect.JointType.HandLeft].Position.X;
                        left_rhand_x = body.Joints[Kinect.JointType.HandRight].Position.X;
                        left_head_y = body.Joints[Kinect.JointType.SpineShoulder].Position.Y;
                        left_knee_y = body.Joints[Kinect.JointType.KneeRight].Position.Y;
                        left_rshoulder_x = body.Joints[Kinect.JointType.ShoulderRight].Position.X;
                        left_lshoulder_x = body.Joints[Kinect.JointType.ShoulderLeft].Position.X;
                        left_lankle_y = body.Joints[Kinect.JointType.AnkleLeft].Position.Y;
                        left_rfoot_y = body.Joints[Kinect.JointType.FootRight].Position.Y;
                    }
                    else if (body.TrackingId == rightId)
                    {
                        right_lhand_y = body.Joints[Kinect.JointType.HandLeft].Position.Y;
                        right_rhand_y = body.Joints[Kinect.JointType.HandRight].Position.Y;
                        right_lhand_x = body.Joints[Kinect.JointType.HandLeft].Position.X;
                        right_rhand_x = body.Joints[Kinect.JointType.HandRight].Position.X;
                        right_head_y = body.Joints[Kinect.JointType.SpineShoulder].Position.Y;
                        right_knee_y = body.Joints[Kinect.JointType.KneeLeft].Position.Y;
                        right_rshoulder_x = body.Joints[Kinect.JointType.ShoulderRight].Position.X;
                        right_lshoulder_x = body.Joints[Kinect.JointType.ShoulderLeft].Position.X;
                        right_rankle_y = body.Joints[Kinect.JointType.AnkleRight].Position.Y;
                        right_lfoot_y = body.Joints[Kinect.JointType.FootLeft].Position.Y;
                    }
                }
            }

            Debug.Log("left head is :" + left_head_y + ".");

            //left-right-middle
            if (left_lhand_y < left_head_y && right_rhand_y > right_head_y)
            {
                isLeft = true;
                isRight = false;
            }
            else if (left_lhand_y > left_head_y && right_rhand_y < right_head_y)
            {
                isRight = true;
                isLeft = false;
            }
            else
            {
                isLeft = false;
                isRight = false;
            }

            //top-down-middle
            if (left_rhand_y > left_head_y || right_lhand_y > right_head_y)
            {
                isUp = true;
                isDown = false;
            }
            else if (left_rhand_y < left_knee_y || right_lhand_y < right_knee_y)
            {
                isUp = false;
                isDown = true;
            }
            else
            {
                isUp = false;
                isDown = false;

            }

            //step
            if (left_lankle_y < left_rfoot_y)//left step
            {
                isLeftStep = true;
            }
            else
            {
                isLeftStep = false;
            }

            if (right_rankle_y < right_lfoot_y)
            {
                isRightStep = true;
            }
            else
            {
                isRightStep = false;
            }

            //clap
            if (
                ((right_rhand_x - right_lhand_x)*2
                <
                (right_rshoulder_x - right_lshoulder_x))
                ||
                ((left_rhand_x - left_lhand_x) * 2
                <
                (left_rshoulder_x - left_lshoulder_x))
                )
            {
                isClap = true;
            }
            else
            {
                isClap = false;
            }

        }
        trackedIds.Clear();

    }
}
