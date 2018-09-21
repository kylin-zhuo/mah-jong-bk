from hand import *


class Player:

    def __init__(self,
                 name=None,
                 location=None,
                 is_dealer=None,
                 score=None,
                 uuid=None,
                 pool=None):
        """
        :param name
        :param location: {'e', 's', 'w', 'n'}
        :param is_dealer: boolean
        :param score: the initial score
        :param id: the uuid
        """
        self.name = name
        self.location = location
        self.is_dealer = is_dealer
        self.score = score
        self.hands = Hand(uuid=uuid)
        self.uuid = uuid
        self.next = None
        self.pool = pool

    def assign_pool(self, pool):
        self.pool = pool

    def take(self, mjs):
        self.hands.take(mjs)

    def discard(self, mjs, pool=None):
        self.hands.discard(mjs)
        # discard into the pool
        if pool:
            # specified by the location
            pool.discard[self.location] += mjs

    def add_score(self, s):
        self.score += s

    def can_pon(self, mj):
        return self.hands.can_pon(mj)

    def can_chi(self, mj):
        return self.hands.can_chi(mj)

    def can_kan(self, mj):
        return self.hands.can_kan(mj)

    # define the dynamic actions: pon, chi, kan, richi, win(tsumo, ron)
    def pon(self, mj):
        # mahjong has format '1s'
        self.hands.pon(mj)

    def chi(self, mj, mj1, mj2):
        self.hands.chi(mj, mj1, mj2)

    def kan(self, mj):
        self.hands.kan(mj)

    def richi(self):
        self.score -= RICHI_FEE
        self.pool.n_richi += 1

    def win(self, mj=None, type='ron'):
        if not mj:
            return self.hands.win()
        else:
            self.hands.take(mj)
            ret = self.hands.win()
            self.hands.discard(mj)
            return ret

    def tenpai(self):
        return self.hands.tenpai()