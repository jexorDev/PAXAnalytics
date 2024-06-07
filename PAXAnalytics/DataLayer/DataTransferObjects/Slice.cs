namespace PAXAnalyticsAPI.DataLayer.DataTransferObjects
{
    public class Slice
    {
        public DateTime Date { get; set; }
        public int PassengersArriving { get; set; }
        public int PlanesArriving { get; set; }
        public int PassengersDeparting { get; set; }
        public int PlanesDeparting { get; set; }
    }
}
