import numpy as np 
import re
import functions as f

num_map = {0:'m', 1:'p', 2:'s', 3:'z'}
color_list = ['m', 'p', 's', 'z']

# the set at hand
class Hand(object):

    def __init__(self,
                 num=0,
                 is_clear=True,
                 mahjongs=[],
                 exposure=[],
                 hidden=[]):
        """
        :param num: 13 or 14
        :param is_clear: has any exposure?
        :param mahjongs: total mahjongs in hand
        :param exposure: the exposed part of mahjongs
        :param hidden: the still hidden part of mahjongs
        """
        self.num = num
        self.is_clear = is_clear
        self.mahjongs = mahjongs
        self.exposure = exposure
        self.hidden = hidden
        self.mpsz = dict({'m': [], 'p': [], 's': [], 'z': []})

    # Output the hand tiles in a formatted way. 123m456p789s11234z
    def to_string(self):
        return ''.join([
                ''.join(
                    [
                        m[0] for m in self.mahjongs
                        if m[1] == ch
                    ]
                )
                + ch
                for ch in color_list])


# Mountain. Randomly initialized at the beginning
class Mountain:

    def __init__(self):

        def regenerate():
            n_numbers = range(1, 10)
            n_colors = range(1, 8)
            chs = ['m', 'p', 's', 'z']

            new_mahjongs = np.array([y for x in [
                map(lambda _: str(_) + ch, n_colors if ch == 'z' else n_numbers)
                for ch in chs] for y in x] * 4)
            np.random.shuffle(new_mahjongs)

            # the new mountain
            return new_mahjongs

        self.mahjongs = regenerate()
        # treasure
        self.treasure = [self.mountain[-6]]
        self.pointer = 0
        self.next = self.mountain[self.pointer]

    def take(self, begin, end):
        res = self.mountain[self.pointer + begin: self.pointer + end+1]
        self.pointer += (end - begin + 1)
        return res


class Mahjong:

    def __init__(self, string, direction, status, hold_by):

        self.number = int(string[0])
        self.color = string[1]
        self.direction = direction
        self.status = status
        self.hold_by = hold_by


class Player(object):

    def __init__(self, name, location, is_dealer, points):
        """
        :param name
        :param location: {'e', 's', 'w', 'n'}
        :param is_dealer: boolean
        :param points: the points the he has
        """
        self.name = name
        self.location = location
        self.is_dealer = is_dealer
        self.points = points
        self.hands = Hand()

        self.next = None

    def take(self, mjs):
        self.hands.mahjongs += mjs




def initialize_players(score=25000):

    p1 = Player("East", 'e', True, score)
    p2 = Player("South", 's', False, score)
    p3 = Player("West", 'w', False, score)
    p4 = Player("North", 'n', False, score)

    p1.next = p2
    p2.next = p3
    p3.next = p4
    p4.next = p1

    return [p1, p2, p3, p4]


def initialize_mountain():

    _mountain = Mountain()
    return _mountain


def distribute(_mountain, _begin_player):

    # take 4 mahjongs each time for 3 times
    _curr_taker = _begin_player
    for _x in range(3):
        for _y in range(4):
            mjs = _mountain.take(0,3)
            _curr_taker.take(mjs)
            _curr_taker = _curr_taker.next

    # take the last few
    for _x in range(4):
        mj = _mountain.take(0, 1)
        _curr_taker.take(mj)
        _curr_taker = _curr_taker.next

    mj = mountain.take(0, 1)
    _curr_taker.take(mj)

    print "distributed"


if __name__ == '__main__':

    # player_1, player_2, player_3, player_4 = initialize_players()
    players = initialize_players()
    [p1, p2, p3, p4] = players
    mountain = initialize_mountain()
    distribute(mountain, p1)






