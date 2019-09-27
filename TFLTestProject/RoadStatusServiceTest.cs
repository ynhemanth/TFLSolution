using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository;
using Model;
using Service;

namespace TFLTestProject
{
    [TestClass]
    public class RoadStatusServiceTest
    {
        private Mock<IDataProcess> _dataProcess;
        private Mock<IHelperClass> _helperClass;

        [TestInitialize]
        public void Setup()
        {
            _dataProcess = new Mock<IDataProcess>();
            _helperClass = new Mock<IHelperClass>();
        }

        [TestMethod]        
        public void FetchRoadStatusTest()
        {
             var roadStatusService = new RoadStatusService(_dataProcess.Object, _helperClass.Object);
            var mockRoadDetails = new Mock<RoadDetails>();
            var mockRoadStatusInfo = new Mock<RoadStatusInfo>();
            
            _helperClass.Setup(h => h.JsonConverter<RoadStatusInfo>(It.IsAny<string>())).Returns(mockRoadStatusInfo.Object);
            _dataProcess.Setup(d => d.GetRoadStatusCall(mockRoadDetails.Object)).ReturnsAsync("test");
            

            var check = roadStatusService.FetchRoadStatus(mockRoadDetails.Object);
            _dataProcess.Verify(d => d.GetRoadStatusCall(mockRoadDetails.Object));
            _helperClass.Verify(h => h.JsonConverter<RoadStatusInfo>(It.IsAny<string>()));            
        }

        [TestMethod]
        public void FetchRoadStatus_ValidRoad_Test()
        {
            var roadStatusService = new RoadStatusService(_dataProcess.Object, _helperClass.Object);
            var mockRoadDetails = new Mock<RoadDetails>();            

            var fakeRoadStatusInfo = new RoadStatusInfo
            {
                displayName = "The status of the A2 is as follows",
                statusSeverity = "Road Status is Good",
                statusSeverityDescription = "Road Status Description is No Exceptional Delays"
            };

            _helperClass.Setup(h => h.JsonConverter<RoadStatusInfo>(It.IsAny<string>())).Returns(fakeRoadStatusInfo);
            _dataProcess.Setup(d => d.GetRoadStatusCall(mockRoadDetails.Object)).ReturnsAsync("test");


            var check = roadStatusService.FetchRoadStatus(mockRoadDetails.Object);
            _dataProcess.Verify(d => d.GetRoadStatusCall(mockRoadDetails.Object));
            _helperClass.Verify(h => h.JsonConverter<RoadStatusInfo>(It.IsAny<string>()));

            Assert.AreEqual(check.Result.httpStatusCode, null);
        }

        [TestMethod]
        public void FetchRoadStatus_InvalidRoad_Test()
        {
            var roadStatusService = new RoadStatusService(_dataProcess.Object, _helperClass.Object);
            var mockRoadDetails = new Mock<RoadDetails>();

            var fakeRoadStatusInfo = new RoadStatusInfo
            {
                httpStatusCode = "404",
                message = "A233 is not a valid road"
            };

            _helperClass.Setup(h => h.JsonConverter<RoadStatusInfo>(It.IsAny<string>())).Returns(fakeRoadStatusInfo);
            _dataProcess.Setup(d => d.GetRoadStatusCall(mockRoadDetails.Object)).ReturnsAsync("test");


            var check = roadStatusService.FetchRoadStatus(mockRoadDetails.Object);
            _dataProcess.Verify(d => d.GetRoadStatusCall(mockRoadDetails.Object));
            _helperClass.Verify(h => h.JsonConverter<RoadStatusInfo>(It.IsAny<string>()));

            Assert.AreEqual(check.Result.httpStatusCode, Constants.statusCode);
        }
    }
}
