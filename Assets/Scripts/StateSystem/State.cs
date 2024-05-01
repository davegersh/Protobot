using System.Collections.Generic;

namespace Protobot.StateSystems {
    public class State {
        public List<IElement> elements = new List<IElement>();

        public State(List<IElement> newElements) {
            elements = newElements;
        }

        public void Load() {
            foreach (IElement element in elements)
                element.Load();
        }
    }
}