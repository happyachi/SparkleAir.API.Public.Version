using SparkleAir.IDAL.IRepository.AirFlights;
using SparkleAir.Infa.EFModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.DAL.EFRepository.AirFlights
{
    public class SeatGroupEFRepository : ISeatGroupRepository
    {
        private AppDbContext db;

        public SeatGroupEFRepository(AppDbContext appDbContext)
        {
            db = appDbContext;
        }
        /// <summary>
        /// 1 加長空間
        /// 2 前區
        /// 3 標準
        /// 4 緊急出口
        /// </summary>
        public void Create777300ER()
        {
            SeatGroup seatGroup;
            for (int i = 45; i <= 72; i++)
            {
                for (char c = 'A'; c <= 'K'; c++)
                {
                    if (i == 45 || i == 60)
                    {
                        if (c == 'A' || c == 'B' || c == 'C' || c == 'H' || c == 'J' || c == 'K')
                        {
                            seatGroup = new SeatGroup
                            {
                                AirTypeId = 1,
                                SeatAreaId = 4,
                                SeatNum = i + c.ToString(),
                            };
                            db.SeatGroups.Add(seatGroup);
                            db.SaveChanges();
                        }
                    }
                    else if (i == 57)
                    {
                        if (c == 'A' || c == 'D' || c == 'C' || c == 'E' || c == 'G' || c == 'K' || c == 'H')
                        {
                            seatGroup = new SeatGroup
                            {
                                AirTypeId = 1,
                                SeatAreaId = 2,
                                SeatNum = i + c.ToString(),
                            };
                            db.SeatGroups.Add(seatGroup);
                            db.SaveChanges();
                        }

                    }
                    else if (i >= 46 && i <= 56)
                    {
                        if (c == 'A' || c == 'D' || c == 'C' || c == 'E' || c == 'G' || c == 'K' || c == 'H' || c == 'B' || c == 'J')
                        {
                            seatGroup = new SeatGroup
                            {
                                AirTypeId = 1,
                                SeatAreaId = 2,
                                SeatNum = i + c.ToString(),
                            };
                            db.SeatGroups.Add(seatGroup);
                        }
                    }
                    else if (i >= 61 && i <= 71)
                    {
                        if (c == 'A' || c == 'D' || c == 'C' || c == 'E' || c == 'G' || c == 'K' || c == 'H' || c == 'B' || c == 'J')
                        {
                            seatGroup = new SeatGroup
                            {
                                AirTypeId = 1,
                                SeatAreaId = 2,
                                SeatNum = i + c.ToString(),
                            };
                            db.SeatGroups.Add(seatGroup);
                        }
                    }
                    else if (i == 72)
                    {
                        if (c == 'A' || c == 'C' || c == 'H' || c == 'K')
                        {
                            seatGroup = new SeatGroup
                            {
                                AirTypeId = 1,
                                SeatAreaId = 2,
                                SeatNum = i + c.ToString(),
                            };
                            db.SeatGroups.Add(seatGroup);
                        }
                    }

                }
            }
            db.SaveChanges();
        }

        public void Create78710()
        {
            SeatGroup seatGroup;
            for (int i = 20; i <= 59; i++)
            {
                for (char c = 'A'; c <= 'k'; c++)
                {
                    if (i == 58)
                    {
                        if (c == 'D' || c == 'E' || c == 'G' || c == 'H' || c == 'J')
                        {
                            seatGroup = new SeatGroup
                            {
                                AirTypeId = 2,
                                SeatAreaId = 3,
                                SeatNum = i + c.ToString(),
                            };
                            db.SeatGroups.Add(seatGroup);
                        }
                    }
                    else if (i == 40 || i == 59)
                    {
                        if (c == 'D' || c == 'E' || c == 'G')
                        {
                            seatGroup = new SeatGroup
                            {
                                AirTypeId = 2,
                                SeatAreaId = 3,
                                SeatNum = i + c.ToString(),
                            };
                            db.SeatGroups.Add(seatGroup);
                        }

                    }
                    else if (i >= 20 && i <= 36)
                    {
                        if (c == 'A' || c == 'B' || c == 'C' || c == 'D' || c == 'E' || c == 'G' || c == 'H' || c == 'J' || c == 'K')
                        {
                            seatGroup = new SeatGroup
                            {
                                AirTypeId = 2,
                                SeatAreaId = 2,
                                SeatNum = i + c.ToString(),
                            };
                            db.SeatGroups.Add(seatGroup);
                        }
                    }
                    else if (i >= 41 && i <= 57 && i != 44)
                    {
                        if (i == 41)
                        {
                            if (c == 'A' || c == 'B' || c == 'C' || c == 'H' || c == 'J' || c == 'K')
                            {
                                seatGroup = new SeatGroup
                                {
                                    AirTypeId = 2,
                                    SeatAreaId = 4,
                                    SeatNum = i + c.ToString(),
                                };
                                db.SeatGroups.Add(seatGroup);
                            }
                            else if (c == 'D' || c == 'E' || c == 'G')
                            {
                                seatGroup = new SeatGroup
                                {
                                    AirTypeId = 2,
                                    SeatAreaId = 3,
                                    SeatNum = i + c.ToString(),
                                };
                                db.SeatGroups.Add(seatGroup);
                            }
                        }
                        else if (c == 'A' || c == 'B' || c == 'C' || c == 'D' || c == 'E' || c == 'G' || c == 'H' || c == 'J' || c == 'K')
                        {
                            seatGroup = new SeatGroup
                            {
                                AirTypeId = 2,
                                SeatAreaId = 3,
                                SeatNum = i + c.ToString(),
                            };
                            db.SeatGroups.Add(seatGroup);
                        }
                    }
                }
            }
            db.SaveChanges();
        }

        public void CreateA320neo()
        {
            SeatGroup seatGroup;
            for (int i = 4; i <= 33; i++)
            {
                for (char c = 'A'; c <= 'k'; c++)
                {
                    if (i == 4 || i == 15)
                    {
                        if (c == 'A' || c == 'B' || c == 'C' || c == 'H' || c == 'J' || c == 'K')
                        {
                            seatGroup = new SeatGroup
                            {
                                AirTypeId = 3,
                                SeatAreaId = 1,
                                SeatNum = i + c.ToString(),
                            };
                            db.SeatGroups.Add(seatGroup);
                        }
                    }
                    else if (i >= 5 && i <= 11)
                    {
                        if (c == 'A' || c == 'B' || c == 'C' || c == 'H' || c == 'J' || c == 'K')
                        {
                            seatGroup = new SeatGroup
                            {
                                AirTypeId = 3,
                                SeatAreaId = 2,
                                SeatNum = i + c.ToString(),
                            };
                            db.SeatGroups.Add(seatGroup);
                        }
                    }
                    else if (i == 14)
                    {
                        if (c == 'A' || c == 'B' || c == 'C' || c == 'H' || c == 'J' || c == 'K')
                        {
                            seatGroup = new SeatGroup
                            {
                                AirTypeId = 3,
                                SeatAreaId = 4,
                                SeatNum = i + c.ToString(),
                            };
                            db.SeatGroups.Add(seatGroup);
                        }
                    }
                    else if (c == 'A' || c == 'B' || c == 'C' || c == 'H' || c == 'J' || c == 'K')
                    {
                        seatGroup = new SeatGroup
                        {
                            AirTypeId = 3,
                            SeatAreaId = 3,
                            SeatNum = i + c.ToString(),
                        };
                        db.SeatGroups.Add(seatGroup);
                    }
                }
            }
            db.SaveChanges();
        }

        public void CreateA350900()
        {
            SeatGroup seatGroup;
            for (int i = 20; i <= 24; i++)
            {
                for (char c = 'A'; c <= 'k'; c++)
                {
                    if (i == 24)
                    {
                        if (c == 'A' || c == 'C' || c == 'H' || c == 'K')
                        {
                            seatGroup = new SeatGroup
                            {
                                AirTypeId = 4,
                                SeatAreaId = 3,
                                SeatNum = i + c.ToString(),
                            };
                            db.SeatGroups.Add(seatGroup);
                        }
                    }
                    else if (c == 'A' || c == 'C' || c == 'D' || c == 'E' || c == 'F' || c == 'G' || c == 'H' || c == 'K')
                    {
                        seatGroup = new SeatGroup
                        {
                            AirTypeId = 4,
                            SeatAreaId = 3,
                            SeatNum = i + c.ToString(),
                        };
                        db.SeatGroups.Add(seatGroup);
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
                            seatGroup = new SeatGroup
                            {
                                AirTypeId = 4,
                                SeatAreaId = 1,
                                SeatNum = i + c.ToString(),
                            };
                            db.SeatGroups.Add(seatGroup);
                        }
                    }
                    else if (i >= 31 && i <= 37)
                    {
                        if (i == 31)
                        {
                            if (c == 'A' || c == 'B' || c == 'C' || c == 'H' || c == 'J' || c == 'K')
                            {
                                seatGroup = new SeatGroup
                                {
                                    AirTypeId = 4,
                                    SeatAreaId = 1,
                                    SeatNum = i + c.ToString(),
                                };
                                db.SeatGroups.Add(seatGroup);
                            }
                            else if (c == 'D' || c == 'E' || c == 'G')
                            {

                                seatGroup = new SeatGroup
                                {
                                    AirTypeId = 4,
                                    SeatAreaId = 2,
                                    SeatNum = i + c.ToString(),
                                };
                                db.SeatGroups.Add(seatGroup);
                            }
                        }
                        else if (c == 'A' || c == 'B' || c == 'C' || c == 'D' || c == 'E' || c == 'G' || c == 'H' || c == 'J' || c == 'K')
                        {
                            seatGroup = new SeatGroup
                            {
                                AirTypeId = 4,
                                SeatAreaId = 2,
                                SeatNum = i + c.ToString(),
                            };
                            db.SeatGroups.Add(seatGroup);
                        }
                    }
                    else if (i >= 38 && i <= 41)
                    {
                        if (c == 'A' || c == 'B' || c == 'C' || c == 'D' || c == 'E' || c == 'G' || c == 'H' || c == 'J' || c == 'K')
                        {
                            seatGroup = new SeatGroup
                            {
                                AirTypeId = 4,
                                SeatAreaId = 3,
                                SeatNum = i + c.ToString(),
                            };
                            db.SeatGroups.Add(seatGroup);
                        }
                    }
                    else if (i >= 51 && i <= 59)
                    {
                        if (i == 51)
                        {
                            if (c == 'A' || c == 'B' || c == 'C' || c == 'H' || c == 'J' || c == 'K')
                            {
                                seatGroup = new SeatGroup
                                {
                                    AirTypeId = 4,
                                    SeatAreaId = 4,
                                    SeatNum = i + c.ToString(),
                                };
                                db.SeatGroups.Add(seatGroup);
                            }
                            else if (c == 'D' || c == 'E' || c == 'G')
                            {

                                seatGroup = new SeatGroup
                                {
                                    AirTypeId = 4,
                                    SeatAreaId = 3,
                                    SeatNum = i + c.ToString(),
                                };
                                db.SeatGroups.Add(seatGroup);
                            }
                        }
                        else if (i == 52 || i == 53 || i == 58 || i == 59)
                        {
                            if (c == 'A' || c == 'B' || c == 'C' || c == 'D' || c == 'E' || c == 'G' || c == 'H' || c == 'J' || c == 'K')
                            {
                                seatGroup = new SeatGroup
                                {
                                    AirTypeId = 4,
                                    SeatAreaId = 3,
                                    SeatNum = i + c.ToString(),
                                };
                                db.SeatGroups.Add(seatGroup);
                            }
                        }
                    }
                    else if (i >= 60 && i <= 69)
                    {
                        if (c == 'A' || c == 'B' || c == 'C' || c == 'D' || c == 'E' || c == 'G' || c == 'H' || c == 'J' || c == 'K')
                        {
                            seatGroup = new SeatGroup
                            {
                                AirTypeId = 4,
                                SeatAreaId = 3,
                                SeatNum = i + c.ToString(),
                            };
                            db.SeatGroups.Add(seatGroup);
                        }
                    }
                }
            }
            db.SaveChanges();

        }
    }
}
