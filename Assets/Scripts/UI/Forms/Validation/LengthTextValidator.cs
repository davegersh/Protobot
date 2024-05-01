using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protobot.UI.Forms {
    public class LengthTextValidator : MonoBehaviour, ITextValidator {
        [SerializeField] private int length;

        public enum Comparer {
            Greater,
            GreaterOrEqual,
            Lesser,
            LesserOrEqual,
            Equal,
        }

        [SerializeField] private Comparer comparison;
        public bool IsValid(string text) {
            int textLength = text.Length;

            bool equal = textLength == length;
            bool greater = textLength > length;
            bool lesser = textLength < length;

            if (comparison == Comparer.Greater) 
                return greater;

            if (comparison == Comparer.GreaterOrEqual) 
                return greater || equal;

            if (comparison == Comparer.Lesser) 
                return lesser;

            if (comparison == Comparer.LesserOrEqual)
                return lesser || equal;

            if (comparison == Comparer.Equal) 
                return equal;

            return false;
        }

        [SerializeField] private string invalidMessage;
        public string InvalidMessage => invalidMessage;
    }
}