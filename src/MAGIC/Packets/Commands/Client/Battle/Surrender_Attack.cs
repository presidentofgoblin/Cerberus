﻿using System;
using CRepublic.Magic.Core;
using CRepublic.Magic.Core.Networking;
using CRepublic.Magic.Extensions.Binary;
using CRepublic.Magic.Logic;
using CRepublic.Magic.Logic.Enums;
using CRepublic.Magic.Extensions;
using CRepublic.Magic.Logic.Structure.Slots.Items;
using CRepublic.Magic.Packets.Messages.Server.Battle;

namespace CRepublic.Magic.Packets.Commands.Client.Battle
{
    internal class Surrender_Attack : Command
    {
        internal int Tick;

        public Surrender_Attack(Reader reader, Device client, int id) : base(reader, client, id)
        {
        }

        internal override void Decode()
        {
            this.Tick = this.Reader.ReadInt32();
        }

        internal override void Process()
        {
            if (this.Device.State == State.IN_PC_BATTLE)
            {
                if (!this.Device.Player.Avatar.Variables.IsBuilderVillage && !this.Device.Player.Avatar.Modes.IsAttackingOwnBase)
                {
                    var Battle = Core.Resources.Battles.Get(this.Device.Player.Avatar.Battle_ID);
                    Battle_Command Command = new Battle_Command
                    {
                        Command_Type = this.Identifier,
                        Command_Base = new Command_Base {Tick = this.Tick}
                    };
                    Battle.Add_Command(Command);
                }
            }
            else if (this.Device.State == State.IN_1VS1_BATTLE)
            {
                long UserID = this.Device.Player.Avatar.UserId;
                long BattleID = this.Device.Player.Avatar.Battle_ID_V2;
                var Home = Resources.Battles_V2.GetPlayer(BattleID, UserID);

                Battle_Command Command = new Battle_Command
                {
                    Command_Type = this.Identifier,
                    Command_Base = new Command_Base {Tick = this.Tick}
                };
                Home.Add_Command(Command);
            }
        }
    }
}
