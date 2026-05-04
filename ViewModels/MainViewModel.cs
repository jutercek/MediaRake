using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediaRake.Models;
using MediaRake.Services;
using System.Collections.ObjectModel;

namespace MediaRake.ViewModels;

public partial class MainViewModel : ObservableObject
{
    // We need an instance all the time even if empty
    [ObservableProperty]
    private ObservableCollection<RatingList> lists = new();

    [ObservableProperty]
    private RatingList? selectedList;

    private readonly DataService _dataService = new();
    // Constructor
    public MainViewModel()
    {
        var data = _dataService.Load();
        lists = new ObservableCollection<RatingList>(data.Lists);
    }

    private void Save()
    {
        var data = new AppData { Lists = Lists.ToList() };
        _dataService.Save(data);
    }

    [RelayCommand]
    private void AddList(CreateListRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return;

        var list = new RatingList
        {
            Name = request.Name.Trim(),
            Fields = request.Fields
        };

        Lists.Add(list);
        SelectedList = list;
        Save();
    }

    [RelayCommand]
    private void DeleteList(RatingList list)
    {
        Lists.Remove(list);

        if (list == SelectedList)
            SelectedList = null;

        Save();
    }

    [RelayCommand]
    private void AddItem(MediaItem item)
    {
        if (SelectedList == null)
            return;

        SelectedList.Items.Add(item);
        Save();
    }
    
    [RelayCommand]
    private void DeleteItem(MediaItem item)
    {
        if (SelectedList == null)
            return;

        SelectedList.Items.Remove(item);
        Save();
    }

    [RelayCommand]
    private void AddTag(CreateTagRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return;

        if (request.Item == null)
            return;
        
        var tag = new Tag
        {
            Name = request.Name.Trim()
        };
        
        request.Item.Tags.Add(tag);
        Save();
    }

    [RelayCommand]
    private void DeleteTag(DeleteTagRequest request)
    {
        if (request.Tag == null)
            return;
        
        if (request.Item == null)
            return;
        
        request.Item.Tags.Remove(request.Tag);
        Save();
    }
    
    // Sorting
    
    [ObservableProperty]
    private string sortField = "";

    [ObservableProperty]
    private bool sortAscending = true;
    
    public List<MediaItem> GetSortedItems()
    {
        if (SelectedList == null) return new List<MediaItem>();

        var items = SelectedList.Items.ToList();

        if (string.IsNullOrEmpty(SortField))
            return items;

        var sorted = items.OrderBy(item =>
        {
            if (!item.Values.TryGetValue(SortField, out var fieldValue))
                return (object)"";

            return fieldValue.Score.HasValue
                ? (object)fieldValue.Score.Value
                : fieldValue.Text ?? "";
        });

        return sortAscending
            ? sorted.ToList()
            : sorted.Reverse().ToList();
    }
    
    [RelayCommand]
    private void SortBy(string fieldName)
    {
        if (SortField == fieldName)
            SortAscending = !SortAscending;
        else
        {
            SortField = fieldName;
            SortAscending = true;
        }
    }
}