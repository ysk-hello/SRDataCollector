using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Text;

namespace SRDataCollector.Model
{
    public class Data
    {
        public Data()
        {

        }

        public Data(Member member, DateTime dateTime, int followerNum)
        {
            Member = member;
            DateTime = dateTime;
            FollowerNum = followerNum;
        }

        public int MemberId { get; set; }

        public Member Member { get; set; }

        public DateTime DateTime { get; set; }

        public int FollowerNum { get; set; }

        public static Data CreateData(Member member)
        {
            var uri = new Uri(string.Format("https://www.showroom-live.com/api/room/profile?room_id={0}", member.RoomId));

            var text = string.Empty;
            using (var client = new HttpClient())
            {
                // データを取得する
                // Resultで同期
                text = client.GetStringAsync(uri).Result;
            }

            var obj = JObject.Parse(text);

            var ok = int.TryParse(obj["follower_num"].ToString(), out int num);

            // Intに変換できなかった場合
            if (!ok) num = -1;

            return new Data(member, DateTime.Now, num);
        }
    }
}
