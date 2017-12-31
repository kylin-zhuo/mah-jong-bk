using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mahjong
{
    class Hand
    {
        private List<Tile> collection;
        private List<Meld> melds;

        private Wall wall;

        public Hand()
        {
            wall = new Wall();
            collection = new List<Tile>();
            melds = new List<Meld>();
        }

        public void GetFromWall()
        {
            collection.Add(wall.GetTile());
        }

        public void Sort()
        {
            collection.Sort();
        }

        public Tile GetTile(int index)
        {
            return collection[index];
        }

        public Tile[] isTempai()
        {
            List<Tile> result = new List<Tile>();
            if (melds.Count == 0) {
                List<Tile> temp = collection;
                temp.Sort();
                //check for pairs
                int pairs = 0;
                for (int i = 0; i < temp.Count; i++)
                {
                    if (i < temp.Count - 1 && collection[i] == collection[i + 1])
                    {
                        pairs++;
                        i++;
                    }
                    else
                        result.Add(collection[i]);
                }
                if (pairs == 6)
                    return result.ToArray();
                else
                    result.Clear();
            }



            return result.ToArray();
        }
    }
}
