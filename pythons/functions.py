import numpy as np

def transform(num):
	print 1

def hasWinned(num):
	#locate the pair first
	rows, cols = np.where(num >= 2)
	for i in range(len(rows)):
		row, col = rows[i], cols[i]
		num[row][col] -= 2
		if areTriples(num): 
			num[row][col] += 2
			return True
		num[row][col] += 2
	return False

# whether the hand is a combination of 1~4 triples
def areTriples(num):
	# if remained are all repeats, or nothing, return true
	if np.sum(num % 3) == 0: 
		return True
	row, col = searchTriple(num)
	if row == None: 
		return False
	num[row][col:col+3] -= [1,1,1]
	if areTriples(num): 
		num[row][col:col+3] += [1,1,1] 
		return True
	num[row][col:col+3] += [1,1,1] 
	return False

# return the first index of the series
def searchTriple(num):
	# only search the first 3 rows since there are no series in characters
	for i in range(3):
		for j in range(7):
			if sum(num[i][j:j+3] >= [1,1,1]) == 3:
				return i,j
	return None, None





