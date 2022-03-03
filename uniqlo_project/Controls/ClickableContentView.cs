using System;
using System.Windows.Input;
using NControl.Abstractions;
using Xamarin.Forms;

namespace uniqlo_project.Controls
{
    public class ClickableContentView : NControlView
    {
        private State _currentState = State.Default;

        private uint _animationMiliseconds = 100;

        protected State CurrentState
        {
            get
            {
                return _currentState;
            }
            set
            {
                if (_currentState != value)
                {
                    _currentState = value;
                    UpdateState();
                }
            }
        }

        public ClickableContentView()
        {
            IsAnimated = true;
        }

        #region -- Public properties --

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ClickableContentView), default(ICommand));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty =
                    BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ClickableContentView), default(object));

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly BindableProperty SelectedScaleProperty =

                    BindableProperty.Create(nameof(SelectedScale), typeof(double), typeof(ClickableContentView), Constants.MIN_SCALE_COEF);

        public double SelectedScale
        {
            get { return (double)GetValue(SelectedScaleProperty); }
            set { SetValue(SelectedScaleProperty, value); }
        }

        public static readonly BindableProperty IsAnimatedProperty =
            BindableProperty.Create(nameof(IsAnimated), typeof(bool), typeof(ClickableContentView), default(bool));

        public bool IsAnimated
        {
            get { return (bool)GetValue(IsAnimatedProperty); }
            set { SetValue(IsAnimatedProperty, value); }
        }

        protected virtual void UpdateState()
        {
            if (Constants.MIN_SCALE_COEF > Constants.DELTA_SCALE_COEF && IsAnimated)
            {
                if (CurrentState == State.Selected)
                {
                    this.ScaleTo(SelectedScale, _animationMiliseconds);
                }
                else
                {
                    if (Math.Abs(Scale - Constants.NORMAL_SCALE_COEF) > Constants.DELTA_SCALE_COEF)
                    {
                        this.ScaleTo(Constants.NORMAL_SCALE_COEF);
                    }
                }
            }
        }

        #endregion

        #region -- Overrides --

        public override bool TouchesBegan(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
        {
            base.TouchesBegan(points);

            if (CurrentState != State.Disabled)
            {
                CurrentState = State.Selected;
            }

            return true;
        }

        public override bool TouchesCancelled(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
        {
            base.TouchesCancelled(points);

            if (CurrentState != State.Disabled)
            {
                CurrentState = State.Default;
            }

            return true;
        }

        public override bool TouchesEnded(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
        {
            base.TouchesEnded(points);
            if (CurrentState != State.Disabled)
            {
                var isTouchEndedInside = false;
                foreach (var item in points)
                {
                    if ((this.Bounds.Width >= item.X && item.X >= 0) &&
                        (this.Bounds.Height >= item.Y && item.Y >= 0))
                    {
                        isTouchEndedInside = true;
                    }
                }

                if (isTouchEndedInside)
                {
                    OnClicked();
                }

                CurrentState = State.Default;
            }

            return true;
        }

        #endregion

        #region -- Private helpers --

        private void OnClicked()
        {
            if (Command != null && IsEnabled && Command.CanExecute(CommandParameter))
            {
                Command?.Execute(CommandParameter);
            }
        }

        #endregion

        protected enum State
        {
            Default,
            Selected,
            Disabled
        }
    }
}
