package console

import (
	"fmt"

	"github.com/TwiN/go-color"
)

// values
var banner = []string{
	"   _  __      __  _      __               ",
	"  / |/ /___  / /_| | /| / /___ _ ____ ___ SPOOFER",
	" /    // -_)/ __/| |/ |/ // _ `// __// -_)",
	"/_/|_/ \\__/ \\__/ |__/|__/ \\_,_//_/   \\__/ ",
}

// methods
func DisplayBanner() {
	for _, part := range banner {
		for __ := 0; __ < ((Width / 2) - 21); __++ {
			fmt.Print(" ")
		}

		fmt.Print(part[:17])
		fmt.Print(color.Colorize(colorList["red"], part[17:42]))

		if len(part) > 42 {
			fmt.Print(color.Colorize(colorList["gray"], part[42:49]))
		}

		fmt.Println()
	}
	fmt.Println("\n")
}
