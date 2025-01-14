﻿using System;
using CRepublic.Magic.Core.Networking;
using CRepublic.Magic.Extensions;
using CRepublic.Magic.Extensions.Binary;
using CRepublic.Magic.Files;
using CRepublic.Magic.Files.CSV_Logic;
using CRepublic.Magic.Logic;
using CRepublic.Magic.Logic.Structure.Slots.Items;
using CRepublic.Magic.Packets.Messages.Server.Battle;

namespace CRepublic.Magic.Packets.Messages.Client.Battle
{
    internal class Attack_NPC : Message
    {
        internal int Npc_ID;
        public Attack_NPC(Device device) : base(device)
        {
            // Attack_NPC.
        }

        internal override void Decode()
        {
            this.Npc_ID = this.Reader.ReadInt32();
        }
        
        internal override void Process()
        {
            new Npc_Data(this.Device) { Npc_ID = this.Npc_ID }.Send();
            int Index = this.Device.Player.Avatar.Npcs.FindIndex(N => N.NPC_Id == this.Npc_ID);

            if (Index < 0)
            {
                if (this.Npc_ID == 17000000)
                    this.Device.Player.Avatar.Mission_Finish(21000002);
                else if (this.Npc_ID == 17000001)
                    this.Device.Player.Avatar.Mission_Finish(21000009);

                this.Device.Player.Avatar.Npcs.Add(new Npc(this.Npc_ID));
            }
        }
    }
}
