using System.Windows;
using MediaRake.ViewModels;
using MediaRake.Models;
using System.Collections.Generic;

namespace MediaRake;

public partial class MainWindow : Window
{
    private readonly MainViewModel _vm = new();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = _vm;
    }
    
    private void NewList_Click(object sender, RoutedEventArgs e)
    {
        var request = new CreateListRequest
        {
            Name = "Test List",
            Fields = new List<FieldDefinition>
            {
                new FieldDefinition { Name = "Story", Type = FieldType.Score },
                new FieldDefinition { Name = "Overall", Type = FieldType.Score }
            }
        };
        _vm.AddListCommand.Execute(request);
    }
    
    private void AddItem_Click(object sender, RoutedEventArgs e)
    {
        if (_vm.SelectedList == null) return;

        var item = new MediaItem
        {
            Title = "Test Item"
        };
        _vm.AddItemCommand.Execute(item);
    }
}