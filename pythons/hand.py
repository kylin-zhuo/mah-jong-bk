from collections import Counter
import itertools
import re
from mahjong import *


class Hand:

    def __init__(self,
                 uuid="",
                 num=0,
                 is_clear=True,
                 from_str=None
                 ):
        """
        :param uuid: the unique ID of the hand
        :param num: 13 or 14
        :param is_clear: has any exposure?
        :param from_str: create the hand from an input string
        """
        self.uuid = uuid
        self.num = num
        self.is_clear = is_clear
        self.mahjongs = []  # the part that can do the in/out
        self.exposure = []  # max length: 4
        self.hidden = []  # the part for the hidden kan
        # the most important part of core:
        self.mpsz = dict({'m': [], 'p': [], 's': [], 'z': []})  # the part that can do the in/out
        if from_str:
            self.from_str(from_str)

    def update(self):
        """
        Organizing the hand.
        :return: void
        """
        self.mahjongs = list(itertools.chain(*[map(lambda x: str(x) + ch, self.mpsz[ch]) for ch in COLORS]))

    def get_num(self):
        return self.num

    def from_str(self, string):
        """
        Form the hand with a string. The string should be in format: ????m???p???s???z
        :param string:
        :return:
        """
        tmp = re.split(r"[mpsz]", string)[:-1]
        values = list(map(lambda x: list(map(lambda y: int(y), list(x))), tmp))
        keys = COLORS
        self.mpsz = dict(zip(keys, values))
        self.num = len(string) - 4
        self.update()

    def __str__(self):
        """
        Output the hand tiles in a formatted way.
        :return: a formatted string as ????m???p???s???z, e.g. 123m456p789s11234z
        """
        return ''.join([
                ''.join(
                    sorted(
                    [
                        m[0] for m in self.mahjongs
                        if m[1] == ch
                    ])
                )
                + ch
                for ch in COLORS])

    def can_chi(self, mj):
        """
        Whether this hand can call Chi upon a tile.
        :param mj: in the format r'[1-9][mps]' or r'[1-7][z]'
        :return: a list of possibilities for tiles for calling Chi
        """
        # 3 possibilities for chi: [n-2,n-1], [n-1, n+1], [n+1, n+2]
        n, color = int(mj[0]), mj[1]
        s = self.mpsz[color]
        poss = []
        for p in [(n-2, n-1), (n-1, n+1), (n+1, n+2)]:
            if 1 <= p[0] <= 9 and 1 <= p[1] <= 9 and (p[0] in s) and (p[1] in s):
                poss.append(p)
        return poss

    def can_pon(self, mj):
        n, color = int(mj[0]), mj[1]
        return sum(np.array(self.mpsz[color]) == n) >= 2

    def can_kan(self, mj, type):
        n, color = int(mj[0]), mj[1]
        if type == 1 or type == 2:
            return sum(np.array(self.mpsz[color] == n)) == 3
        else:
            return any(all(i) for i in np.array(self.exposure) == [n] * 3)

    def take(self, mjs):
        # keep track of two sources
        if type(mjs) == list:
            self.mahjongs += mjs
            for mj in mjs: self.mpsz[mj[1]].append(int(mj[0]))
            self.num += len(mjs)
        elif type(mjs) in (str, np.string_):
            self.mahjongs.append(mjs)
            self.mpsz[mjs[1]].append(int(mjs[0]))
            self.num += 1
        else:
            return

    def discard(self, mjs):
        # when pool == None: transfer from mahjongs to hidden or exposure
        # when pool exists: discard to the pool
        if type(mjs) == list:
            for mj in mjs:
                self.mpsz[mj[1]].remove(int(mj[0]))
                self.mahjongs.remove(mj)
            self.num -= len(mjs)
        elif type(mjs) in (str, np.string_):
            self.mahjongs.remove(mjs)
            self.mpsz[mjs[1]].remove(int(mjs[0]))
            self.num -= 1
        else:
            pass

    def pon(self, mj):
        for _ in range(2):
            # self.mahjongs.remove(mj)
            self.discard(mj)
        self.exposure.append([mj] * 3)
        self.is_clear = False

    def chi(self, mj, mj1, mj2):
        self.discard([mj1, mj2])
        self.exposure.append([mj, mj1, mj2])
        self.is_clear = False

    def kan(self, mj, kan_type):
        # type 1->off, 2->add, 3->on
        if kan_type == 1:
            self.discard([mj] * 3)
            self.hidden.append([mj] * 4)

        elif kan_type == 2:
            self.discard([mj] * 3)
            self.exposure.append([mj] * 4)

        elif kan_type == 3:
            for ex in self.exposure:
                if ex == [mj] * 3:
                    ex.append(mj)
                    break
        else:
            pass

    # return all the possible eyese
    def get_eyes(self):
        eyes = [[str(k)+ch for k, v in Counter(self.mpsz[ch]).items() if v >= 2] for ch in COLORS]
        return list(itertools.chain(*eyes))

    # temporarily remove the eye (for a trial)
    def remove_eye(self, eye):
        self.discard([eye, eye])

    # append the eye back
    def append_eye(self, eye):
        self.take([eye, eye])

    def win(self):
        # firstly not consider the special winning pattern
        # 1) find all the possibilities of doubles
        eyes = self.get_eyes()
        if not eyes:
            return False

        for eye in eyes:
            self.remove_eye(eye)
            cp = self.mpsz.copy()
            vals = [cp['m'], cp['p'], cp['s'], cp['z']]

            if func1(vals):
                self.append_eye(eye)
                return True
            else:
                self.append_eye(eye)

        return False

    def tenpai(self, step_from=0):
        """
        :param step_from: the steps from tenpai
        :return:
        """

        if step_from == 0:

            if self.num != 13:
                return False
            res = []
            for c in COLORS:
                if len(self.mpsz[c]) % 3 == 0:
                    continue
                for n in NUMBERS:
                    mj = str(n) + c
                    self.take(mj)
                    if self.win():
                        res.append(mj)
                    self.discard(mj)
            # return (True if res else False), res
            return res

        else:
            res = []
            for c in COLORS:
                if not self.mpsz[c]:
                    continue
                for n1 in NUMBERS:
                    mj_add = str(n1) + c
                    self.take(mj_add)
                    for mj_del in self.mahjongs:
                        self.discard(mj_del)
                        if self.tenpai(step_from-1):
                            res.append(mj_add)
                            self.take(mj_del)
                            break
                        self.take(mj_del)
                    self.discard(mj_add)
            return res

    def tenpai_step_1(self):
        res = []
        for c in COLORS:
            if not self.mpsz[c]:
                continue
            for n in NUMBERS:
                mj = str(n) + c

    def sort_mpsz(self):
        for ch in COLORS:
            self.mpsz[ch].sort()

    # get the number of isolated mahjongs
    def isolated(self):
        self.sort_mpsz()
        res = []
        for ch in 'm', 'p', 's':
            tmp, s = self.mpsz[ch], set()
            for dif in np.where(np.diff(tmp) <= 2)[0]:
                s.add(dif)
                s.add(dif + 1)
            res += [str(tmp[p]) + ch for p in (set(range(len(tmp))) - s)]
        ctr = Counter(self.mpsz['z'])
        for c in ctr:
            if ctr[c] < 2:
                res.append(str(c) + 'z')
        return res