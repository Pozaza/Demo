using LinqToDB.Mapping;

namespace Demo;

[Table("routes")]
public class Routes
{
	[PrimaryKey, Identity]
	public int RouteID { get; set; }

	[Column, NotNull]
	public string RouteName { get; set; }

	[Column, NotNull]
	public string StartPoint { get; set; }

	[Column, NotNull]
	public string EndPoint { get; set; }

	[Column, Nullable]
	public decimal? RouteLength { get; set; }
	public string RouteLengthFormatted { get => RouteLength.HasValue ? RouteLength.Value.ToString().Replace(',', '.') + " км." : ""; }
}

[Table("vehicles")]
public class Vehicles
{
	[PrimaryKey, Identity]
	public int VehicleID { get; set; }

	[Column, NotNull]
	public int TypeID { get; set; }

	[Association(ThisKey = nameof(TypeID), OtherKey = nameof(VehicleTypes.TypeID))]
	public VehicleTypes VehicleType { get; set; }
	public string VehicleTypeFormatted { get => VehicleType.TypeName; }

	[Column, NotNull]
	public int ScheduleID { get; set; }

	[Association(ThisKey = nameof(ScheduleID), OtherKey = nameof(Schedules.ScheduleID))]
	public Schedules Schedule { get; set; }
	public string ScheduleFormatted { get => Schedule.DepartureTimeFormatted + " - " + Schedule.ArriveTimeFormatted; }
}