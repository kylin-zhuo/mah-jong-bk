using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mahjong
{
    class Wall
    {
        private List<Tile> collection = new List<Tile>();

        public Wall()
        {
            for (Suit i = Suit.manzu; i != Suit.wind; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    int num = 4;
                    if (j == 5)
                        num = 3;
                    for (int k = 0; k < num; k++)
                        collection.Add(new Tile(j, i));
                }
                collection.Add(new Tile(10, i));
            }
            for (int i = 1; i < 5; i++)
                for (int j = 0; j < 4; j++)
                    collection.Add(new Tile(i, Suit.wind));
            for (int i = 1; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    collection.Add(new Tile(i, Suit.dragon));

            Shuffle();

        }

        public Tile GetTile()
        {
            Tile temp = collection[0];
            collection.RemoveAt(0);
            return temp;
        }

        void Shuffle()
        {
            for (int i = 0; i < collection.Count; i++)
            {
                Tile temp = collection[i];
                int num = UnityEngine.Random.Range(0, collection.Count);
                collection[i] = collection[num];
                collection[num] = temp;

            }
        }

    }
}
