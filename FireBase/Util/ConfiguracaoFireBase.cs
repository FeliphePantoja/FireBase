using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Storage;

namespace FireBase.Util
{
    public class ConfiguracaoFireBase
    {
        private static FirebaseAuth firebaseAuth;
        private static DatabaseReference databaseReference;
        private static StorageReference referenciaStorage;
        
        public static string getIdUsuario()
        {
            FirebaseAuth auth = getFirebaseAutenticacao();
            return auth.CurrentUser.Uid;
        }

        /// <summary>
        /// RETORNA A REFERENCIA DO DATABASE
        /// </summary>
        /// <returns></returns>
        public static DatabaseReference getFireBaseData()
        {
            if (databaseReference == null)
            {
                databaseReference = FirebaseDatabase.Instance.Reference;
            }

            return databaseReference;
        }

        /// <summary>
        /// RETORNA A INSTANCIA DO FIREBASEAUTH
        /// </summary>
        /// <returns></returns>
        public static FirebaseAuth getFirebaseAutenticacao()
        {
            if (firebaseAuth == null)
            {
                firebaseAuth = FirebaseAuth.Instance;
            }

            return firebaseAuth;
        }

        /// <summary>
        /// RETORNA A INSTANCIA DO FIREBASESTORAGE
        /// </summary>
        /// <returns></returns>
        public static StorageReference getFireBaseStorage()
        {
            if (referenciaStorage == null)
            {
                referenciaStorage = FirebaseStorage.Instance.Reference;
            }

            return referenciaStorage;
        }
    }
}