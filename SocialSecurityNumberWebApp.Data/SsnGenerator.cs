using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using SocialSecurityNumberWebApp.Data.Data;
using SocialSecurityNumberWebApp.Data.Models;

namespace SocialSecurityNumberWebApp.Data
{
    public class SsnGenerator
    {
        User _user;
        string ssn = string.Empty;
        Random rand = new Random();
        private readonly IUserDbContext _dbContext;

        public SsnGenerator(User user, IUserDbContext dbContext)
        {
            _dbContext = dbContext;
            _user = user;
        }


        public void FirstPair()
        {
            if (_user.Male)
            {
                var dayOfBirth = _user.BirthDateTime.Day;
                int num = 11;

                for (int i = 1; i < dayOfBirth; i++)
                {
                    num++;
                }
                if (num <= 41)
                {
                    ssn = num.ToString();
                }
            }

            if (_user.Female)
            {
                var dayOfBirth = _user.BirthDateTime.Day;
                int num = 51;

                for (int i = 1; i < dayOfBirth; i++)
                {
                    num++;
                }
                if (num <= 81)
                {
                    ssn = num.ToString();
                }
            }

            SecondPair(ssn);
        }

        public void SecondPair(string ssn2)
        {
            if (_user.BirthDateTime.Year < 2000)
            {
                int month = 1;

                if (_user.BirthDateTime.Month == 6)
                {
                    ssn2 += 14.ToString();
                }
                else
                {
                    for (int i = 1; i < _user.BirthDateTime.Month - 1; i++)
                    {
                        month++;
                    }
                    if (month < 10)
                    {
                        ssn2 += 0.ToString();
                        ssn2 += month;
                    }
                }
            }

            if (_user.BirthDateTime.Year >= 2000)
            {
                int month = 21;

                if (_user.BirthDateTime.Month == 6)
                {
                    ssn2 += 34.ToString();
                }
                else
                {
                    for (int i = 1; i < _user.BirthDateTime.Month - 1; i++)
                    {
                        month++;
                    }
                    ssn2 += month;
                }
            }
            ssn = ssn2;

            ThirthPair(ssn);
        }

        public void ThirthPair(string ssn3)
        {
            if (_user.BirthDateTime.Year % 100 < 10)
            {
                ssn3 += 0.ToString();
                ssn3 += _user.BirthDateTime.Year % 100;
            }
            else
            {
                ssn3 += _user.BirthDateTime.Year % 100;
            }
            ssn = ssn3;

            FourthPair(ssn);
        }

        public void FourthPair(string ssn4)
        {
            int fourthPair = 1;

            if (_dbContext.Users.Count == 0)
            {
                ssn4 += 0;
                ssn4 += 0;
                ssn4 += 1;
            }

            if (_dbContext.Users.Count > 0)
            {
                fourthPair = Convert.ToInt32(_dbContext.Users[0].SSN.Substring(6, 3));
            }

            for (int i = 0; i < _dbContext.Users.Count; i++)
            {
                if ((_user.Male && _dbContext.Users[i].Male) && (_user.BirthDateTime == _dbContext.Users[i].BirthDateTime))
                {
                    fourthPair++;
                }

                if ((_user.Female && _dbContext.Users[i].Female) && (_user.BirthDateTime == _dbContext.Users[i].BirthDateTime))
                {
                    fourthPair++;
                }
            }
            if (fourthPair < 10 && _dbContext.Users.Count > 0)
            {
                ssn4 += 0;
                ssn4 += 0;
                ssn4 += fourthPair;
            }
            if (fourthPair > 10 && fourthPair < 100)
            {
                ssn4 += 0;
                ssn4 += fourthPair;
            }
            ssn = ssn4;

            // LastOne(ssn);
        }

        public string GetSSN()
        {
            FirstPair();

            ssn += rand.Next(0, 9);

            return ssn;

        }
    }
}