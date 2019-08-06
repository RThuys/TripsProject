using mobile.Interfaces;
using mobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace mobile.ViewModels
{
    public class ChildrenPageModelView: ViewModelBase
    {
        private readonly IChildDataService _childDataService;
        private ObservableCollection<Child> _children;

        public ChildrenPageModelView(INavigationService navigationService, IChildDataService childDataService
            ) : base(navigationService)
        {
            _childDataService = childDataService;
            Children = new ObservableCollection<Child>();
            GetChildren();
        }

        public ObservableCollection<Child> Children
        {
            get => _children;
            set
            {
                _children = value;
                OnPropertyChanged();
            }
        }

        private async void GetChildren()
        {
            Children = (await _childDataService.GetAllChildren()).ToObservableCollection();
            Console.WriteLine();
        }

    }
}
