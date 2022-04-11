using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.Core.Text;
using Blocks.Models;
using Google.Android.Material.BottomNavigation;

namespace Blocks
{
    [Activity(Label = "@string/title_wordle")]
    public class WordleActivity : NavigationActivity
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Green = "00ff00";
        private const string Yellow = "ffff00";
        private const string Gray = "888888";

        int Round;
        string Word;

        Button SubmitButton;
        TextView GuessedLettersTextView;
        TextView ResultTextView;

        Dictionary<int, List<EditText>> Grid;
        Dictionary<char, string> GuessedLetters;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.wordle);

            Round = 1;
            Word = new WordCollection().GetRandomWord();

            PopulateGrid();
            PopulateGuessedLetters();

            foreach (List<EditText> row in Grid.Values)
            {
                SetFocus(row);
            }

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            SubmitButton = FindViewById<Button>(Resource.Id.submitButton);
            SubmitButton.Click += OnSubmitButton_Click;

            GuessedLettersTextView = FindViewById<TextView>(Resource.Id.guessedLettersTextView);
            ResultTextView = FindViewById<TextView>(Resource.Id.resultTextView);

            // Set the focus to the first cell in the grid.
            Grid[1][0].RequestFocus();
            this.Window.SetSoftInputMode(SoftInput.StateVisible);
        }

        private void OnSubmitButton_Click(object sender, EventArgs e)
        {
            Color correctLocationColor = Color.Green;
            Color correctLetterColor = Color.Yellow;
            Color incorrectColor = Color.Gray;

            PorterDuffColorFilter correctLocationPorterDuffColorFilter = new PorterDuffColorFilter(correctLocationColor, PorterDuff.Mode.Multiply);
            PorterDuffColorFilter correctLetterPorterDuffColorFilter = new PorterDuffColorFilter(correctLetterColor, PorterDuff.Mode.Multiply);
            PorterDuffColorFilter incorrectPorterDuffColorFilter = new PorterDuffColorFilter(incorrectColor, PorterDuff.Mode.Multiply);

            int position = 0;
            bool correctAnswer = true;

            List<EditText> row = Grid[Round];
            foreach (EditText cell in row)
            {
                char letter = cell.Text[0];

                if (letter == Word[position])
                {
                    // Correct letter in the correct position.
                    cell.Background.SetColorFilter(correctLocationPorterDuffColorFilter);
                    GuessedLetters[letter] = GetGuessedLetterHtml(Green, letter);
                }
                else if (Word.Contains(letter))
                {
                    // Correct letter in the incorrect position.
                    cell.Background.SetColorFilter(correctLetterPorterDuffColorFilter);

                    // Make sure the letter hasn't already been flagged as green.
                    if (GuessedLetters[letter] != GetGuessedLetterHtml(Green, letter))
                    {
                        GuessedLetters[letter] = GetGuessedLetterHtml(Yellow, letter);
                    }

                    correctAnswer = false;
                }
                else
                {
                    // Incorrect letter.
                    cell.Background.SetColorFilter(incorrectPorterDuffColorFilter);
                    GuessedLetters[letter] = GetGuessedLetterHtml(Gray, letter);
                    correctAnswer = false;
                }

                position++;
                cell.Enabled = false;
            }

            if (Round < Grid.Count && !correctAnswer)
            {
                List<EditText> nextRow = Grid[Round + 1];
                foreach (EditText cell in nextRow)
                {
                    cell.Enabled = true;
                }

                InputMethodManager imm = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
                nextRow[0].RequestFocus();
                imm.ShowSoftInput(row[0], ShowFlags.Forced);

                Round++;
            }
            else
            {
                SubmitButton.Enabled = false;

                if (!correctAnswer)
                {
                    ResultTextView.Text = $"The word was {Word}.";
                }
            }

            string guessedLetters = string.Empty;

            foreach (string value in GuessedLetters.Values)
            {
                guessedLetters += value + " ";
            }

            GuessedLettersTextView.SetText(HtmlCompat.FromHtml(guessedLetters, HtmlCompat.FromHtmlModeLegacy), TextView.BufferType.Spannable);
        }

        private void PopulateGrid()
        {
            Grid = new Dictionary<int, List<EditText>>
            {
                {
                    1, new List<EditText>
                    {
                        FindViewById<EditText>(Resource.Id.cell1),
                        FindViewById<EditText>(Resource.Id.cell2),
                        FindViewById<EditText>(Resource.Id.cell3),
                        FindViewById<EditText>(Resource.Id.cell4),
                        FindViewById<EditText>(Resource.Id.cell5)
                    }
                },
                {
                    2, new List<EditText>
                    {
                        FindViewById<EditText>(Resource.Id.cell6),
                        FindViewById<EditText>(Resource.Id.cell7),
                        FindViewById<EditText>(Resource.Id.cell8),
                        FindViewById<EditText>(Resource.Id.cell9),
                        FindViewById<EditText>(Resource.Id.cell10)
                    }
                },
                {
                    3, new List<EditText>
                    {
                        FindViewById<EditText>(Resource.Id.cell11),
                        FindViewById<EditText>(Resource.Id.cell12),
                        FindViewById<EditText>(Resource.Id.cell13),
                        FindViewById<EditText>(Resource.Id.cell14),
                        FindViewById<EditText>(Resource.Id.cell15)
                    }
                },
                {
                    4, new List<EditText>
                    {
                        FindViewById<EditText>(Resource.Id.cell16),
                        FindViewById<EditText>(Resource.Id.cell17),
                        FindViewById<EditText>(Resource.Id.cell18),
                        FindViewById<EditText>(Resource.Id.cell19),
                        FindViewById<EditText>(Resource.Id.cell20)
                    }
                },
                {
                    5, new List<EditText>
                    {
                        FindViewById<EditText>(Resource.Id.cell21),
                        FindViewById<EditText>(Resource.Id.cell22),
                        FindViewById<EditText>(Resource.Id.cell23),
                        FindViewById<EditText>(Resource.Id.cell24),
                        FindViewById<EditText>(Resource.Id.cell25)
                    }
                },
                {
                    6, new List<EditText>
                    {
                        FindViewById<EditText>(Resource.Id.cell26),
                        FindViewById<EditText>(Resource.Id.cell27),
                        FindViewById<EditText>(Resource.Id.cell28),
                        FindViewById<EditText>(Resource.Id.cell29),
                        FindViewById<EditText>(Resource.Id.cell30)
                    }
                }
            };
        }

        private void PopulateGuessedLetters()
        {
            GuessedLetters = new Dictionary<char, string>();

            foreach (char letter in Alphabet)
            {
                GuessedLetters.Add(letter, letter.ToString());
            }
        }

        private void SetFocus(List<EditText> row)
        {
            for (int i = 0; i < row.Count - 1; i++)
            {
                EditText currentLetter = row[i];
                EditText nextLetter = row[i + 1];

                currentLetter.TextChanged += (object sender, TextChangedEventArgs e) =>
                {
                    if (currentLetter.Text != string.Empty)
                    {
                        nextLetter.RequestFocus();
                    }
                };
            }

            for (int i = row.Count - 1; i > 0; i--)
            {
                EditText currentLetter = row[i];
                EditText previousLetter = row[i - 1];

                currentLetter.KeyPress += (object sender, View.KeyEventArgs e) =>
                {
                    if (e.KeyCode != Keycode.Del)
                    {
                        e.Handled = false;
                        return;
                    }

                    if (e.Event.Action == KeyEventActions.Down)
                    {
                        if (currentLetter.Text != string.Empty)
                        {
                            currentLetter.Text = string.Empty;
                        }
                        else
                        {
                            previousLetter.Text = string.Empty;
                            previousLetter.RequestFocus();
                        }
                    }
                };
            }
        }

        private string GetGuessedLetterHtml(string color, char letter)
        {
            return $"<span style=\"background-color:#{color}\">{letter}</span>";
        }
    }
}
