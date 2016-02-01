using System.Diagnostics.CodeAnalysis;

namespace TaskStart.ViewModel
{
    public class ViewModelLocator
    {
        private static MainViewModel _main;

        public ViewModelLocator()
        {
            _main = new MainViewModel();
        }

        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get { return _main; }
        }
    }
}