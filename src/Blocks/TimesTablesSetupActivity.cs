using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Google.Android.Material.BottomNavigation;

namespace Blocks
{
    [Activity(Label = "@string/title_times_tables")]
    public class TimesTablesSetupActivity : NavigationActivity
    {
        RadioGroup Questions;
        ToggleButton Include11s;
        ToggleButton Include12s;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.times_tables_setup);

            Questions = FindViewById<RadioGroup>(Resource.Id.questions);
            Include11s = FindViewById<ToggleButton>(Resource.Id.include11s);
            Include12s = FindViewById<ToggleButton>(Resource.Id.include12s);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            Button startButton = FindViewById<Button>(Resource.Id.startButton);
            startButton.Click += (sender, e) =>
            {
                RadioButton questions = FindViewById<RadioButton>(Questions.CheckedRadioButtonId);
                int questionsAmount = Convert.ToInt32(questions.Text);

                Intent intent = new Intent(this, typeof(TimesTablesActivity));
                intent.PutExtra("questions", questionsAmount);
                intent.PutExtra("include11s", Include11s.Checked);
                intent.PutExtra("include12s", Include12s.Checked);
                StartActivity(intent);
            };
        }
    }
}
