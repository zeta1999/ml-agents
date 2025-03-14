using NUnit.Framework;
using UnityEngine;
using Unity.MLAgents.Extensions.Sensors;

namespace Unity.MLAgents.Extensions.Tests.Sensors
{
    public class ChannelShapeTests
    {

        GameObject testGo;
        GridSensorComponent gridSensorComponent;

        [SetUp]
        public void SetupScene()
        {
            testGo = new GameObject("test");
            testGo.transform.position = Vector3.zero;
            gridSensorComponent = testGo.AddComponent<GridSensorComponent>();
        }

        [TearDown]
        public void ClearScene()
        {
            Object.DestroyImmediate(testGo);
        }

        [Test]
        public void OneChannel()
        {
            string[] tags = { "Box" };
            int[] depths = { 1 };
            Color[] colors = { Color.magenta };
            GridObsTestUtils.SetComponentParameters(gridSensorComponent, tags, depths, GridDepthType.Channel,
                1f, 1f, 10, 10, LayerMask.GetMask("Default"), false, colors);
            var gridSensor = (GridSensor)gridSensorComponent.CreateSensors()[0];

            var expectedShape = new InplaceArray<int>(10, 10, 1);
            Assert.AreEqual(expectedShape, gridSensor.GetObservationSpec().Shape);

        }

        [Test]
        public void TwoChannel()
        {
            string[] tags = { "Box", "Ball" };
            int[] depths = { 1, 1 };
            Color[] colors = { Color.magenta };
            GridObsTestUtils.SetComponentParameters(gridSensorComponent, tags, depths, GridDepthType.Channel,
                1f, 1f, 10, 10, LayerMask.GetMask("Default"), false, colors);
            var gridSensor = (GridSensor)gridSensorComponent.CreateSensors()[0];

            var expectedShape = new InplaceArray<int>(10, 10, 2);
            Assert.AreEqual(expectedShape, gridSensor.GetObservationSpec().Shape);

        }

        [Test]
        public void SevenChannel()
        {
            string[] tags = { "Box", "Ball" };
            int[] depths = { 1, 1, 1, 1, 1, 1, 1 };
            Color[] colors = { Color.magenta };
            GridObsTestUtils.SetComponentParameters(gridSensorComponent, tags, depths, GridDepthType.Channel,
                1f, 1f, 10, 10, LayerMask.GetMask("Default"), false, colors);
            var gridSensor = (GridSensor)gridSensorComponent.CreateSensors()[0];

            var expectedShape = new InplaceArray<int>(10, 10, 7);
            Assert.AreEqual(expectedShape, gridSensor.GetObservationSpec().Shape);

        }
    }
}
