using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Blocks.Models;
using Newtonsoft.Json;

namespace Blocks
{
    [Activity(Label = "Results")]
    public class TimesTablesResultsActivity : AppCompatActivity
    {
        Button GoBackButton;
        TextView ResultsCount;
        TextView ResultsMessage;
        TableLayout ResultsTable;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.times_tables_results);

            GoBackButton = FindViewById<Button>(Resource.Id.goBackButton);
            ResultsCount = FindViewById<TextView>(Resource.Id.resultsCountTextView);
            ResultsMessage = FindViewById<TextView>(Resource.Id.resultsMessageTextView);
            ResultsTable = FindViewById<TableLayout>(Resource.Id.resultsTable);

            GoBackButton.Click += (sender, e) =>
            {
                Finish();
            };

            List<TimesTableResult> timesTableResults = JsonConvert.DeserializeObject<List<TimesTableResult>>(Intent.GetStringExtra("results"));
            int correctAnswers = timesTableResults.Count(result => result.Result);
            int numberOfQuestions = timesTableResults.Count;
            int correctAnswersPercentage = Convert.ToInt32((double)correctAnswers / numberOfQuestions * 100);

            ResultsCount.Text = $"{correctAnswers}/{numberOfQuestions}";
            ResultsMessage.Text = GetResultsMessage(correctAnswersPercentage);

            GenerateResultsTable(timesTableResults);
        }

        private string GetResultsMessage(int correctAnswersPercentage)
        {
            if (correctAnswersPercentage < 60) return "Better luck next time!";
            if (correctAnswersPercentage < 80) return "Nice work!";
            if (correctAnswersPercentage < 100) return "Incredible!";

            return "Perfect!";
        }

        private void GenerateResultsTable(List<TimesTableResult> timesTableResults)
        {
            TableRow.LayoutParams layoutParams = new TableRow.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent, 1);

            foreach (TimesTableResult result in timesTableResults)
            {
                TableRow tableRow = new TableRow(this);
                tableRow.SetPadding(0, 0, 0, 40);

                tableRow.AddView(GetTextView(layoutParams, result.QuestionNumberString, true)); // Question Number
                tableRow.AddView(GetTextView(layoutParams, result.TimesTable.ToString(true), true)); // Times Table
                tableRow.AddView(GetTextView(layoutParams, result.SelectedAnswer.ToString(), true)); // Selected Answer
                tableRow.AddView(GetTextView(layoutParams, result.Result ? "\u2714" : "\u274C", false)); // Result

                ResultsTable.AddView(tableRow);
            }
        }

        private TextView GetTextView(TableRow.LayoutParams layoutParams, string text, bool setPadding)
        {
            TextView textView = new TextView(this)
            {
                LayoutParameters = layoutParams,
                Text = text,
                TextSize = 20
            };

            if (setPadding)
            {
                textView.SetPadding(0, 0, 120, 0);
            }

            return textView;
        }
    }
}
