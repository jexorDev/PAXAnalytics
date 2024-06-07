using Npgsql;
using PAXAnalyticsAPI.DataLayer.DataTransferObjects;

namespace PAXAnalyticsAPI.DataLayer.SqlRepositories
{
    public class SlicesSqlRepository
    {
        public List<Slice> GetSlices(NpgsqlConnection conn)
        {
            const string Sql = @"
SELECT
 date
,pax_arriving
,planes_arriving
,pax_departing
,planes_departing
FROM slices
";
            List<Slice> slices = new List<Slice>();
            
            using (var cmd = new NpgsqlCommand(Sql, conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    slices.Add(new Slice
                    {
                        Date = reader.GetDateTime(0),
                        PassengersArriving = reader.GetInt16(1),
                        PlanesArriving = reader.GetInt16(2),
                        PassengersDeparting = reader.GetInt16(3),
                        PlanesDeparting = reader.GetInt16(4),
                    });
                }
                    
            }

            return slices;
        }

        public void InsertSlice(NpgsqlConnection conn, NpgsqlTransaction trans, Slice slice)
        {
            const string Sql = @"
INSERT INTO slices 
(
 date
,pax_arriving
,planes_arriving
,pax_departing
,planes_departing
)
VALUES
(
 @date
,@pax_arriving
,@planes_arriving
,@pax_departing
,@planes_departing
)
";
            using (var cmd = new NpgsqlCommand(Sql, conn, trans))
            {
                cmd.Parameters.AddWithValue("date", slice.Date);
                cmd.Parameters.AddWithValue("pax_arriving", slice.PassengersArriving);
                cmd.Parameters.AddWithValue("planes_arriving", slice.PlanesArriving);
                cmd.Parameters.AddWithValue("pax_departing", slice.PassengersDeparting);
                cmd.Parameters.AddWithValue("planes_departing", slice.PlanesDeparting);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
