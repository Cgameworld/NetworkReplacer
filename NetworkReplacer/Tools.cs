using ColossalFramework;
using ColossalFramework.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkReplacer
{
    static class Tools
    {

        public static void UpgradeNetSegments(string netInfoSegmentName)
        {
            //from Road Removal Tool
            var replacementNetInfo = PrefabCollection<NetInfo>.FindLoaded("Large Road");
            var netSegmentIds = GetNetSegmentIds(netInfoSegmentName);
            var randomizer = new Randomizer();
            foreach (var netSegmentId in netSegmentIds)
            {
                var segment = NetManager.instance.m_segments.m_buffer[netSegmentId];
                segment.Info.m_netAI.ManualDeactivation(netSegmentId, ref segment);
                var direction = segment.GetDirection(netSegmentId);

                //create a new segment over the old segment
                NetManager.instance.CreateSegment(
                    out ushort newSegmentId,
                    ref randomizer,
                    replacementNetInfo,
                    segment.m_startNode,
                    segment.m_endNode,
                    segment.m_startDirection,
                    segment.m_endDirection,
                    segment.m_buildIndex,
                    Singleton<SimulationManager>.instance.m_currentBuildIndex,
                    (segment.m_flags & NetSegment.Flags.Invert) != NetSegment.Flags.None
                );

                //demolish the old segment
                segment.Info.m_netAI.ManualDeactivation(netSegmentId, ref segment);
                NetManager.instance.ReleaseSegment(netSegmentId, false);
            }
        }

        public static List<ushort> GetNetSegmentIds(string netInfoSegmentName)
        {
            Debug.Log("e " + netInfoSegmentName);
            var result = new List<ushort>();
            var bufferLength = (ushort)NetManager.instance.m_segments.m_buffer.Length;
            for (ushort i = 0; i < bufferLength; i++)
            {
                var segment = NetManager.instance.m_segments.m_buffer[i];
                if (segment.Info == null)
                {
                    continue;
                }

                if (segment.Info.name == netInfoSegmentName)
                {
                    Debug.Log("seg" + i);
                    result.Add(i);
                }
            }

            return result;
        }
    }
}
