namespace Protobot.SelectionSystem {
    public class ClearInfo {
        public ISelection selection;
        public bool setting = false;

        public ClearInfo(ISelection _selection, bool _setting) {
            selection = _selection;
            setting = _setting;
        }
    }
}