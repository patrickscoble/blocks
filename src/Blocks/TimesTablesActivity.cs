using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using Blocks.Models;
using Newtonsoft.Json;

namespace Blocks
{
    [Activity(Label = "@string/title_times_tables")]
    public class TimesTablesActivity : AppCompatActivity
    {
        Button GoBackButton;
        TextView CountTextView;
        TextView TimesTableTextView;
        Button OptionOneButton;
        Button OptionTwoButton;
        Button OptionThreeButton;

        public int NumberOfQuestions;
        int Count;
        TimesTable CurrentTimesTable;

        private TimesTableCollection _timesTableCollection;
        private List<TimesTableResult> TimesTableResults = new List<TimesTableResult>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.times_tables);

            NumberOfQuestions = Intent.GetIntExtra("questions", 10);
            bool include11s = Intent.GetBooleanExtra("include11s", true);
            bool include12s = Intent.GetBooleanExtra("include12s", true);

            GoBackButton = FindViewById<Button>(Resource.Id.goBackButton);
            CountTextView = FindViewById<TextView>(Resource.Id.countTextView);
            TimesTableTextView = FindViewById<TextView>(Resource.Id.timesTableTextView);
            OptionOneButton = FindViewById<Button>(Resource.Id.optionOneButton);
            OptionTwoButton = FindViewById<Button>(Resource.Id.optionTwoButton);
            OptionThreeButton = FindViewById<Button>(Resource.Id.optionThreeButton);

            GoBackButton.Click += (sender, e) =>
            {
                Finish();
            };

            OptionOneButton.Click += OnOptionButton_Click;
            OptionTwoButton.Click += OnOptionButton_Click;
            OptionThreeButton.Click += OnOptionButton_Click;

            _timesTableCollection = new TimesTableCollection();
            _timesTableCollection.LoadTimesTables(include11s, include12s, NumberOfQuestions);

            Count = 1;

            LoadTimesTable();
        }

        private void LoadTimesTable()
        {
            Random random = new Random();

            CurrentTimesTable = _timesTableCollection.TimesTables[Count - 1];

            List<Button> buttons = new List<Button>();
            buttons.Add(OptionOneButton);
            buttons.Add(OptionTwoButton);
            buttons.Add(OptionThreeButton);
            buttons = buttons.OrderBy(button => random.Next()).ToList();

            TimesTableTextView.Text = CurrentTimesTable.ToString(false);
            buttons[0].Text = CurrentTimesTable.CalculateFirstIncorrectNumber().ToString();
            buttons[1].Text = CurrentTimesTable.Answer.ToString();
            buttons[2].Text = CurrentTimesTable.CalculateSecondIncorrectNumber().ToString();

            CountTextView.Text = $"{Count}/{NumberOfQuestions}";
        }

        private void OnOptionButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            bool result = button.Text == CurrentTimesTable.Answer.ToString();

            Color color = result ? Color.Green : Color.Red;

            PorterDuffColorFilter porterDuffColorFilter = new PorterDuffColorFilter(color, PorterDuff.Mode.Multiply);

            button.Background.SetColorFilter(porterDuffColorFilter);

            TimesTableResults.Add(
                new TimesTableResult()
                {
                    TimesTable = CurrentTimesTable,
                    QuestionNumber = Count,
                    SelectedAnswer = Convert.ToInt32(button.Text),
                    Result = result
                });

            if (Count == NumberOfQuestions)
            {
                Finish();

                Intent intent = new Intent(this, typeof(TimesTablesResultsActivity));
                intent.PutExtra("results", JsonConvert.SerializeObject(TimesTableResults));
                StartActivity(intent);
            }
            else
            {
                Count++;
                ResetButtonColor(button);
                LoadTimesTable();
            }
        }

        private async void ResetButtonColor(Button button)
        {
            int sleepTime = 150;
            await Task.Delay(sleepTime);

            button.Background.SetColorFilter(null);
        }
    }
}
