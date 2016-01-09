namespace Io.GuessWhat.MainApp.Models
{
    /**
    ChecklistResult contains all possible selections that the user can
    make for an individual ChecklistItem when filling out a checklist.
    **/
    public enum ChecklistResult
    {
        /// Checked and OK
        CheckedAndOk,
        /// Checked, not OK
        CheckedAndNotOk,
        /// Not checked
        NotChecked,
    }
}
