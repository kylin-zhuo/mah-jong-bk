import numpy as np 
import re

num_map = {0:'m', 1:'p', 2:'s', 3:'z'}
chars_map = {1:'e', 2:'s', 3:'w', 4:'n', 5:'z', 6:'f', 7:'b'}

def transform(num):
	print 1

class Hand:
	def __init__(self):
		self.numbers = np.zeros([4,9]).astype(int)
		# self.chars = np.zeros(7).astype(int)

	def N(self):
		return np.sum(self.numbers)

	# Output the hand tiles in a formatted way. 123m456p789s11234z
	def toString(self):
		n = ['', '', '', '']
		for i in range(1,5):
			(n1, n2) = np.where(self.numbers == i)
			n2 = n2 + 1
			for j in range(len(n1)):
				n[n1[j]] += str(n2[j])*i
		f = lambda x: ''.join(sorted(x))
		n = map(f, n)
		return n[0]+'m'+n[1]+'p'+n[2]+'s'+n[3]+'z'

	# input the sequence into the hand tiles
	def insert(self, seq):
		s = re.split('[mpsz]',seq)[:-1]
		for i in range(len(s)):
			part = s[i]
			for j in range(len(part)):
				self.numbers[i][int(part[j])-1] += 1

	# take a tile from mountain or other's discarded tile
	def take(self, mahjong):
		num, color = int(mahjong[0]), mahjong[1]
		self.numbers[num_map[color]][num] += 1

	# discard a tile to the pool
	def discard(self, mahjong):
		num, color = int(mahjong[0]), mahjong[1]
		self.numbers[num_map[color]][num] -= 1

	def hasWinned(self):
		pass

	
class Mount:
	N = 34
	def __init__(self):
		self.regenerate()

	def regenerate(self):
		self.mount = np.array(range(1,self.N+1)*4)
		np.random.shuffle(self.mount)
		self.treasures = [self.mount[-6]]

if __name__ == '__main__':
	mount = Mount()
	hand = Hand()
	print mount.mount
	print mount.treasures

	hand.insert('123456789m1p1s123z')
	print hand.N()
	print 







