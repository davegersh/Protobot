namespace FirebaseClient.Firestore {
    public static class FirestoreConfig {
        public static DatabaseReference DefaultDb =>
            new DatabaseReference("https://firestore.googleapis.com/v1/projects/vex-virtual-builder/databases/(default)/documents");
    }
}
