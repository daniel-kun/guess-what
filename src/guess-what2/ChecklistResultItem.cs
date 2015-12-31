namespace guess_what2.Models
{
    public class ChecklistResultItem
    {
        /**
        The original ChecklistItem that was used to enter the results
        for this checklist item.
        **/
        public ChecklistItem TemplateItem
        {
            get;
            set;
        }

        /**
        The result that the user entered for this checklist item (ok, not ok, not checked).
        **/
        public ChecklistResult Result
        {
            get;
            set;
        }
    }
}
