namespace Protobot.UI.Forms {
    public interface ITextValidator {
        bool IsValid(string text);
        string InvalidMessage { get; }
    }
}
