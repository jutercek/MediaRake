using System.Windows;
using MediaRake.ViewModels;

namespace MediaRake;

public partial class MainWindow : Window
{
    private readonly MainViewModel _vm = new();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = _vm;
    }
}