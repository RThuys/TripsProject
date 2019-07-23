using System;

using uwp_app.ViewModels;

using Windows.UI.Xaml.Controls;

namespace uwp_app.Views
{
    public sealed partial class ChildrenOverviewPage : Page
    {
        public ChildrenOverviewViewModel ViewModel { get; } = new ChildrenOverviewViewModel();

        public ChildrenOverviewPage()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
        }
    }
}
