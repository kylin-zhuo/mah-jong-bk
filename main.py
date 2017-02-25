from mahjong import Hand
from mahjong import Mount
import functions as f


if __name__ == '__main__':
	mount = Mount()
	hand = Hand()
	print mount.mount
	print mount.treasures
	hand.insert('123456789m1p1s123z')
	print hand.N()
	print 


