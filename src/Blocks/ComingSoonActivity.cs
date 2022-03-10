using Android.App;
using Android.OS;
using Google.Android.Material.BottomNavigation;

namespace Blocks
{
    [Activity(Label = "@string/title_coming_soon")]
    public class ComingSoonActivity : NavigationActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.coming_soon);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
        }
    }
}
