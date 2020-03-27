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

        public static void UpgradeNetSegments(string fromNetInfoSegmentName, string toNetInfoSegmentName)
        {
            //from Road Removal Tool by egi
            var replacementNetInfo = PrefabCollection<NetInfo>.FindLoaded(toNetInfoSegmentName);
            var netSegmentIds = GetNetSegmentIds(fromNetInfoSegmentName);
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
                NetManager.instance.UpdateSegmentRenderer(netSegmentId, true);
            }
        }


        public static List<ushort> GetNetSegmentIds(string netInfoSegmentName)
        {
            var result = new List<ushort>();
            var bufferLength = (ushort)NetManager.instance.m_segments.m_buffer.Length;
            int idCount = 0;
            for (ushort i = 0; i < bufferLength; i++)
            {
                var segment = NetManager.instance.m_segments.m_buffer[i];
                if (segment.Info == null)
                {
                    continue;
                }

                if (segment.Info.name == netInfoSegmentName && (segment.m_endNode != 0 || segment.m_startNode != 0))
                {
                    result.Add(i);
                    idCount++;
                }


            }
            Debug.Log("Number of " + netInfoSegmentName + " : " + idCount);
            return result;
        }
    }
}
