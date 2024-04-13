using Microsoft.EntityFrameworkCore;
using SparkleAir.IDAL.IRepository.AirFlights;
using SparkleAir.Infa.EFModel.Models;
using SparkleAir.Infa.Entity.AirFlightsEntity;
using SparkleAir.Infa.Utility.Exts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.DAL.EFRepository.AirFlights
{
    public class AirFlightSeatsEFRepository : IAirFlightSeatsRepository
    {
        private AppDbContext db;

        public AirFlightSeatsEFRepository(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        public async Task Create777300ER(int flightId)
        {
            await Task.Run(() =>
            {
                AirFlightSeat seats;
                //商務
                for (int i = 1; i <= 11; i++)
                {
                    for (char c = 'A'; c <= 'k'; c++)
                    {
                        if (i == 7)
                        {
                            if (c == 'D' || c == 'G')
                            {
                                seats = new AirFlightSeat
                                {
                                    AirFlightId = flightId,
                                    AirCabinId = 2,
                                    SeatNum = i + c.ToString(),
                                    IsSeated = false
                                };
                                db.AirFlightSeats.Add(seats);
                            }
                        }
                        else if (i != 4)
                        {
                            if (c == 'A' || c == 'D' || c == 'G' || c == 'K')
                            {
                                seats = new AirFlightSeat
                                {
                                    AirFlightId = flightId,
                                    AirCabinId = 2,
                                    SeatNum = i + c.ToString(),
                                    IsSeated = false
                                };
                                db.AirFlightSeats.Add(seats);
                            }
                        }
                    }
                }

                //豪華
                for (int i = 20; i <= 27; i++)
                {
                    for (char c = 'A'; c <= 'k'; c++)
                    {
                        if (c == 'A' || c == 'C' || c == 'D' || c == 'E' || c == 'F' || c == 'G' || c == 'H' || c == 'K')
                        {
                            seats = new AirFlightSeat
                            {
                                AirFlightId = flightId,
                                AirCabinId = 3,
                                SeatNum = i + c.ToString(),
                                IsSeated = false
                            };

                            db.AirFlightSeats.Add(seats);
                        }

                    }
                }

                //經濟
                for (int i = 45; i <= 72; i++)
                {
                    for (char c = 'A'; c <= 'k'; c++)
                    {
                        if (i == 45 || i == 60)
                        {
                            if (c == 'A' || c == 'B' || c == 'C' || c == 'H' || c == 'J' || c == 'K')
                            {
                                seats = new AirFlightSeat
                                {
                                    AirFlightId = flightId,
                                    AirCabinId = 4,
                                    SeatNum = i + c.ToString(),
                                    IsSeated = false
                                };
                                db.AirFlightSeats.Add(seats);
                            }
                        }
                        else if (i == 57)
                        {
                            if (c == 'A' || c == 'D' || c == 'C' || c == 'E' || c == 'G' || c == 'K' || c == 'H')
                            {
                                seats = new AirFlightSeat
                                {
                                    AirFlightId = flightId,
                                    AirCabinId = 4,
                                    SeatNum = i + c.ToString(),
                                    IsSeated = false
                                };
                                db.AirFlightSeats.Add(seats);
                            }

                        }
                        else if (i >= 46 && i <= 56)
                        {
                            if (c == 'A' || c == 'D' || c == 'C' || c == 'E' || c == 'G' || c == 'K' || c == 'H' || c == 'B' || c == 'J')
                            {
                                seats = new AirFlightSeat
                                {
                                    AirFlightId = flightId,
                                    AirCabinId = 4,
                                    SeatNum = i + c.ToString(),
                                    IsSeated = false
                                };
                                db.AirFlightSeats.Add(seats);
                            }
                        }
                        else if (i >= 61 && i <= 71)
                        {
                            if (c == 'A' || c == 'D' || c == 'C' || c == 'E' || c == 'G' || c == 'K' || c == 'H' || c == 'B' || c == 'J')
                            {
                                seats = new AirFlightSeat
                                {
                                    AirFlightId = flightId,
                                    AirCabinId = 4,
                                    SeatNum = i + c.ToString(),
                                    IsSeated = false
                                };
                                db.AirFlightSeats.Add(seats);
                            }
                        }
                        else if (i == 72)
                        {
                            if (c == 'A' || c == 'C' || c == 'H' || c == 'K')
                            {
                                seats = new AirFlightSeat
                                {
                                    AirFlightId = flightId,
                                    AirCabinId = 4,
                                    SeatNum = i + c.ToString(),
                                    IsSeated = false
                                };
                                db.AirFlightSeats.Add(seats);
                            }
                        }
                    }
                }
                db.SaveChanges();
            });

        }

        public async Task Create78710(int flightId)
        {
            await Task.Run(() =>
            {
                AirFlightSeat seats;
                //商務
                for (int i = 1; i <= 10; i++)
                {
                    for (char c = 'A'; c <= 'k'; c++)
                    {
                        if (i == 1)
                        {
                            if (c == 'A' || c == 'K')
                            {
                                seats = new AirFlightSeat
                                {
                                    AirFlightId = flightId,
                                    AirCabinId = 2,
                                    SeatNum = i + c.ToString(),
                                    IsSeated = false
                                };
                                db.AirFlightSeats.Add(seats);
                            }
                        }
                        else if (i != 4)
                        {
                            if (c == 'A' || c == 'D' || c == 'G' || c == 'K')
                            {
                                seats = new AirFlightSeat
                                {
                                    AirFlightId = flightId,
                                    AirCabinId = 2,
                                    SeatNum = i + c.ToString(),
                                    IsSeated = false
                                };
                                db.AirFlightSeats.Add(seats);
                            }
                        }
                    }
                }

                //經濟
                for (int i = 20; i <= 59; i++)
                {
                    for (char c = 'A'; c <= 'k'; c++)
                    {
                        if (i == 58)
                        {
                            if (c == 'D' || c == 'E' || c == 'G' || c == 'H' || c == 'J')
                            {
                                seats = new AirFlightSeat
                                {
                                    AirFlightId = flightId,
                                    AirCabinId = 4,
                                    SeatNum = i + c.ToString(),
                                    IsSeated = false
                                };
                                db.AirFlightSeats.Add(seats);
                            }
                        }
                        else if (i == 40 || i == 59)
                        {
                            if (c == 'D' || c == 'E' || c == 'G')
                            {
                                seats = new AirFlightSeat
                                {
                                    AirFlightId = flightId,
                                    AirCabinId = 4,
                                    SeatNum = i + c.ToString(),
                                    IsSeated = false
                                };
                                db.AirFlightSeats.Add(seats);
                            }

                        }
                        else if (i >= 20 && i <= 36)
                        {
                            if (c == 'A' || c == 'D' || c == 'C' || c == 'E' || c == 'G' || c == 'K' || c == 'H' || c == 'B' || c == 'J')
                            {
                                seats = new AirFlightSeat
                                {
                                    AirFlightId = flightId,
                                    AirCabinId = 4,
                                    SeatNum = i + c.ToString(),
                                    IsSeated = false
                                };
                                db.AirFlightSeats.Add(seats);
                            }
                        }
                        else if (i >= 41 && i <= 57 && i != 44)
                        {
                            if (c == 'A' || c == 'D' || c == 'C' || c == 'E' || c == 'G' || c == 'K' || c == 'H' || c == 'B' || c == 'J')
                            {
                                seats = new AirFlightSeat
                                {
                                    AirFlightId = flightId,
                                    AirCabinId = 4,
                                    SeatNum = i + c.ToString(),
                                    IsSeated = false
                                };
                                db.AirFlightSeats.Add(seats);
                            }
                        }
                    }
                }
                db.SaveChanges();
            });
        }

        public async Task CreateA320neo(int flightId)
        {
            await Task.Run(() =>
            {
                AirFlightSeat seats;
                //商務
                for (int i = 2; i <= 3; i++)
                {
                    for (char c = 'A'; c <= 'k'; c++)
                    {
                        if (c == 'A' || c == 'C' || c == 'H' || c == 'K')
                        {
                            seats = new AirFlightSeat
                            {
                                AirFlightId = flightId,
                                AirCabinId = 2,
                                SeatNum = i + c.ToString(),
                                IsSeated = false
                            };
                            db.AirFlightSeats.Add(seats);
                        }
                    }
                }

                //經濟
                for (int i = 4; i <= 33; i++)
                {
                    for (char c = 'A'; c <= 'k'; c++)
                    {
                        if (c == 'A' || c == 'B' || c == 'C' || c == 'H' || c == 'J' || c == 'K')
                        {
                            seats = new AirFlightSeat
                            {
                                AirFlightId = flightId,
                                AirCabinId = 4,
                                SeatNum = i + c.ToString(),
                                IsSeated = false
                            };
                            db.AirFlightSeats.Add(seats);
                        }
                    }
                }
                db.SaveChanges();
            });
        }

        public async Task CreateA350900(int flightId)
        {
            await Task.Run(() =>
            {
                AirFlightSeat seats;
                //頭等
                for (int i = 1; i < 2; i++)
                {
                    for (char c = 'A'; c <= 'k'; c++)
                    {
                        if (c == 'A' || c == 'D' || c == 'G' || c == 'K')
                        {
                            seats = new AirFlightSeat
                            {
                                AirFlightId = flightId,
                                AirCabinId = 1,
                                SeatNum = i + c.ToString(),
                                IsSeated = false
                            };
                            db.AirFlightSeats.Add(seats);
                        }
                    }
                }

                //商務
                for (int i = 2; i <= 8; i++)
                {
                    for (char c = 'A'; c <= 'k'; c++)
                    {
                        if (i == 8)
                        {
                            if (c == 'D' || c == 'G')
                            {
                                seats = new AirFlightSeat
                                {
                                    AirFlightId = flightId,
                                    AirCabinId = 2,
                                    SeatNum = i + c.ToString(),
                                    IsSeated = false
                                };
                                db.AirFlightSeats.Add(seats);
                            }
                        }
                        else if (c == 'A' || c == 'D' || c == 'G' || c == 'K')
                        {
                            seats = new AirFlightSeat
                            {
                                AirFlightId = flightId,
                                AirCabinId = 2,
                                SeatNum = i + c.ToString(),
                                IsSeated = false
                            };
                            db.AirFlightSeats.Add(seats);
                        }
                    }
                }

                //豪華
                for (int i = 20; i <= 24; i++)
                {
                    for (char c = 'A'; c <= 'k'; c++)
                    {
                        if (i == 24)
                        {
                            if (c == 'A' || c == 'C' || c == 'H' || c == 'K')
                            {
                                seats = new AirFlightSeat
                                {
                                    AirFlightId = flightId,
                                    AirCabinId = 3,
                                    SeatNum = i + c.ToString(),
                                    IsSeated = false
                                };

                                db.AirFlightSeats.Add(seats);
                            }
                        }
                        else if (c == 'A' || c == 'C' || c == 'D' || c == 'E' || c == 'F' || c == 'G' || c == 'H' || c == 'K')
                        {
                            seats = new AirFlightSeat
                            {
                                AirFlightId = flightId,
                                AirCabinId = 3,
                                SeatNum = i + c.ToString(),
                                IsSeated = false
                            };

                            db.AirFlightSeats.Add(seats);
                        }

                    }
                }

                //經濟
                for (int i = 30; i <= 69; i++)
                {
                    for (char c = 'A'; c <= 'k'; c++)
                    {
                        if (i == 30 || i == 50)
                        {
                            if (c == 'D' || c == 'E' || c == 'G')
                            {
                                seats = new AirFlightSeat
                                {
                                    AirFlightId = flightId,
                                    AirCabinId = 4,
                                    SeatNum = i + c.ToString(),
                                    IsSeated = false
                                };
                                db.AirFlightSeats.Add(seats);
                            }
                        }
                        else if (i >= 31 && i <= 41)
                        {
                            if (c == 'A' || c == 'B' || c == 'C' || c == 'D' || c == 'E' || c == 'G' || c == 'H' || c == 'J' || c == 'K')
                            {
                                seats = new AirFlightSeat
                                {
                                    AirFlightId = flightId,
                                    AirCabinId = 4,
                                    SeatNum = i + c.ToString(),
                                    IsSeated = false
                                };
                                db.AirFlightSeats.Add(seats);
                            }
                        }
                        else if (i >= 51 && i <= 59)
                        {
                            if (i == 51 || i == 52 || i == 53 || i == 58 || i == 59)
                            {
                                if (c == 'A' || c == 'B' || c == 'C' || c == 'D' || c == 'E' || c == 'G' || c == 'H' || c == 'J' || c == 'K')
                                {
                                    seats = new AirFlightSeat
                                    {
                                        AirFlightId = flightId,
                                        AirCabinId = 4,
                                        SeatNum = i + c.ToString(),
                                        IsSeated = false
                                    };
                                    db.AirFlightSeats.Add(seats);
                                }
                            }

                        }
                        else if (i >= 60 && i <= 69)
                        {
                            if (c == 'A' || c == 'B' || c == 'C' || c == 'D' || c == 'E' || c == 'G' || c == 'H' || c == 'J' || c == 'K')
                            {
                                seats = new AirFlightSeat
                                {
                                    AirFlightId = flightId,
                                    AirCabinId = 4,
                                    SeatNum = i + c.ToString(),
                                    IsSeated = false
                                };
                                db.AirFlightSeats.Add(seats);
                            }
                        }
                    }
                }
                db.SaveChanges();
            });
        }

        public List<AirFlightSeatsEntity> GetByFlightId(int flightId)
        {
            Func<AirFlightSeat, AirFlightSeatsEntity> seatFunc = x => x.ToFlightSeatsEntity(flightId);
            var flightSeats = db.AirFlightSeats
                    .AsNoTracking()
                    .Where(x => x.AirFlightId == flightId)
                    .Include(x => x.AirCabin)
                    .Include(x => x.AirFlight)
                    .Include(x => x.AirFlight.AirOwn)
                    .Include(x => x.AirFlight.AirOwn.AirType)
                    .ToList()
                    .Select(seatFunc)
                    .ToList();

            return flightSeats;
        }

        public EachSeatInfoEntity GetEachSeatInfo(int seatId)
        {
            var bookSeat = db.AirBookSeats.AsNoTracking().Where(x => x.AirFlightSeatId == seatId).FirstOrDefault();
            Func<AirBookSeat, EachSeatInfoEntity> func = x => x.ToEachSeatInfoEntity(seatId);

            var entity = bookSeat.ToEachSeatInfoEntity(seatId);
            return entity;
        }
    }
}
