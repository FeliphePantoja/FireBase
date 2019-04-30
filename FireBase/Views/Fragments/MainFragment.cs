using Android.OS;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using FireBase.Util;

namespace FireBase.Views.Fragments
{
    public class MainFragment : Android.Support.V4.App.Fragment, IValueEventListener
    {
        private Button btCadastro;

        private DatabaseReference databaseReference;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public static MainFragment NewInstance()
        {
            return new MainFragment();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            this.databaseReference = ConfiguracaoFireBase.getFireBaseData();

            View view = inflater.Inflate(Resource.Layout.Tela1Layout, container, false);

            this.btCadastro = (Button)view.FindViewById(Resource.Id.BtCadastro);


            /* Eventos */
            this.btCadastro.Click += BtCadastro_Click;
            return view;
        }

        private void BtCadastro_Click(object sender, System.EventArgs e)
        {
            this.databaseReference.KeepSynced(false);
            this.databaseReference.Child("teste").Child(UsuarioFireBase.getIdUsuario()).Child("Dados Teste").SetValue("teste Felipe Pantoja");
            this.databaseReference.AddValueEventListener(this);
        }

        public void OnCancelled(DatabaseError error)
        {
            var res = error;
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Value != null)
            {
                var res = snapshot.Value;
            }
        }
    }
}