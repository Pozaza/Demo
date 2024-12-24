using LinqToDB;
using System.Windows;

namespace Demo;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();

		ConfigHelper.Instance.SetLang("ru");

		if (DB.db == null)
		{
			if (MessageBox.Show("Не найден файл настроек подключения!\nОткрыть настройки?", "Ошибка", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
			{
				Visibility = Visibility.Collapsed;
				var settings = new SettingsPage(true);
				settings.ShowDialog();
			}

			Application.Current?.Shutdown();
		}
	}

	private void Add()
	{
		// data = new Drivers()
		// {
		// 	FirstName = name.Text.Trim()
		// };

		// try
		// {
		// 	DB.db.Insert(data);
		// }
		// catch
		// {
		// 	MessageBox.Warning("Такой номер водительского удостоверения уже есть в БД!", "Внимание");
		// 	return;
		// }

		// DialogResult = true;
	}

	private void Edit()
	{
		// if (phone.Text.Trim().Length > 0 && !Helper.IsValidNumber(phone.Text.Trim()))
		// {
		// 	MessageBox.Warning("Неверный номер телефона", "Внимание");
		// 	return;
		// }

		// data = new Drivers()
		// {
		// 	DriverID = data.DriverID,
		// 	FirstName = name.Text.Trim()
		// };

		// try
		// {
		// 	DB.db.Update(data);
		// }
		// catch
		// {
		// 	MessageBox.Warning("Такой номер водительского удостоверения уже есть в БД!", "Внимание");
		// 	return;
		// }

		// DialogResult = true;
	}

	private void Refresh(bool filter_applied = false)
	{
		list.Columns.Clear();

		switch (tab_control.SelectedIndex)
		{
			case 0:
				Helper.AddColumn(list, "id", "ScheduleID", false);
				Helper.AddColumn(list, "routeid", "RouteID", false);
				Helper.AddColumn(list, "Маршрут", "RouteFormatted");
				Helper.AddColumn(list, "driverid", "DriverID", false);
				Helper.AddColumn(list, "Водитель", "DriverFormatted");
				Helper.AddColumn(list, "Время отправления из нач. пункта", "DepartureTimeFormatted");
				Helper.AddColumn(list, "Время отправления из конеч. пункта", "ArriveTimeFormatted");
				Helper.AddColumn(list, "Рабочие дни", "DaysOperational");

				var listS = from s in DB.db?.GetTable<Schedules>().LoadWith(s => s.Route).LoadWith(s => s.Driver) select s;

				list.ItemsSource = listS;
				break;
		}

		edit_btn.IsEnabled = list.SelectedItem != null;
		delete_btn.IsEnabled = list.SelectedItem != null;
	}
}