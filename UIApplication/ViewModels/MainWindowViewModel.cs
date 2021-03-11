using Core.Commands;
using Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace UIApplication.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {

        #region Private variables
        /// <summary>
        /// The child window to hold
        /// </summary>
        private Window _window;

        private double _outerMarginSize = 6;

        private double _cornerRadius = 10;

        private double _resizeBorderThickness = 6;
        #endregion

        #region Public properties

        #region Minimum Window Size
        public int MinHeight => 450;

        public int MinWidth => 800; 
        #endregion


        #region Window Tweaking Properties
        /// <summary>
        /// The size of the outer margin - from the main content grid to the top most border
        /// </summary>
        public Thickness OuterMarginSize => new Thickness(_outerMarginSize);

        /// <summary>
        /// The window coner radius
        /// </summary>
        public CornerRadius CornerRadius => _window.WindowState == WindowState.Maximized ? new CornerRadius(0) : new CornerRadius(_cornerRadius);

        /// <summary>
        /// The resize thickness of the border
        /// </summary>
        public Thickness ResizeBorderThickness => _window.WindowState == WindowState.Maximized ? new Thickness(0) : new Thickness(_resizeBorderThickness);

        /// <summary>
        /// The height of the Caption/Title bar 
        /// </summary>
        public double CaptionHeight { get; set; } = 35;
        #endregion

        #region Command Buttons Commands
        public ICommand Minimize { get; private set; }

        public ICommand Maximize { get; private set; }

        public ICommand Close { get; private set; }
        #endregion

        #endregion

        #region Default Constructor
        public MainWindowViewModel(Window window)
        {
            _window = window;

            //fire of all events of properties affected on window state change
            _window.StateChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(CornerRadius));
                OnPropertyChanged(nameof(ResizeBorderThickness));
            };

            //fire of commands
            Minimize = new RelayCommands(() => _window.WindowState = WindowState.Minimized);
            Maximize = new RelayCommands(() => _window.WindowState ^= WindowState.Maximized);
            Close = new RelayCommands(() => _window.Close());
        } 
        #endregion
    }
}
