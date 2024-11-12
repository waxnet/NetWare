# check decorator
class Tasks:
    registry : dict = {}

    def __init__(self, tag : int, message : str, error : str) -> None:
        self.tag = tag
        self.message = message
        self.error = error

    def __call__(self, target):
        Tasks.registry[self.tag] = (self.message, target, self.error)
        return target

    @staticmethod
    def get() -> list[tuple]:
        return [(message, target, error) for (_, (message, target, error)) in sorted(Tasks.registry.items())]
