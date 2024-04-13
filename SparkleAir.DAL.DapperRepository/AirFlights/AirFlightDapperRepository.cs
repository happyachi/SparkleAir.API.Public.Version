using Dapper;
using Microsoft.Data.SqlClient;
using SparkleAir.IDAL.IRepository.AirFlights;
using SparkleAir.Infa.Criteria.AirFlights;
using SparkleAir.Infa.Entity.AirFlightsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.DAL.DapperRepository.AirFlights
{
    public class AirFlightDapperRepository : IAirFlightRepository
    {
        private string _connectionString;
        public AirFlightDapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Task<(int, string)> Create(AirFlightEntity entity)
        {
            throw new NotImplementedException();
        }

        public List<AirFlightEntity> GetAll()
        {
            string connstr = _connectionString;
            string sql = @"SELECT af.Id,
                af.AirOwnId,
                af.AirFlightManagementId,
                af.ScheduledDeparture,
                af.ScheduledArrival,
                af.AirFlightSaleStatusId,
                aoat.FlightModel,
                afm.FlightCode,
                dep.Lata AS DepartureAirport,
                arr.Lata AS ArrivalAirport,
                dep.Id AS DepartureAirportId,
                arr.Id AS ArrivalAirportId,
                dep.TimeArea AS DepartureTimeZone,
                arr.TimeArea AS ArrivalTimeZone,
                afs.Status AS AirFlightSaleStatus,
                afm.DayofWeek,
                ao.RegistrationNum,
                afm.CrossDay
             FROM AirFlights af
            JOIN AirOwns ao ON af.AirOwnId = ao.Id
            JOIN AirTypes aoat ON ao.AirTypeId = aoat.Id
            JOIN AirFlightManagements afm ON af.AirFlightManagementId = afm.Id
            JOIN AirPorts dep ON afm.DepartureAirportId = dep.Id
            JOIN AirPorts arr ON afm.ArrivalAirportId = arr.Id
            JOIN AirFlightSaleStatuses afs ON af.AirFlightSaleStatusId = afs.Id";
            using (var conn = new SqlConnection(connstr))
            {
                return conn.Query<AirFlightEntity>(sql).ToList();
            }
        }

        public AirFlightEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateSaleStatusAsync(AirFlightEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AirFlightEntity>> GetAllAsync()
        {
            string connstr = _connectionString;
            string sql = @"SELECT af.Id,
                af.AirOwnId,
                af.AirFlightManagementId,
                af.ScheduledDeparture,
                af.ScheduledArrival,
                af.AirFlightSaleStatusId,
                aoat.FlightModel,
                afm.FlightCode,
                dep.Lata AS DepartureAirport,
                arr.Lata AS ArrivalAirport,
                dep.Id AS DepartureAirportId,
                arr.Id AS ArrivalAirportId,
                dep.TimeArea AS DepartureTimeZone,
                arr.TimeArea AS ArrivalTimeZone,
                afs.Status AS AirFlightSaleStatus,
                afm.DayofWeek,
                ao.RegistrationNum
             FROM AirFlights af
            JOIN AirOwns ao ON af.AirOwnId = ao.Id
            JOIN AirTypes aoat ON ao.AirTypeId = aoat.Id
            JOIN AirFlightManagements afm ON af.AirFlightManagementId = afm.Id
            JOIN AirPorts dep ON afm.DepartureAirportId = dep.Id
            JOIN AirPorts arr ON afm.ArrivalAirportId = arr.Id
            JOIN AirFlightSaleStatuses afs ON af.AirFlightSaleStatusId = afs.Id";
            using (var conn = new SqlConnection(connstr))
            {
                return (await conn.QueryAsync<AirFlightEntity>(sql)).ToList();
            }
        }

        public List<AirFlightEntity> Search(AirFlightCriteria criteria)
        {
            throw new NotImplementedException();
        }
    }
}
