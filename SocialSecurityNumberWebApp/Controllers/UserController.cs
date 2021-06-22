using System;
using System.Linq;
using AppServices.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using SocialSecurityNumberWebApp.Data;
using SocialSecurityNumberWebApp.Data.Data;
using SocialSecurityNumberWebApp.Data.Models;

namespace SocialSecurityNumberWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserDbContext _dbContext;
        private readonly IUserService _userService;

        public UserController(IUserDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        // GET: UserController
        public ActionResult Index()
        {
            return View(_userService.GetAll());
        }

        // GET: UserController/Details/5
        public ActionResult Details(Guid id)
        {
            return View(_userService.Get(id));
        }

        [HttpGet]
        public ActionResult DisplayData()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DisplayData(string ssn)
        {
            for (int i = 0; i < _dbContext.Users.Count; i++)
            {
                if (ssn == _dbContext.Users[i].SSN)
                {
                    _dbContext.Users[i].ReportDateTime = DateTime.Now;

                    return View(_dbContext.Users[i]);
                }
            }

            return RedirectToAction("Index");
        }


        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                if ((user.Male && user.Female) || (!user.Male && !user.Female))
                {
                    return ViewBag.Error = "Must Be One(Male or Female)!";
                }

                user.Id = Guid.NewGuid();

                user.SSN = GenerateSSN(user);

                _userService.Add(user);

                user.ReportDateTime = DateTime.Now;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(Guid id)
        {
            User user = _dbContext.Users.FirstOrDefault(x => x.Id == id);

            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, User user)
        {
            try
            {
                if ((user.Male && user.Female) || (!user.Male && !user.Female))
                {
                    return ViewBag.Error = "Must Be One(Male or Female)!";
                }

                if (id != user.Id)
                {
                    return NotFound();
                }
                user.SSN = GenerateSSN(user);

                user.ReportDateTime = DateTime.Now;

                _userService.Update(user);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View(_userService.Get(id));
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, User user)
        {
            try
            {
                if (id != user.Id)
                {
                    return NotFound();
                }
                _userService.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public string GenerateSSN(User user)
        {
            var ssnObj = new SsnGenerator(user, _dbContext);

            return ssnObj.GetSSN();

            #region SSN

            //string ssn = string.Empty;
            //Random rand = new Random();

            //#region FirstPair

            //if (user.Male)
            //{
            //    var dayOfBirth = user.BirthDateTime.Day;
            //    int num = 11;

            //    for (int i = 1; i < dayOfBirth; i++)
            //    {
            //        num++;
            //    }
            //    if (num <= 41)
            //    {
            //        ssn = num.ToString();
            //    }
            //}

            //if (user.Female)
            //{
            //    var dayOfBirth = user.BirthDateTime.Day;
            //    int num = 51;

            //    for (int i = 1; i < dayOfBirth; i++)
            //    {
            //        num++;
            //    }
            //    if (num <= 81)
            //    {
            //        ssn = num.ToString();
            //    }
            //}
            //#endregion

            //#region SecondPair
            //if (user.BirthDateTime.Year < 2000)
            //{
            //    int month = 1;

            //    if (user.BirthDateTime.Month == 6)
            //    {
            //        ssn += 14.ToString();
            //    }
            //    else
            //    {
            //        for (int i = 1; i < user.BirthDateTime.Month - 1; i++)
            //        {
            //            month++;
            //        }
            //        if (month < 10)
            //        {
            //            ssn += 0.ToString();
            //            ssn += month;
            //        }
            //    }
            //}

            //if (user.BirthDateTime.Year >= 2000)
            //{
            //    int month = 21;

            //    if (user.BirthDateTime.Month == 6)
            //    {
            //        ssn += 34.ToString();
            //    }
            //    else
            //    {
            //        for (int i = 1; i < user.BirthDateTime.Month - 1; i++)
            //        {
            //            month++;
            //        }
            //        ssn += month;
            //    }
            //}

            //#endregion

            //#region ThirthPair

            //if (user.BirthDateTime.Year % 100 < 10)
            //{
            //    ssn += 0.ToString();
            //    ssn += user.BirthDateTime.Year % 100;
            //}
            //else
            //{
            //    ssn += user.BirthDateTime.Year % 100;
            //}

            //#endregion

            //#region FourthPair

            //int fourthPair = 1;

            //if (_dbContext.Users.Count == 0)
            //{
            //    ssn += 0;
            //    ssn += 0;
            //    ssn += 1;
            //}

            //if (_dbContext.Users.Count > 0)
            //{
            //    fourthPair = Convert.ToInt32(_dbContext.Users[0].SSN.Substring(6, 3));
            //}

            //for (int i = 0; i < _dbContext.Users.Count; i++)
            //{
            //    if ((user.Male && _dbContext.Users[i].Male) && (user.BirthDateTime == _dbContext.Users[i].BirthDateTime))
            //    {
            //        fourthPair++;
            //    }

            //    if ((user.Female && _dbContext.Users[i].Female) && (user.BirthDateTime == _dbContext.Users[i].BirthDateTime))
            //    {
            //        fourthPair++;
            //    }
            //}
            //if (fourthPair < 10 && _dbContext.Users.Count > 0)
            //{
            //    ssn += 0;
            //    ssn += 0;
            //    ssn += fourthPair;
            //}
            //if (fourthPair > 10 && fourthPair < 100)
            //{
            //    ssn += 0;
            //    ssn += fourthPair;
            //}

            //#endregion

            //#region LastOne

            //ssn += rand.Next(0, 9);

            //#endregion

            //return ssn;

            #endregion
        }
    }
}