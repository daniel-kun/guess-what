namespace Io.GuessWhat.MainApp.Models
{
    /**
    A view-model for the _CheckboxControl page.
    Pass this as the model when rendering _CheckboxControl.
    Contains a few HTML elements to customize the _CheckboxControl.
    **/
    public class CheckboxControlViewModel
    {
        /**
        Initializes this checkbox control view-model with the given Id and Label.
        **/
        public CheckboxControlViewModel(string id, string label)
        {
            Id = id;
            Label = label;
        }

        /**
        The element Id for the outermost <a> element of the _CheckboxControl.
        **/
        public string Id
        {
            get;
            set;
        } = string.Empty;

        /**
        The label of the checkbox.
        **/
        public string Label
        {
            get;
            set;
        } = string.Empty;

        /**
        Additional class(es) for the outermost <a> element of the _CheckboxControl.
        **/
        public string Class
        {
            get;
            set;
        } = string.Empty;

    }
}
