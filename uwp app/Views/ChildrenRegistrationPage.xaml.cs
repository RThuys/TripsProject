using System;

using uwp_app.ViewModels;

using Windows.UI.Xaml.Controls;

namespace uwp_app.Views
{
    public sealed partial class ChildrenRegistrationPage : Page
    {
        public ChildrenRegistrationViewModel ViewModel { get; } = new ChildrenRegistrationViewModel();

        public ChildrenRegistrationPage()
        {
            InitializeComponent();
        }
    }
}
