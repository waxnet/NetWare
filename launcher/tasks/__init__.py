# check decorator
class Tasks:
    registry = {}

    def __init__(self, tag, message, error):
        self.tag = tag
        self.message = message
        self.error = error

    def __call__(self, target):
        Tasks.registry[self.tag] = (self.message, target, self.error)
        return target

    @staticmethod
    def get():
        return [(message, target, error) for (_, (message, target, error)) in sorted(Tasks.registry.items())]
