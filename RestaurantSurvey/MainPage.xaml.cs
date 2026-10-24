using System.Text;

namespace RestaurantSurvey;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnRequiredFieldChanged(object? sender, TextChangedEventArgs e)
    {
        bool isNameFilled = !string.IsNullOrWhiteSpace(_nameEntry_.Text);
        bool isPhoneFilled = !string.IsNullOrWhiteSpace(_phoneEntry_.Text);

        _submitButton_.IsEnabled = isNameFilled && isPhoneFilled;
    }

    private void OnCelebrationToggled(object? sender, ToggledEventArgs e)
    {
        if (e.Value && string.IsNullOrWhiteSpace(_wishesEditor_.Text))
        {
            _wishesEditor_.Text = "Празднование события (подарок-комплимент от шефа). ";
        }
    }

    private void OnSubmitClicked(object? sender, EventArgs e)
    {
        string selectedZone = _mainHallRadio_.IsChecked ? "Основной зал"
                            : _terraceRadio_.IsChecked ? "Летняя терраса"
                            : _vipRadio_.IsChecked ? "VIP-комната"
                            : "Не выбрано";

        string selectedTime = _timePicker_.SelectedItem?.ToString() ?? "Время не выбрано";

        var summary = new StringBuilder();
        summary.AppendLine("ДЕТАЛИ БРОНИРОВАНИЯ СТОЛИКА:");
        summary.AppendLine($"Гость: {_nameEntry_.Text?.Trim()}");
        summary.AppendLine($"Телефон: {_phoneEntry_.Text?.Trim()}");
        summary.AppendLine($"Дата: {_bookingDatePicker_.Date:dd.MM.yyyy}");
        summary.AppendLine($"Время: {selectedTime}");
        summary.AppendLine($"Зона: {selectedZone}");
        summary.AppendLine($"Гостей: {(int)Math.Round(_guestsSlider_.Value)} чел.");
        summary.AppendLine($"Праздник: {(_celebrationSwitch_.IsToggled ? "Да" : "Нет")}");
        summary.AppendLine($"Детский стул: {(_babyChairCheckBox_.IsChecked ? "Нужен" : "Нет")}");
        summary.AppendLine($"Помощь сомелье: {(_sommelierCheckBox_.IsChecked ? "Да" : "Нет")}");

        string wishes = string.IsNullOrWhiteSpace(_wishesEditor_.Text) ? "нет" : _wishesEditor_.Text.Trim();
        summary.AppendLine($"• Пожелания: {wishes}");

        _resultLabel_.Text = summary.ToString();
        _resultFrame_.IsVisible = true;
    }

    private void OnResetClicked(object? sender, EventArgs e)
    {
        _nameEntry_.Text = string.Empty;
        _phoneEntry_.Text = string.Empty;

        _bookingDatePicker_.Date = new DateTime(2026, 10, 24);
        _timePicker_.SelectedIndex = -1;
        _mainHallRadio_.IsChecked = true;
        _guestsSlider_.Value = 2;
        _celebrationSwitch_.IsToggled = false;
        _babyChairCheckBox_.IsChecked = false;
        _sommelierCheckBox_.IsChecked = false;
        _wishesEditor_.Text = string.Empty;

        _resultFrame_.IsVisible = false;
        _resultLabel_.Text = string.Empty;
        _submitButton_.IsEnabled = false;
    }
}