namespace FirebaseClient.Firestore {
    public class DatabaseReference {
        readonly string path;
        public DatabaseReference(string dbPath) => path = dbPath;
        public CollectionReference Collection(string colName) => new CollectionReference(path, colName);
    }
}
