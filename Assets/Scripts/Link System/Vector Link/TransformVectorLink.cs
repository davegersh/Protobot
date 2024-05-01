using UnityEngine;
using System.Collections;

namespace Protobot {
    public class TransformVectorLink : VectorLink {
        public enum Direction {
            Up,
            Right,
            Forward,
            Down,
            Left,
            Back
        }

        [SerializeField] private Transform refTransform;
        [SerializeField] private Direction direction;

        public override Vector3 Vector => GetVector(direction, refTransform);

        public static Vector3 GetVector(Direction dir, Transform transform) {
            if (dir == Direction.Up) return transform.up;
            if (dir == Direction.Right) return transform.right;
            if (dir == Direction.Forward) return transform.forward;
            if (dir == Direction.Down) return -transform.up;
            if (dir == Direction.Left) return -transform.right;
            if (dir == Direction.Back) return -transform.forward;

            return Vector3.zero;
        }
    }
}