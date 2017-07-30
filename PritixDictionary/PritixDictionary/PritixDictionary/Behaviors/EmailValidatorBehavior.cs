using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PritixDictionary.Behaviors
{
    public class EmailValidatorBehavior : Behavior<Entry>
    {

        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        //static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmailValidatorBehavior), false);

        //public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public static readonly BindableProperty IsValidProperty =
    BindableProperty.Create("IsValid", typeof(bool), typeof(EmailValidatorBehavior), false, BindingMode.TwoWay);

        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.Unfocused += HandleTextChanged;
        }

        void HandleTextChanged(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(((Entry)sender).Text))
                IsValid = (Regex.IsMatch(((Entry)sender).Text, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            ((Entry)sender).TextColor = IsValid ? (Color)App.Current.Resources["ThemeForegroundColor"] : (Color)App.Current.Resources["ErrorTextColor"];
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.Unfocused -= HandleTextChanged;
        }
    }
}
