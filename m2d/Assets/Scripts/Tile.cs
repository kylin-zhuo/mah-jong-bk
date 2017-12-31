using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mahjong
{

    public class Tile:IComparable
    {
        int value;
        Suit suit;
        public Sprite face { get; private set; }

        public Tile(int value, Suit suit)
        {
            this.value = value;
            this.suit = suit;
            face = LoadImage();
        }

        public static bool operator <(Tile first, Tile next)
        {
            if (first.suit != next.suit)
                return (first.suit < next.suit);
            else {
                int value1 = first.value, value2 = next.value;
                if (first.value == 10)
                    value1 = 5;
                if (next.value == 10)
                    value2 = 5;
                return (value1 < value2);
            }
        }

        public static bool operator >(Tile first, Tile next)
        {
            if (first.suit != next.suit)
                return (first.suit > next.suit);
            else
            {
                int value1 = first.value, value2 = next.value;
                if (first.value == 10)
                    value1 = 5;
                if (next.value == 10)
                    value2 = 5;
                return (value1 > value2);
            }
        }

        public static bool operator ==(Tile first, Tile next)
        {

            int value1 = first.value, value2 = next.value;
            if (value1 == 10)
                value1 = 5;
            if (value2 == 10)
                value2 = 5;

            return (first.suit == next.suit && value1 == value2);
        }
        public static bool operator !=(Tile first, Tile next)
        {
            int value1 = first.value, value2 = next.value;
            if (value1 == 10)
                value1 = 5;
            if (value2 == 10)
                value2 = 5;

            return (first.suit != next.suit || value1 != value2);

        }

        public override bool Equals(object o)
        {
            try
            {
                return (this == (Tile)o);
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            int value1 = value;
            if (value == 10)
                value1 = 5;
            return value1+(int)suit*10;
        }

        public int CompareTo(object obj)
        {
            if (this == (Tile)obj)
                return 0;
            if (this < (Tile)obj)
                return -1;
            else
                return 1;
        }

        public bool IsTerminal()
        {
            return (IsNumeric() && (value == 1 || value == 9));
        }

        public bool IsHonor()
        {
            return (suit == Suit.wind || suit == Suit.dragon);
        }

        public bool IsNumeric()
        {
            return (!IsHonor());
        }

        public bool IsSimple()
        {
            return (IsNumeric() && !IsTerminal());
        }

        public bool IsGreen()
        {
            if (suit != Suit.souzu && suit != Suit.dragon)
                return false;
            else
            {
                if (suit == Suit.souzu &&
                    (value == 2 || value == 3 || value == 4 || value == 6 || value == 8))
                    return true;
                if (suit == Suit.dragon && value == 2)
                    return true;
                return false;

            }
        }

        public override string ToString()
        {
            string result = "";
            if (IsNumeric())
            {
                if (value == 10)
                    result = "aka 5";
                else
                    result = value.ToString();
                switch (suit)
                {
                    case Suit.manzu:
                        result += " man";
                        break;
                    case Suit.pinzu:
                        result += " pin";
                        break;
                    case Suit.souzu:
                        result += " sou";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (suit == Suit.dragon)
                {
                    switch (value)
                    {
                        case 1:
                            result = "white";
                            break;
                        case 2:
                            result = "red";
                            break;
                        case 3:
                            result = "green";
                            break;
                        default:
                            break;
                    }
                    result += " dragon";
                }
                else
                {
                    switch (value)
                    {
                        case 1:
                            result = "east";
                            break;
                        case 2:
                            result = "south";
                            break;
                        case 3:
                            result = "west";
                            break;
                        case 4:
                            result = "north";
                            break;
                        default:
                            break;
                    }
                    result += " wind";

                }

            }
            return result;
        }

        //should be in Unity part
        public Sprite LoadImage()
        {
            string path = "Tiles/";
            switch (suit)
            {
                case Suit.manzu:
                    path += "man/man";
                    path += value.ToString();
                    break;
                case Suit.pinzu:
                    path += "pin/pin";
                    path += value.ToString();
                    break;
                case Suit.souzu:
                    path += "bamboo/bamboo";
                    path += value.ToString();
                    break;
                case Suit.wind:
                    path += "wind/wind-";
                    switch (value)
                    {
                        case 1:
                            path += "east";
                            break;
                        case 2:
                            path += "south";
                            break;
                        case 3:
                            path += "west";
                            break;
                        case 4:
                            path += "north";
                            break;
                        default:
                            throw (new ArgumentOutOfRangeException("I do not know this wind"));
                    }
                    break;
                case Suit.dragon:
                    path += "dragon/dragon-";
                    switch (value)
                    {
                        case 1:
                            path += "haku";
                            break;
                        case 2:
                            path += "green";
                            break;
                        case 3:
                            path += "chun";
                            break;
                        default:
                            throw (new ArgumentOutOfRangeException("I do not know this dragin"));
                    }
                    break;
                default:
                    throw (new ArgumentOutOfRangeException("I do not know this kind of suit"));
            }
            Sprite pic = Resources.Load<Sprite>(path);
            return pic;
        }

        public Tile DoraBonus()
        {
            return NextTile(true);
        }

        public Tile NextTile(bool wrap = false)
        {
            int result = 1;
            if (suit == Suit.manzu || suit == Suit.pinzu || suit == Suit.souzu)
            {
                switch (value)
                {
                    case 10:
                        result = 6;
                        break;
                    case 9:
                        if (!wrap)
                            return null;
                        result = 1;
                        break;
                    default:
                        result = value + 1;
                        break;
                }
            }
            if (suit == Suit.dragon)
            {
                if (!wrap)
                    return null;
                if (value == 3)
                    result = 1;
                else
                    result = value + 1;
            }
            if (suit == Suit.wind)
            {
                if (!wrap)
                    return null;
                if (value == 4)
                    result = 1;
                else
                    result = value + 1;
            }
            return new Tile(result, this.suit);

        }

        public int GetDoras(Tile[] indicators)
        {
            int numDoras = 0;
            if (value == 10)
                numDoras++;
            foreach (Tile item in indicators)
            {
                if (item.DoraBonus().Equals(this))
                    numDoras++;
            }
            return numDoras;
        }
    }
}
