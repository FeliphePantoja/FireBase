using Android.App;
using Android.OS;
using Android.Views;

namespace FireBase.Views.Fragments
{
    public class LoginFragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState); 
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View v = inflater.Inflate(Resource.Layout.Tela1Layout, container, false);

            return v;
        }
        public static LoginFragment NewInstance()
        {
            return new LoginFragment();
        }
    }
}