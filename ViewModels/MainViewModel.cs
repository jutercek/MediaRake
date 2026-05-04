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
    private ObservableCollection<Category> categories = new();

    // App needs to track this, even if empty for now
    [ObservableProperty]
    private Category? selectedCategory;

    [ObservableProperty]
    private Subcategory? selectedSubcategory;

    [ObservableProperty]
    private RatingList? selectedList;
    

    private readonly DataService _dataService = new();
    // Constructor
    public MainViewModel()
    {
        var data = _dataService.Load();
        categories = new ObservableCollection<Category>(data.Categories);
    }
    
    // Add category
    [RelayCommand]
    private void AddCategory(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return;

        var category = new Category { Name = name.Trim() };
        Categories.Add(category);
        Save();
    }

    private void Save()
    {
        var data = new AppData { Categories = Categories.ToList() };
        _dataService.Save(data);
    }
    
    //Delete category
    [RelayCommand]
    private void DeleteCategory(Category category)
    {
        Categories.Remove(category);
        Save();
    }
    
    // Add subcategory
    [RelayCommand]
    private void AddSubcategory(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return;
        
        if (selectedCategory == null)
            return;

        var subcategory = new Subcategory { Name = name.Trim() };
        selectedCategory.Subcategories.Add(subcategory); //not correct
        Save();
    }
    
    // Remove subcategory
    [RelayCommand]
    private void DeleteSubcategory(Subcategory subcategory)
    {
        if (selectedCategory == null)
            return;

        selectedCategory.Subcategories.Remove(subcategory);
        Save();
    }
}