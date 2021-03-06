// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.QualityTools.Testing.Fakes;
using System.Web.Http.Results;
using AccessibilityInsights.Actions.Contexts;

namespace AccessibilityInsights.WebApiHost.Controllers.Tests
{
    [TestClass()]
    public class DataControllerTests
    {
        [TestMethod()]
        public void TestData_ElementContext_Succeeded_ElementExists()
        {
            using (ShimsContext.Create())
            {
                var guid = Guid.NewGuid();
                var controller = new DataController();
                var element = new AccessibilityInsights.Core.Bases.Fakes.ShimA11yElement();
                var context = new AccessibilityInsights.Actions.Contexts.Fakes.ShimElementContext();

                context.ElementGet = () => element;
                context.IdGet = () => guid;

                AccessibilityInsights.Actions.Fakes.ShimGetDataAction.ExistElementContextGuid = g => true;
                AccessibilityInsights.Actions.Fakes.ShimGetDataAction.GetElementContextGuid = g => context;

                var result = controller.ElementContext(guid) as OkNegotiatedContentResult<ElementContext>;
                Assert.IsNotNull(result);
                Assert.AreEqual(guid, result.Content.Id);
            }
        }

        [TestMethod()]
        public void TestData_ElementContext_NotFound_ElementNotExists()
        {
            using (ShimsContext.Create())
            {
                var guid = Guid.NewGuid();
                var controller = new DataController();

                AccessibilityInsights.Actions.Fakes.ShimGetDataAction.ExistElementContextGuid = g => false;

                var result = controller.ElementContext(guid);
                Assert.IsTrue(result is NotFoundResult);
            }
        }

        [TestMethod()]
        public void TestData_ElementContext_BadRequest_Exception()
        {
            using (ShimsContext.Create())
            {
                var guid = Guid.NewGuid();
                var controller = new DataController();

                AccessibilityInsights.Actions.Fakes.ShimGetDataAction.ExistElementContextGuid = g => throw new Exception();

                var result = controller.ElementContext(guid);
                Assert.IsTrue(result is BadRequestErrorMessageResult);
            }
        }

        [TestMethod()]
        public void TestData_DataContext_Succeeded_ElementExists()
        {
            using (ShimsContext.Create())
            {
                var guid = Guid.NewGuid();
                var controller = new DataController();
                var element = new AccessibilityInsights.Core.Bases.Fakes.ShimA11yElement();
                var data = new AccessibilityInsights.Actions.Contexts.Fakes.ShimElementDataContext();

                AccessibilityInsights.Actions.Fakes.ShimGetDataAction.ExistElementContextGuid = g => true;
                AccessibilityInsights.Actions.Fakes.ShimGetDataAction.GetElementDataContextGuid = g => data;

                var result = controller.DataContext(guid) as OkNegotiatedContentResult<ElementDataContext>;
                Assert.IsNotNull(result);
                Assert.AreEqual(data, result.Content);
            }
        }

        [TestMethod()]
        public void TestData_DataContext_NotFound_ElementNotExists()
        {
            using (ShimsContext.Create())
            {
                var guid = Guid.NewGuid();
                var controller = new DataController();

                AccessibilityInsights.Actions.Fakes.ShimGetDataAction.ExistElementContextGuid = g => false;

                var result = controller.DataContext(guid);
                Assert.IsTrue(result is NotFoundResult);
            }
        }

        [TestMethod()]
        public void TestData_DataContext_BadRequest_Exception()
        {
            using (ShimsContext.Create())
            {
                var guid = Guid.NewGuid();
                var controller = new DataController();

                AccessibilityInsights.Actions.Fakes.ShimGetDataAction.ExistElementContextGuid = g => throw new Exception();

                var result = controller.DataContext(guid);
                Assert.IsTrue(result is BadRequestErrorMessageResult);
            }
        }

    }
}
