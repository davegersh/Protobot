using UnityEngine;

namespace Protobot.SelectionSystem {
    public interface IResponseSelector {
        ISelection GetResponseSelection(ISelection incomingSelection); //returns null if no new selection is found
    }
}