using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using TaskStart.ViewModel;

namespace TaskStart
{
    public partial class MainWindow
    {
        private readonly MainViewModel _model;

        public MainWindow()
        {
            InitializeComponent();

            Closing += MainWindowClosing;
            var locator = new ViewModelLocator();
            _model = locator.Main;
            DataContext = _model;
        }

        private void MainWindowClosing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void CanExecuteHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        private void WindowDrop(object sender, DragEventArgs e)
        {
            var data = e.Data as DataObject;
            if (data != null && data.ContainsFileDropList())
                foreach (var filePath in data.GetFileDropList())
                    if (File.Exists(filePath))
                        _model.Add(filePath);
        }
    }
}