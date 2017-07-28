import numpy as np 
import re
import functions as f

num_map = {0:'m', 1:'p', 2:'s', 3:'z'}
color_list = ['m', 'p', 's', 'z']

# the set at hand
class Hand:

    def __init__(self,
                 id="",
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
        self.id = id
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

    def take(self, mjs):
        self.mahjongs += mjs


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
        self.treasure = [self.mahjongs[-6]]
        self.pointer = 0
        self.next = self.mahjongs[self.pointer]

    def take(self, num):
        res = list(self.mahjongs[self.pointer: self.pointer + num])
        self.pointer += num
        return res


class Mahjong:

    def __init__(self, string, direction, status, hold_by):

        self.number = int(string[0])
        self.color = string[1]
        self.direction = direction
        self.status = status
        self.hold_by = hold_by


class Player:

    def __init__(self, name, location, is_dealer, score, hand_id):
        """
        :param name
        :param location: {'e', 's', 'w', 'n'}
        :param is_dealer: boolean
        :param points: the points the he has
        """
        self.name = name
        self.location = location
        self.is_dealer = is_dealer
        self.score = score
        self.hands = Hand(id=hand_id)

        self.next = None

    def take(self, mjs):
        self.hands.take(mjs)


def initialize_players(score=25000):

    p1 = Player("East", 'e', True, score, hand_id=1)
    p2 = Player("South", 's', False, score, hand_id=2)
    p3 = Player("West", 'w', False, score, hand_id=3)
    p4 = Player("North", 'n', False, score, hand_id=4)

    # p1.next = p2
    # p2.next = p3
    # p3.next = p4
    # p4.next = p1

    return [p1, p2, p3, p4]


def initialize_mountain():

    _mountain = Mountain()
    return _mountain


def distribute(_mountain, players):

    # cp = p1

    # take 4 mahjongs each time for 3 times
    for _x in range(3):
        for _y in range(4):
            mjs = _mountain.take(4)
            cp = players[_y]
            cp.take(mjs)
            # cp = cp.next

    # take the last few
    for _x in range(4):
        mj = _mountain.take(1)
        cp = players[_y]
        cp.take(mj)
        # cp = cp.next

    mj = mountain.take(1)
    # cp.take(mj)
    players[0].take(mj)

    print "distributed"


if __name__ == '__main__':

    # player_1, player_2, player_3, player_4 = initialize_players()
    players = initialize_players()

    mountain = initialize_mountain()
    distribute(mountain, players)

    [p1, p2, p3, p4] = players
    print p1.hands.mahjongs
    # print mountain.mahjongs