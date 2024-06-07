using Microsoft.AspNetCore.Mvc;
using Npgsql;
using PAXAnalyticsAPI.DataLayer.DataTransferObjects;
using PAXAnalyticsAPI.DataLayer.SqlRepositories;
using PAXAnalyticsAPI.Models;
using PAXAnalyticsAPI.Utility;

namespace PAXAnalyticsAPI.Controllers
{
    [ApiController]
    [Route("days")]
    public class SliceController : ControllerBase
    {
        private readonly ILogger<SliceController> _logger;
        private readonly IConfiguration _configuration;
        private readonly SlicesSqlRepository _daysSqlRepository;

        public SliceController(ILogger<SliceController> logger, IConfiguration config)
        {
            _logger = logger;
            _configuration = config;
            _daysSqlRepository = new SlicesSqlRepository();
        }

        [HttpGet]
        public async Task<List<DayModel>> Get()
        {
            List<Slice> slices = new List<Slice>();
            List<DayModel> days = new List<DayModel>();

            slices.Add(new Slice
            {
                Date = new DateTime(2024, 7, 7, 0, 0, 0),
                PassengersArriving = 69
            });

            using (var conn = new NpgsqlConnection(DatabaseConnectionStringBuilder.GetSqlConnectionString(_configuration)))
            {
                conn.Open();

                //slices = _daysSqlRepository.GetSlices(conn);

                conn.Close();
            }

            days.Add(DaySliceConverter.SlicesToDay(slices));

            return  days;
        }

        [HttpPost]
        public async Task Post(DayModel day)
        {
            try
            {
                var slices = DaySliceConverter.DayToSlices(day);

                using (var conn = new NpgsqlConnection(DatabaseConnectionStringBuilder.GetSqlConnectionString(_configuration)))
                {
                    conn.Open();
                    NpgsqlTransaction transaction = conn.BeginTransaction();

                    foreach (var slice in slices)
                    {
                        _daysSqlRepository.InsertSlice(conn, transaction, slice);
                    }

                    transaction.Commit();

                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}