using Firebase.Auth;
using System;

namespace FireBase.Util
{
    public class UsuarioFireBase
    {
        public static string getIdUsuario()
        {
            FirebaseAuth autenticacao = ConfiguracaoFireBase.getFirebaseAutenticacao();
            return autenticacao.CurrentUser.Uid;
        }

        public static FirebaseUser getUsuarioAtual()
        {
            FirebaseAuth usuario = ConfiguracaoFireBase.getFirebaseAutenticacao();
            return usuario.CurrentUser;
        }
    }
}