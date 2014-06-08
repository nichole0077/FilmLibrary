using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FilmLibrary.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using FilmLibrary.Models;

namespace FilmLibrary.Tests.Controllers
{
    [TestClass]
    public class MovieControllerTest
    {
        MovieController Controller;

        [TestInitialize]
        public void Setup()
        {
            Controller = new MovieController();
        }

        [TestMethod]
        public void Index_view_loads()
        {
            var result = Controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Index_view_has_movies()
        {
            var result = Controller.Index() as ViewResult;
            var model = result.Model as List<Movie>;
            Assert.IsNotNull(model);
            Assert.IsTrue(model.Count > 0);

            Assert.IsNotNull(model.Find(x => x.Title == "Snow White"));

        }

    }
}
