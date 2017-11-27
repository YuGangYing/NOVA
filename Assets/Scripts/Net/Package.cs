using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Package
{
    protected int curChar = 0;
    public UInt16 ReadUInt16(ref string stream)
    {
        UInt16 value = stream[curChar];
        curChar++;
        return value;
    }

    public void WriteInt16(UInt16 value, ref string stream)
    {
        stream = stream.PadRight(stream.Length + 1, (char)value);
    }
}
//0
public class LoginPkg : Package
{
    public UInt16 PlayerID;

    public void Read(ref string stream)
    {
        UInt16 msgID = ReadUInt16(ref stream);
        PlayerID = ReadUInt16(ref stream);
    }

    public void Write(ref string stream)
    {
        WriteInt16('0', ref stream);
        WriteInt16(PlayerID, ref stream);
    }
}

// 1
public class ReadyPkg : Package
{
    public UInt16[] LegID = { 0, 1, 2 };
    public UInt16[] BodyID = { 0, 1, 2 };
    public UInt16[] WeaponID = { 0, 1, 2 };

    public void Read(ref string stream)
    {
        UInt16 msgID = ReadUInt16(ref stream);
        for (int iSlot = 0; iSlot < 3; iSlot++)
        {
            LegID[iSlot] = ReadUInt16(ref stream);
            BodyID[iSlot] = ReadUInt16(ref stream);
            WeaponID[iSlot] = ReadUInt16(ref stream);
        }
    }

    public void Write(ref string stream)
    {
        WriteInt16('1', ref stream);
        for (int iSlot = 0; iSlot < 3; iSlot++)
        {
            WriteInt16(LegID[iSlot], ref stream);
            WriteInt16(BodyID[iSlot], ref stream);
            WriteInt16(WeaponID[iSlot], ref stream);
        }
    }
}
// 2
public class BattleStartPkg : Package
{
    public UInt16[] LegID = { 0, 1, 2 };
    public UInt16[] BodyID = { 0, 1, 2 };
    public UInt16[] WeaponID = { 0, 1, 2 };

    public void Read(ref string stream)
    {
        UInt16 msgID = ReadUInt16(ref stream);
        for (int iSlot = 0; iSlot < 3; iSlot++)
        {
            LegID[iSlot] = ReadUInt16(ref stream);
            BodyID[iSlot] = ReadUInt16(ref stream);
            WeaponID[iSlot] = ReadUInt16(ref stream);
        }
    }
    public void Write(ref string stream)
    {
        WriteInt16('2', ref stream);
    }
}
// 3
public class NewMechPkg : Package
{
    public UInt16 MechSlot;
    public UInt16 MapX;
    public UInt16 MapY;

    public void Read(ref string stream)
    {
        UInt16 msgID = ReadUInt16(ref stream);
        MechSlot = ReadUInt16(ref stream);
        MapX = ReadUInt16(ref stream);
        MapY = ReadUInt16(ref stream);
    }

    public void Write(ref string stream)
    {
        WriteInt16('3', ref stream);
        WriteInt16(MechSlot, ref stream);
        WriteInt16(MapX, ref stream);
        WriteInt16(MapY, ref stream);
    }
}
// 4
public class AccountPkg : Package
{
    public UInt16 WinID;
    public UInt16 LoserID;

    public void Read(ref string stream)
    {
        UInt16 msgID = ReadUInt16(ref stream);
        WinID = ReadUInt16(ref stream);
        LoserID = ReadUInt16(ref stream);
    }

    public void Write(ref string stream)
    {
        WriteInt16('4', ref stream);
        WriteInt16(WinID, ref stream);
        WriteInt16(LoserID, ref stream);
    }
}
// 5
public class ExitBattlePkg : Package
{
    public void Read(ref string stream)
    {
        UInt16 msgID = ReadUInt16(ref stream);
    }
    public void Write(ref string stream)
    {
        WriteInt16('5', ref stream);
    }
}