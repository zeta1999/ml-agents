using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.MLAgents.Extensions.Sensors;

namespace Unity.MLAgents.Extensions.Tests.Sensors
{
    public class CountingGridSensorPerceiveTests
    {
        GameObject testGo;
        GameObject boxGo;
        GameObject boxGoTwo;
        GameObject boxGoThree;
        // CountingGridSensor gridSensor;
        GridSensorComponent gridSensorComponent;

        // Use built-in tags
        const string k_Tag1 = "Player";
        const string k_Tag2 = "Respawn";


        public GameObject CreateBlock(Vector3 postion, string tag, string name)
        {
            GameObject boxGo = new GameObject(name);
            boxGo.tag = tag;
            boxGo.transform.position = postion;
            boxGo.AddComponent<BoxCollider>();
            return boxGo;
        }

        [UnitySetUp]
        public IEnumerator SetupScene()
        {
            testGo = new GameObject("test");
            testGo.transform.position = Vector3.zero;
            gridSensorComponent = testGo.AddComponent<GridSensorComponent>();

            boxGo = CreateBlock(new Vector3(3f, 0f, 3f), k_Tag1, "box1");

            yield return null;
        }

        [TearDown]
        public void ClearScene()
        {
            Object.DestroyImmediate(boxGo);
            Object.DestroyImmediate(boxGoTwo);
            Object.DestroyImmediate(boxGoThree);
            Object.DestroyImmediate(testGo);
        }

        [UnityTest]
        public IEnumerator OneChannelDepthOneCount()
        {
            string[] tags = { k_Tag1 };
            int[] depths = { 1 };
            Color[] colors = { Color.red, Color.magenta };
            GridObsTestUtils.SetComponentParameters(gridSensorComponent, tags, depths, GridDepthType.Counting,
                1f, 1f, 10, 10, LayerMask.GetMask("Default"), false, colors);
            var gridSensor = (GridSensor)gridSensorComponent.CreateSensors()[0];

            yield return null;

            gridSensor.Perceive();

            Assert.AreEqual(10 * 10 * 1, gridSensor.m_PerceptionBuffer.Length);

            int[] subarrayIndicies = new int[] { 77, 78, 87, 88 };
            float[][] expectedSubarrays = GridObsTestUtils.DuplicateArray(new[] { 1f }, 4);
            float[] expectedDefault = new float[] { 0 };
            GridObsTestUtils.AssertSubarraysAtIndex(gridSensor.m_PerceptionBuffer, subarrayIndicies, expectedSubarrays, expectedDefault);
        }

        [UnityTest]
        public IEnumerator OneChannelDepthOneCountMax()
        {
            string[] tags = { k_Tag1 };
            int[] depths = { 1 };
            Color[] colors = { Color.red, Color.magenta };
            GridObsTestUtils.SetComponentParameters(gridSensorComponent, tags, depths, GridDepthType.Counting,
                1f, 1f, 10, 10, LayerMask.GetMask("Default"), false, colors);
            var gridSensor = (GridSensor)gridSensorComponent.CreateSensors()[0];

            boxGoTwo = CreateBlock(new Vector3(3.1f, 0f, 3.1f), k_Tag1, "box2");

            yield return null;

            gridSensor.Perceive();

            Assert.AreEqual(10 * 10 * 1, gridSensor.m_PerceptionBuffer.Length);

            int[] subarrayIndicies = new int[] { 77, 78, 87, 88 };
            float[][] expectedSubarrays = GridObsTestUtils.DuplicateArray(new[] { 1f }, 4);
            float[] expectedDefault = new float[] { 0f };
            GridObsTestUtils.AssertSubarraysAtIndex(gridSensor.m_PerceptionBuffer, subarrayIndicies, expectedSubarrays, expectedDefault);
        }

        [UnityTest]
        public IEnumerator OneChannelDepthFourCount()
        {
            string[] tags = { k_Tag1 };
            int[] depths = { 4 };
            Color[] colors = { Color.red, Color.magenta };
            GridObsTestUtils.SetComponentParameters(gridSensorComponent, tags, depths, GridDepthType.Counting,
                1f, 1f, 10, 10, LayerMask.GetMask("Default"), false, colors);
            var gridSensor = (GridSensor)gridSensorComponent.CreateSensors()[0];

            boxGoTwo = CreateBlock(new Vector3(3.1f, 0f, 3.1f), k_Tag1, "box2");

            yield return null;

            gridSensor.Perceive();

            Assert.AreEqual(10 * 10 * 1, gridSensor.m_PerceptionBuffer.Length);

            int[] subarrayIndicies = new int[] { 77, 78, 87, 88 };
            float[][] expectedSubarrays = GridObsTestUtils.DuplicateArray(new[] { .5f }, 4);
            float[] expectedDefault = new float[] { 0 };
            GridObsTestUtils.AssertSubarraysAtIndex(gridSensor.m_PerceptionBuffer, subarrayIndicies, expectedSubarrays, expectedDefault);
        }

        [UnityTest]
        public IEnumerator TwoChannelDepthFourCount()
        {
            string[] tags = { k_Tag1, k_Tag2 };
            int[] depths = { 4, 1 };
            Color[] colors = { Color.red, Color.magenta };
            GridObsTestUtils.SetComponentParameters(gridSensorComponent, tags, depths, GridDepthType.Counting,
                1f, 1f, 10, 10, LayerMask.GetMask("Default"), false, colors);
            var gridSensor = (GridSensor)gridSensorComponent.CreateSensors()[0];

            boxGoTwo = CreateBlock(new Vector3(3.1f, 0f, 3.1f), k_Tag1, "box2");
            boxGoThree = CreateBlock(new Vector3(2.9f, 0f, 2.9f), k_Tag2, "box2");

            yield return null;

            gridSensor.Perceive();

            Assert.AreEqual(10 * 10 * 2, gridSensor.m_PerceptionBuffer.Length);

            int[] subarrayIndicies = new int[] { 77, 78, 87, 88 };
            float[][] expectedSubarrays = GridObsTestUtils.DuplicateArray(new[] { .5f, 1 }, 4);
            float[] expectedDefault = new float[] { 0, 0 };
            GridObsTestUtils.AssertSubarraysAtIndex(gridSensor.m_PerceptionBuffer, subarrayIndicies, expectedSubarrays, expectedDefault);
        }
    }
}
