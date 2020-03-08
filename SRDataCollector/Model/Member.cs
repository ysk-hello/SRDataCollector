using System;
using System.Collections.Generic;
using System.Text;

namespace SRDataCollector.Model
{
    public class Member
    {
        public int MemberId { get; set; }

        public string Name { get; set; }

        public int RoomId { get; set; }

        public int TeamId { get; set; }

        public Team Team { get; set; }
    }
}
