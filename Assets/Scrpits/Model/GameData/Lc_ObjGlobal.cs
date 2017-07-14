using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSpace.GameCore {
    public class lc_ObjGlobal {
        public int globalyear; /*整体时间*/
        public int round { get; set; }  /*当前回合数*/

        public uint player_id { get; set; } /*玩家ID*/
        public uint player_relid { get; set; } /*玩家宗教ID*/

        public lc_ObjGlobal() {
            round = 1;
        }

        public void setBeginRound(int round) {
            this.round = round;
        }

        public void NextRound() {
            round++;
        }

    }
}
