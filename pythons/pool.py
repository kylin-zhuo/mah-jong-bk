from mahjong import *


class Pool:

    def __init__(self, chan=None):

        self.num = 0
        self.discard = {'e': [], 's': [], 'w': [], 'n': []}
        self.n_richi = 0
        self.score_left = 0
        self.honba = 0
        self.chan = chan

    def set_chan(self, player):
        self.chan = player

    def get_exposure(self, location):
        n = {'e': 0, 's': 1, 'w': 2, 'n': 3}[location]
        p = self.chan
        while n > 0:
            p = p.next
            n -= 1
        return p.hands.exposure

    # refresh the pool when a game has ended
    def refresh(self, ryuukyoku=False, renchan=False):
        if ryuukyoku:
            self.score_left = self.honba * HONBA_FEE + self.n_richi * RICHI_FEE
        if renchan:
            self.honba += 1
        else:
            self.honba = 0
            # self.chan = self.chan + 1 if self.chan < 4 else 1
            self.chan = self.chan.next

        self.num = 0
        self.discard = {'e': [], 's': [], 'w': [], 'n': []}