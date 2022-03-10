namespace Blocks.Models
{
    public class TimesTableResult
    {
        public TimesTable TimesTable { get; set; }
        public int QuestionNumber { get; set; }
        public string QuestionNumberString { get { return $"#{QuestionNumber}"; } }
        public int SelectedAnswer { get; set; }
        public bool Result { get; set; }
    }
}
