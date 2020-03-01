using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkReplacer
{
    static class Tools
    {
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
