from player import *
from mountain import *
from pool import *
import numpy as np


COLORS = ['m', 'p', 's', 'z']
NUMBERS = range(1, 10)
C2L = {'m': 0, 'p': 1, 's': 2, 'z': 3}
L2C = {0: 'm', 1: 'p', 2: 's', 3: 'z'}
HONBA_FEE = 300
RICHI_FEE = 1000


def generate_all_mahjong(shuffled=True, distinct=False):
    """
    The function of generating a set of mahjong tiles
    :param shuffled: the option to shuffle the tiles
    :param distinct: create a whole mahjong set (each tile has 4 copies) or only one copy
    :return:
    """
    n_numbers, n_colors = range(1, 10), range(1, 8)
    all_mj = np.array([y for x in [
        map(lambda _: str(_) + ch, n_colors if ch == 'z' else n_numbers)
        for ch in COLORS] for y in x] * (1 if distinct else 4))
    if shuffled:
        np.random.shuffle(all_mj)
    return all_mj


ALL_MJ = generate_all_mahjong(distinct=False)
ALL_MJ_DIST = generate_all_mahjong(distinct=True)


# detect the Fertigkeit of all the mahjong set
def func1(vals):
    # vals: list[list]
    for v in vals[:-1]:
        if not v:
            continue
        if len(v) % 3 != 0:
            return False
        else:
            if not func2(sorted(v)):
                return False

    if func2(vals[-1], True):
        return True
    else:
        return False


# detect the Fertigkeit of same color (except Z)
def func2(v, z=False):
    # detect the triple first
    # v = sorted(v)
    if z:
        return len(v) == 3 * len(set(v))

    if len(v) == 0:
        return True
    elif len(v) == 3:
        return len(set(v)) == 1 or (v[0] + 1 == v[1] == v[2] - 1)

    elif len(v) in (6, 9, 12):
        triples = [key for key, val in Counter(v).items() if val >= 3]
        if len(triples) == len(v) / 3:
            return True
        else:
            flag = False
            for t in triples:
                if t == v[0] or t == v[-1]:
                    flag = True
                    for _ in range(3):
                        v.remove(t)
            if flag:
                return func2(v)
            else:
                minv = min(v)
                if minv + 1 in v and minv + 2 in v:
                    for m in minv, minv + 1, minv + 2:
                        v.remove(m)
                    return func2(v)
                else:
                    return False
    else:
        return False


# The class of a single mahjong
class Mahjong:

    def __init__(self, string, direction='e', status=None, hold_by=1):
        self.number = int(string[0])
        self.color = string[1]
        self.direction = direction
        self.status = status
        self.hold_by = hold_by

    def __str__(self):
        return str(self.number) + self.color


def initialize_mountain():
    _mountain = Mountain()
    return _mountain


def initialize_players(score=25000):

    p1 = Player("East", 'e', True, score, uuid=1)
    p2 = Player("South", 's', False, score, uuid=2)
    p3 = Player("West", 'w', False, score, uuid=3)
    p4 = Player("North", 'n', False, score, uuid=4)

    p1.next = p2
    p2.next = p3
    p3.next = p4
    p4.next = p1

    return p1, p2, p3, p4


def distribute(_mountain, start_player):

    cp = start_player

    # take 4 mahjongs each time for 3 times
    for _x in range(3):
        for _y in range(4):
            mjs = _mountain.take(4)
            # cp = players[_y]
            cp.take(mjs)
            cp = cp.next

    # take the last few
    for _x in range(4):
        mj = _mountain.take(1)
        # cp = players[_y]
        cp.take(mj)
        cp = cp.next

    mj = _mountain.take(1)
    cp.take(mj)
    # players[0].take(mj)


if __name__ == '__main__':

    hand = Hand()
    hand.from_str("12345678mp146s4z")
    print(hand.tenpai())
    print(hand.tenpai(1))
    print(hand.tenpai(2))
    print(hand.isolated())
    print(hand)

