using PAXAnalyticsAPI.DataLayer.DataTransferObjects;
using PAXAnalyticsAPI.Models;

namespace PAXAnalyticsAPI.Utility
{
    public class DaySliceConverter
    {
        public static DayModel SlicesToDay(List<Slice> slices)
        {
            DayModel day = new DayModel();

            foreach (var slice in slices)
            {
                string hourString = slice.Date.Hour.ToString().PadLeft(2, '0');
                
                string paxArrivingPropertyName = $"PaxArriving{hourString}";
                string planesArrivingPropertyName = $"PlanesArriving{hourString}";
                string paxDepartingPropertyName = $"PaxDeparting{hourString}";
                string planesDepartingPropertyName = $"PlanesDeparting{hourString}";

                SetPropertyValue(day, "Day", DateOnly.FromDateTime(slice.Date));
                SetPropertyValue(day, paxArrivingPropertyName, slice.PassengersArriving);
                SetPropertyValue(day, planesArrivingPropertyName, slice.PassengersArriving);
                SetPropertyValue(day, paxDepartingPropertyName, slice.PassengersArriving);
                SetPropertyValue(day, planesDepartingPropertyName, slice.PassengersArriving);
            }

            return day;
        }

        public static List<Slice> DayToSlices(DayModel day)
        {
            List<Slice> slices = new List<Slice>();

            for (var hour = 0; hour < 24; hour++)
            {
                Slice slice = new Slice();

                string hourString = hour.ToString().PadLeft(2, '0');

                string paxArrivingPropertyName = $"PaxArriving{hourString}";
                string planesArrivingPropertyName = $"PlanesArriving{hourString}";
                string paxDepartingPropertyName = $"PaxDeparting{hourString}";
                string planesDepartingPropertyName = $"PlanesDeparting{hourString}";

                int paxArrivingValue = 0;

                object? paxArrivingValueObject = GetPropertyValue(day, paxArrivingPropertyName);
                if (paxArrivingValueObject != null)
                {
                    int.TryParse(paxArrivingValueObject.ToString(), out paxArrivingValue);
                }

                slices.Add(slice);
            }

            return slices;
        }

        private static void SetPropertyValue(DayModel dayObject, string propertyName, object value)
        {
            var propertyInfo = dayObject.GetType().GetProperty(propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            propertyInfo?.SetValue(dayObject, value, null);
        }

        private static object? GetPropertyValue(DayModel dayObject, string propertyName)
        {
            var propertyInfo = dayObject.GetType().GetProperty(propertyName, System.Reflection.BindingFlags.Public);
            return propertyInfo?.GetValue(dayObject);
        }
    }
}
