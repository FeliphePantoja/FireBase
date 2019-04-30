using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Database;
using FireBase.Activitys;
using FireBase.Util;
using FireBase.Views.Fragments;
using System;

namespace FireBase
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnCompleteListener, IValueEventListener, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private FirebaseAuth firebaseAuth;
        private DatabaseReference databaseReference;

        private BottomNavigationView navigationView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            this.navigationView = (BottomNavigationView)FindViewById(Resource.Id.navigationView);
             
            //this.configuracaoFireBase = new ConfiguracaoFireBase(this);

            this.firebaseAuth = ConfiguracaoFireBase.getFirebaseAutenticacao();
            this.databaseReference = ConfiguracaoFireBase.getFireBaseData();

            verificarUsuarioLogado();

            this.navigationView.SetOnNavigationItemSelectedListener(this);
            this.navigationView.SelectedItemId = Resource.Id.navigation_songs;
        }

        private void verificarUsuarioLogado()
        {
            if (this.firebaseAuth.CurrentUser != null)
            {
                Toast.MakeText(this, "Usuário Logado", ToastLength.Short).Show();
            }
            else
            {
                /* Criando usuario */
                this.firebaseAuth.CreateUserWithEmailAndPassword("FireBaseXamarinForms@gmail.com", "123456789").AddOnCompleteListener(this, this);
            }
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                Toast.MakeText(this, "Cadastro realizado com sucesso!", ToastLength.Short).Show();
            }
            else
            {
                string erroExcecao = "";

                try
                {
                    throw task.Exception;
                }
                catch (FirebaseAuthWeakPasswordException e)
                {
                    erroExcecao = "Digite uma senha mais forte!";
                }
                catch (FirebaseAuthInvalidCredentialsException e)
                {
                    erroExcecao = "Por favor, digite um e-mail válido";
                }
                catch (FirebaseAuthUserCollisionException e)
                {
                    Login();
                    erroExcecao = "Este conta já foi cadastrada";
                }
                catch (Exception e)
                {
                    erroExcecao = "ao cadastrar usuário: " + e.Message;
                    var res = e.StackTrace;
                }
            }
        }
        private void BtCadastro_Click(object sender, EventArgs e)
        {
            this.databaseReference.KeepSynced(false);
            this.databaseReference.Child("teste").Child(UsuarioFireBase.getIdUsuario()).Child("Dados Teste").SetValue("teste Felipe Pantoja");
            this.databaseReference.AddValueEventListener(this);
        }

        private void Login()
        {
            if (this.firebaseAuth.CurrentUser == null)
            {
                this.firebaseAuth.SignInWithEmailAndPassword("FireBaseXamarinForms@gmail.com", "123456789").AddOnCompleteListener(this);
            }
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

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_sair:
                    this.firebaseAuth.SignOut();
                    break;

                case Resource.Id.menu_logar:
                    StartActivity(new Intent(this, typeof(LoginActivity)));
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_songs:
                    SupportActionBar.Title = "Login";
                    openFragment(LoginFragment.NewInstance());
                    break;

                case Resource.Id.navigation_albums:
                    SupportActionBar.Title = "MAin";
                    openFragment(MainFragment.NewInstance());
                    break;

                case Resource.Id.navigation_artists:
                    SupportActionBar.Title = "MAin2";
                    openFragment(MainFragment.NewInstance());
                    break;
            }

            return true;
        }

        private void openFragment(Android.Support.V4.App.Fragment fragment)
        {
            Android.Support.V4.App.FragmentTransaction transaction = SupportFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.container, fragment);
            //transaction.AddToBackStack(null);
            transaction.Commit();
        }

        public override void OnBackPressed()
        {
            if (navigationView.SelectedItemId != Resource.Id.navigation_songs)
            {
                this.navigationView.SelectedItemId = Resource.Id.navigation_songs;
            }
            else
            {
                base.OnBackPressed();
            }
        }
    }
}