﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.AI;
using AIGame.CoreGame;
using AIGame.Interfaces;

namespace AIGame.CoreGame
{
    public class SignalOrigin : ISignalOrigin
    {
        public Tuple<int, int> OriginCoordinates { get; private set; }
        public Side Broadcaster { get; private set; }
        public Broadcast Broadcast { get; private set; }
        public SignalType Type { get; private set; }
        public int Age { get; set; }
        public SignalOrigin(Tuple<int, int> originCoordinates, Side broadcaster,  SignalType type)
        {
            OriginCoordinates = originCoordinates;
            Broadcaster = broadcaster;
            Type = type;
        }
        public SignalOrigin(IUnit unit,Broadcast broadcast)
        {
            OriginCoordinates = unit.Coordinates;
            Broadcaster = unit.Owner;

            Broadcast = broadcast;

            Type = SignalType.Broadcast;
        }

        public Signal GetSignal(IUnit unit)
        {
            DirectionPrecise directionPrecise = Helper.GetDirection(unit.Coordinates, OriginCoordinates);
            Broadcast broadcast = new Broadcast();
            bool sameSide = Broadcaster == unit.Owner;
            bool encrypted = Broadcast.Type == BroadcastType.Encrypted;
            broadcast.Type = Broadcast.Type;

            if (!sameSide && encrypted)
                broadcast.Message = "**Encrypted**";
            else
                broadcast.Message = Broadcast.Message;

            return new Signal(directionPrecise, broadcast, Type);
        }

    }
}
