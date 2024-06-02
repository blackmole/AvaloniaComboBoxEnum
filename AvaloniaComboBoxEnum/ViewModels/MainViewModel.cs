using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AvaloniaComboBoxEnum.ViewModels;

public enum MyEnum
{
    A,
    B,
}

public class MyModel : ReactiveUI.ReactiveObject
{
    private MyEnum _enum;
    public MyEnum Enum { get => _enum; set => this.RaiseAndSetIfChanged(ref _enum, value); }
    public ObservableCollection<MyModel> Children { get; set; }
}

public class MainViewModel : ViewModelBase
{
    private ObservableCollection<MyModel> list { get; set; }
    public HierarchicalTreeDataGridSource<MyModel> GridSource { get; }

    public MainViewModel()
    {
        list = new ObservableCollection<MyModel>()
        {
            new MyModel { Enum = MyEnum.A },
        };
        GridSource = new HierarchicalTreeDataGridSource<MyModel>(list)
        {
            Columns =
            {
                new HierarchicalExpanderColumn<MyModel>(
                    new TextColumn<MyModel, MyEnum>("Enum", x => x.Enum),
                    x => x.Children),
            }
        };
    }

    public void ToggleEnum()
    {
        foreach (var item in list)
        {
            item.Enum = item.Enum == MyEnum.A ? MyEnum.B : MyEnum.A;
        }
    }
}
