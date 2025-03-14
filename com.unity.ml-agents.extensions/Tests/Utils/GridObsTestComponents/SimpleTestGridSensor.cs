using UnityEngine;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Extensions.Sensors;

namespace Unity.MLAgents.Extensions.TestUtils.Sensors
{
    public class SimpleTestGridSensor : GridSensor
    {
        public SimpleTestGridSensor(
            string name,
            Vector3 cellScale,
            Vector3Int gridNumSide,
            bool rotateWithAgent,
            int[] channelDepth,
            string[] detectableObjects,
            LayerMask observeMask,
            GridDepthType depthType,
            GameObject root,
            SensorCompressionType compression,
            int maxColliderBufferSize,
            int initialColliderBufferSize
        ) : base(
            name,
            cellScale,
            gridNumSide,
            rotateWithAgent,
            channelDepth,
            detectableObjects,
            observeMask,
            depthType,
            root,
            compression,
            maxColliderBufferSize,
            initialColliderBufferSize)
        { }
        protected override float[] GetObjectData(GameObject currentColliderGo, int type_index)
        {
            return (float[])currentColliderGo.GetComponent<GridSensorDummyData>().Data.Clone();
        }
    }

    public class SimpleTestGridSensorComponent : GridSensorComponent
    {
        public override ISensor[] CreateSensors()
        {
            m_Sensor = new SimpleTestGridSensor(
                SensorName,
                CellScale,
                GridSize,
                RotateWithAgent,
                ChannelDepths,
                DetectableObjects,
                ColliderMask,
                DepthType,
                gameObject,
                CompressionType,
                MaxColliderBufferSize,
                InitialColliderBufferSize
            );
            return new ISensor[] { m_Sensor };
        }
    }
}
