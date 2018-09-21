from mahjong import *


class Mountain:

    def __init__(self):
        self.mahjongs = generate_all_mahjong(shuffled=True)
        # treasure
        self.treasure = [self.mahjongs[-5]]
        self.pointer = 0
        self.next = self.mahjongs[self.pointer]

    # take multiple mahjongs, especially in the distribution phase
    def take(self, num=None):
        if num is None:
            ret = self.mahjongs[self.pointer]
            self.pointer += 1
            return ret
        else:
            res = list(self.mahjongs[self.pointer: self.pointer + num])
            self.pointer += num
            return res

