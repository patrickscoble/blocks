using Android.Content;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using Google.Android.Material.BottomNavigation;

namespace Blocks
{
    public abstract class NavigationActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                {
                    Finish();
                    Intent intent = new Intent(this, typeof(HomeActivity));
                    StartActivity(intent);
                    return true;
                }
                case Resource.Id.navigation_times_tables:
                {
                    Finish();
                    Intent intent = new Intent(this, typeof(TimesTablesSetupActivity));
                    StartActivity(intent);
                    return true;
                }
                case Resource.Id.navigation_wordle:
                {
                    Finish();
                    Intent intent  = new Intent(this, typeof(WordleActivity));
                    StartActivity(intent);
                    return true;
                }
                case Resource.Id.navigation_coming_soon:
                {
                    Finish();
                    Intent intent  = new Intent(this, typeof(ComingSoonActivity));
                    StartActivity(intent);
                    return true;
                }
            }

            return false;
        }
    }
}
