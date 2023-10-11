using UnityEngine;

namespace __ProjectCodeNeon.ImplantsRenderSystem
{
    public class Skeleton
    {
        private Transform _head, _body;
        private Transform _leftShoulder, _leftHand;
        private Transform _rightShoulder, _rightHand;
        private Transform _leftLeg, _leftFoot;
        private Transform _rightLeg, _rightFoot;
        
        private Transform _leftShoulderOrigin, _leftHandOrigin;
        private Transform _rightShoulderOrigin, _rightHandOrigin;
        private Transform _legsOrigin;
        
        public Skeleton(Transform root)
        {
            _head = FindBone(root, "head");
            _body = FindBone(root, "body");
            _leftShoulder = FindBone(root, "armLeft");
            _leftHand = FindBone(root, "fistLeft");
            _rightShoulder = FindBone(root, "armRight");
            _rightHand = FindBone(root, "fistRight");
            _leftLeg = FindBone(root, "topLegLeft");
            _leftFoot = FindBone(root, "bottomLegLeft");
            _rightLeg = FindBone(root, "topLegRight");
            _rightFoot = FindBone(root, "bottomLegRight");
            
            _leftShoulderOrigin = FindBone(root, "shoulderLeftOrigin");
            _leftHandOrigin = FindBone(root, "armLeftOrigin");
            _rightShoulderOrigin = FindBone(root, "shoulderRightOrigin");
            _rightHandOrigin = FindBone(root, "armRightOrigin");
            _legsOrigin = FindBone(root, "legsOrigin");
        }

        public void EnableAllOrigins()
        {
            _leftShoulderOrigin.gameObject.SetActive(true);
            _leftHandOrigin.gameObject.SetActive(true);
            _rightShoulderOrigin.gameObject.SetActive(true);
            _rightHandOrigin.gameObject.SetActive(true);
            _legsOrigin.gameObject.SetActive(true);
        }
        
        public void ToggleOrigin(string type, bool state)
        {
            
            switch (type)
            {
                case "LeftShoulder":
                    _leftShoulderOrigin.gameObject.SetActive(!state);
                    break;
                case "LeftHand":
                    _leftHandOrigin.gameObject.SetActive(!state);
                    break;
                case "RightShoulder":
                    _rightShoulderOrigin.gameObject.SetActive(!state);
                    break;
                case "RightHand":
                    _rightHandOrigin.gameObject.SetActive(!state);
                    break;
                case "LeftLeg":
                    _legsOrigin.gameObject.SetActive(!state);
                    break;
                case "LeftFoot":
                    _legsOrigin.gameObject.SetActive(!state);
                    break;
                case "RightLeg":
                    _legsOrigin.gameObject.SetActive(!state);
                    break;
                case "RightFoot":
                    _legsOrigin.gameObject.SetActive(!state);
                    break;
            }
        }

        public Transform GetBone(string type)
        {
            switch (type)
            {
                case "Head":
                    return _head;
                case "Body":
                    return _body;
                case "LeftShoulder":
                    return _leftShoulder;
                case "LeftHand":
                    return _leftHand;
                case "RightShoulder":
                    return _rightShoulder;
                case "RightHand":
                    return _rightHand;
                case "LeftLeg":
                    return _leftLeg;
                case "LeftFoot":
                    return _leftFoot;
                case "RightLeg":
                    return _rightLeg;
                case "RightFoot":
                    return _rightFoot;
                default:
                    return null;
            }
        }
        
        private Transform FindBone(Transform root, string name)
        {
            foreach (Transform child in root)
            {
                if (child.name == name)
                    return child;
                else
                {
                    var result = FindBone(child, name);
                    if (result != null)
                        return result;
                }
            }

            return null;
        }


        public Transform Head => _head;
        public Transform Body => _body;
        public Transform LeftShoulder => _leftShoulder;
        public Transform LeftHand => _leftHand;
        public Transform RightShoulder => _rightShoulder;
        public Transform RightHand => _rightHand;
        public Transform LeftLeg => _leftLeg;
        public Transform LeftFoot => _leftFoot;
        public Transform RightLeg => _rightLeg;
        public Transform RightFoot => _rightFoot;
    }
}
