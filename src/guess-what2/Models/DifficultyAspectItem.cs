namespace guess_what2.Models
{
    /**
    The DifficultyAspectItem is the model for each aspect of difficulty in the two difficulty asset
    pages: page-preconditions and page-complexity (not yet implemented).
    A difficulty aspect consists of a DisplayName that is shown to the user, to the left of the buttons,
    and an internal name that is used to form the IDs of some HTML elements and will be later
    be used to store the results in a database.
    **/
    public class DifficultyAspectItem
    {
        /**
        The display name of this aspect of difficulty that is shown to the user.
        **/
        public string DisplayName
        {
            get;
            set;
        }

        /**
        The internal name of this aspect of difficulty that is used to identify that aspect.
        The aspect's names must be unique across all pages (preconditions and complexity).
        **/
        public string InternalName
        {
            get;
            set;
        }

        /**
        Specifies which difficulty is currently selected.
        When making a new estimation, always "N/A" (not available) is selected.
        When loading results, it is either read from the url (old scheme) or
        loaded from a stored session from a database (not yet implemented).
        Possible values:
        0 = Not Available
        1 = Superb / Low
        2 = Good / Normal
        3 = Not good / High
        4 = Bad / Very High
        **/
        public int SelectedDifficulty
        {
            get;
            set;
        }
    }
}
